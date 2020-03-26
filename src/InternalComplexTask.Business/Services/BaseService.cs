using System;
using InternalComplexTask.Data.Contracts;
using Microsoft.Extensions.Logging;

namespace InternalComplexTask.Business.Services
{
    public class BaseService
    {
        protected readonly IUnitOfWork Uow;
        protected readonly ILogger<BaseService> Logger;

        protected BaseService(IUnitOfWork uow, ILogger<BaseService> logger)
        {
            Uow = uow ?? throw new ArgumentNullException(nameof(uow));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
