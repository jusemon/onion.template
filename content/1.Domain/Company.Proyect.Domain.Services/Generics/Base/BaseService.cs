namespace Company.Proyect.Domain.Services.Generics.Base
{
    using Company.Proyect.Infra.Utils.Exceptions;
    using Entities.Generics;
    using Entities.Generics.Base;
    using FluentValidation;
    using Interfaces.Generics.Base;
    using System.Collections.Generic;
    using System.Linq;

    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly IBaseRepository<TEntity> baseRepository;

        protected AbstractValidator<TEntity> Validator { get; set; }

        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            this.baseRepository = baseRepository;
        }

        public virtual bool Create(TEntity entity)
        {
            this.Validate(entity);
            return this.baseRepository.Create(entity);
        }

        public virtual bool Delete(int id)
        {
            return this.baseRepository.Delete(id);
        }

        public virtual IEnumerable<TEntity> Read()
        {
            return this.baseRepository.Read();
        }

        public virtual TEntity Read(int id)
        {
            return this.baseRepository.Read(id);
        }

        public virtual Page<TEntity> Read(int pageIndex, int pageSize, string sortBy = null, bool isAsc = true)
        {
            return this.baseRepository.Read(pageIndex, pageSize, sortBy, isAsc);
        }

        public virtual bool Update(TEntity entity)
        {
            this.Validate(entity);
            return this.baseRepository.Update(entity);
        }

        protected virtual void Validate(TEntity entity)
        {
            if (this.Validator != null)
            {
                var result = this.Validator.Validate(entity);
                if (!result.IsValid)
                {
                    throw new AppException(AppExceptionTypes.Validation, result.Errors.FirstOrDefault()?.ErrorMessage);
                }
            }
        }
    }
}
