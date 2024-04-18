using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequests>, ILeaveRequestRepository
    {
        public LeaveRequestRepository(HRDatabaseContext context) : base(context)
        {
        }

        public async Task<LeaveRequests> GetLeaveRequestsWithDetails(int id)
        {
            var leaveRequests = await _context.LeaveRequests
                .Include(x => x.LeaveType)
                .FirstOrDefaultAsync(x => x.Id == id);

            return leaveRequests;
        }

        public async Task<List<LeaveRequests>> GetLeaveRequestsWithDetails()
        {
            var leaveRequests = await _context.LeaveRequests
            .Include(x => x.LeaveType)
            .ToListAsync();

            return leaveRequests;
        }

        public async Task<List<LeaveRequests>> GetLeaveRequestsWithDetails(string userId)
        {
            var leaveRequests = await _context.LeaveRequests
                    .Where( u => u.RequestingEmployeeId == userId)
                    .Include(x => x.LeaveType)
                    .ToListAsync();

            return leaveRequests;
        }
    }
}
