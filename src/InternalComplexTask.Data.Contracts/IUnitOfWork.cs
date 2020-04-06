using System.Threading.Tasks;

namespace InternalComplexTask.Data.Contracts
{
    public interface IUnitOfWork
    {
        ICompanyRepository Companies { get; }
        IDepartmentRepository Departments { get; }
        IProjectRepository Projects { get; }
        IEmployeeRepository Employees { get; }
        Task<int> CommitAsync();
    }
}
