using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequests>
    {
        Task<LeaveRequests> GetLeaveRequestsWithDetails(int id);
        Task<List<LeaveRequests>> GetLeaveRequestsWithDetails();
        Task<List<LeaveRequests>> GetLeaveRequestsWithDetails(string userId);
    }   

}
