using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InternalComplexTask.Data.Contracts;
using InternalComplexTask.Data.Repositories;

namespace InternalComplexTask.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbContext _dbContext;

        private ICompanyRepository _companyRepository;
        private IDepartmentRepository _departmentRepository;
        private IProjectRepository _projectRepository;
        private IEmployeeRepository _userRepository;

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public ICompanyRepository Companies => _companyRepository ??= new CompanyRepository(_dbContext);
        public IDepartmentRepository Departments => _departmentRepository ??= new DepartmentRepository(_dbContext);
        public IProjectRepository Projects => _projectRepository ??= new ProjectRepository(_dbContext);
        public IEmployeeRepository Employees => _userRepository ??= new EmployeeRepository(_dbContext);

        public Task<int> CommitAsync() => _dbContext.SaveChangesAsync();

        public void Dispose() => _dbContext?.Dispose();
    }
}
