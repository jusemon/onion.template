namespace Company.Project.Domain.Services.Security.User
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

    /// <summary>
    /// User Service class. 
    /// </summary>
    /// <seealso cref="Company.Project.Domain.Services.Generics.Base.BaseService{Company.Project.Domain.Entities.Security.Users}" />
    /// <seealso cref="Company.Project.Domain.Interfaces.Security.User.IUserService" />
    public class UserService : BaseService<Users>, IUserService
    {
        /// <summary>
        /// The base repository
        /// </summary>
        private readonly IBaseRepository<Users> baseRepository;

        /// <summary>
        /// The permission repository
        /// </summary>
        private readonly IBaseRepository<Permissions> permissionRepository;

        /// <summary>
        /// The action repository
        /// </summary>
        private readonly IBaseRepository<Entities.Security.Actions> actionRepository;

        /// <summary>
        /// The authentication configuration
        /// </summary>
        private readonly AuthConfig authConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="baseRepository">The base repository.</param>
        /// <param name="permissionRepository">The permission repository.</param>
        /// <param name="actionRepository">The action repository.</param>
        /// <param name="authConfig">The authentication configuration.</param>
        public UserService(
            IBaseRepository<Users> baseRepository,
            IBaseRepository<Permissions> permissionRepository,
            IBaseRepository<Actions> actionRepository,
            AuthConfig authConfig) : base(baseRepository)
        {
            this.baseRepository = baseRepository;
            this.authConfig = authConfig;
            this.actionRepository = actionRepository;
            this.permissionRepository = permissionRepository;
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Users> Read()
        {
            return base.Read().Select(u=> {
                u.Password = string.Empty;
                return u;
            });
        }

        /// <summary>
        /// Reads by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public override Users Read(int id)
        {
            var user = base.Read(id);
            user.Password = string.Empty;
            return user;
        }

        /// <summary>
        /// Reads with paged results.
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="isAsc">if set to <c>true</c> [is asc].</param>
        /// <returns></returns>
        public override Page<Users> Read(int pageIndex, int pageSize, string sortBy = null, bool isAsc = true)
        {
            var page = base.Read(pageIndex, pageSize, sortBy, isAsc);
            page.Items = page.Items.Select(u => {
                u.Password = string.Empty;
                return u;
            });
            return page;
        }

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public override bool Create(Users entity)
        {
            entity.Password = Cryptography.GetHash(Encoding.UTF8.GetString(Convert.FromBase64String(entity.Password)));
            var res = base.Create(entity);
            entity.Password = string.Empty;
            return res;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public override bool Update(Users entity)
        {
            if (!string.IsNullOrWhiteSpace(entity.Password))
            {
                entity.Password = Cryptography.GetHash(Encoding.UTF8.GetString(Convert.FromBase64String(entity.Password)));
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

        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <exception cref="AppException">Usuario o contraseña incorrectos.</exception>
        public Users Login(string username, string password)
        {
            var result = this.baseRepository.Read((u) => u.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            var hash = Cryptography.GetHash(Encoding.UTF8.GetString(Convert.FromBase64String(password)));
            if (result != null && Cryptography.Validate(result.Password, Encoding.UTF8.GetString(Convert.FromBase64String(password))))
            {
                result.Token = this.GetToken(result, this.authConfig.Key, authConfig.SessionTimeout);
                return result;
            }
            throw new AppException(AppExceptionTypes.Validation, "Usuario o contraseña incorrectos.");
        }

        /// <summary>
        /// Gets the user with recovery token.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public Users GetUserWithRecoveryToken(string email)
        {
            var user = new Users { Email = email };
            user = this.baseRepository.Read(u => email.ToUpperInvariant()?.Trim() == u.Email?.ToUpperInvariant()?.Trim()).FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            user.Token = this.GetToken(user, user.Password, 12);
            return user;
        }

        /// <summary>
        /// Checks the recovery token.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        /// <exception cref="AppException">Enlace de recuperación inválido.
        /// or
        /// El enlace de recuperación ha expirado.</exception>
        public Users CheckRecoveryToken(long id, string token)
        {
            try
            {
                var currentUser = this.baseRepository.Read(id);
                var key = Encoding.UTF8.GetBytes(currentUser.Password);
                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out SecurityToken securityToken);
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

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="secretKey">The secret key.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        private string GetToken(Users user, string secretKey, int timeout)
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

        /// <summary>
        /// Gets the claims.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        private List<Claim> GetClaims(Users user)
        {
            var claims = new List<Claim> {
                            new Claim(ClaimTypes.Name, user.Username),
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
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
