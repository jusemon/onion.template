namespace Company.Proyect.Domain.Services.Security.User
{
    using Entities.Config;
    using Entities.Generics;
    using Entities.Security;
    using Generics.Base;
    using Infra.Utils.Exceptions;
    using Infra.Utils.Security;
    using Interfaces.Generics.Base;
    using Interfaces.Security.User;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;

    public class UserService : BaseService<User>, IUserService
    {
        private readonly IBaseRepository<User> baseRepository;

        private readonly IBaseRepository<Permission> permissionRepository;

        private readonly IBaseRepository<Entities.Security.Action> actionRepository;

        private readonly AuthConfig authConfig;

        public UserService(
            IBaseRepository<User> baseRepository,
            IBaseRepository<Permission> permissionRepository,
            IBaseRepository<Entities.Security.Action> actionRepository,
            AuthConfig authConfig) : base(baseRepository)
        {
            this.baseRepository = baseRepository;
            this.authConfig = authConfig;
            this.actionRepository = actionRepository;
            this.permissionRepository = permissionRepository;
        }

        public override IEnumerable<User> Read()
        {
            return base.Read().Select(u=> {
                u.Password = string.Empty;
                return u;
            });
        }

        public override User Read(int id)
        {
            var user = base.Read(id);
            user.Password = string.Empty;
            return user;
        }

        public override Page<User> Read(int pageIndex, int pageSize, string sortBy = null, bool isAsc = true)
        {
            var page = base.Read(pageIndex, pageSize, sortBy, isAsc);
            page.Items = page.Items.Select(u => {
                u.Password = string.Empty;
                return u;
            });
            return page;
        }

        public override bool Create(User entity)
        {
            entity.Password = Cryptography.GetHash(Encoding.UTF8.GetString(Convert.FromBase64String(entity.Password)), this.authConfig.Key);
            var res = base.Create(entity);
            entity.Password = string.Empty;
            return res;
        }

        public override bool Update(User entity)
        {
            if (!string.IsNullOrWhiteSpace(entity.Password))
            {
                entity.Password = Cryptography.GetHash(Encoding.UTF8.GetString(Convert.FromBase64String(entity.Password)), this.authConfig.Key);
            }
            else
            {
                var old = this.baseRepository.Read(entity.Id);
                entity.Password = old.Password;
            }
            var res = base.Update(entity);
            entity.Password = string.Empty;
            return res;
        }

        public void Login(User user)
        {
            var result = this.baseRepository.Read((u) => u.Username.ToUpperInvariant() == user.Username.ToUpperInvariant()).FirstOrDefault();
            var pass = Cryptography.GetHash(Encoding.UTF8.GetString(Convert.FromBase64String(user.Password)), this.authConfig.Key);
            if (result != null && Cryptography.Validate(result.Password, Encoding.UTF8.GetString(Convert.FromBase64String(user.Password)), this.authConfig.Key))
            {
                user.Token = this.GetToken(result, this.authConfig.Key, authConfig.SessionTimeout);
                user.Password = string.Empty;
                user.RoleId = result.RoleId;
                user.Email = result.Email;
                user.Id = result.Id;
                return;
            }
            throw new AppException(AppExceptionTypes.Validation, "Usuario o contraseña incorrectos.");
        }

        public User GetUserWithRecoveryToken(string email)
        {
            var user = new User { Email = email };
            user = this.baseRepository.Read(u => email.ToUpperInvariant()?.Trim() == u.Email?.ToUpperInvariant()?.Trim()).FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            user.Token = this.GetToken(user, user.Password, 12);
            return user;
        }

        public User CheckRecoveryToken(User user)
        {
            try
            {
                var currentUser = this.baseRepository.Read(user.Id);
                var key = Encoding.UTF8.GetBytes(currentUser.Password);
                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(user.Token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out SecurityToken token);
                var claim = principal.Claims.First(c => c.Type == ClaimTypes.Name);
                if (claim.Value != currentUser.Username)
                {
                    throw new AppException(AppExceptionTypes.Validation, "Enlace de recuperación inválido.");
                }
                currentUser.Password = string.Empty;
                return currentUser;
            }
            catch (AppException)
            {
                throw;
            }
            catch (SecurityTokenExpiredException)
            {
                throw new AppException(AppExceptionTypes.Validation, "El enlace de recuperación ha expirado.");
            }
            catch (Exception)
            {
                throw new AppException(AppExceptionTypes.Validation, "Enlace de recuperación inválido.");
            }
        }

        private string GetToken(User user, string secretKey, int timeout)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.Default.GetBytes(secretKey);
            var claims = this.GetClaims(user);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(timeout),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return token;
        }

        private List<Claim> GetClaims(User user)
        {
            var claims = new List<Claim> {
                            new Claim(ClaimTypes.Name, user.Username)
                        };
            var permissions = this.permissionRepository.Read((p) => p.RoleId == user.RoleId).Select(p => p.ActionId).ToList();
            var customClaims = this.actionRepository.Read((a) => permissions.Contains(a.Id)).Select(p =>
                new Claim(CustomClaimTypes.Permission, p.Name)
            );
            claims.AddRange(customClaims);
            return claims;
        }
    }
}
