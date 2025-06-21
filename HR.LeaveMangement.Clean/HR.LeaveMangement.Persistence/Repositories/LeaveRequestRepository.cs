using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveMangement.Domain;
using HR.LeaveMangement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveMangement.Persistence.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        public LeaveRequestRepository(HrDatabaseContext hrDatabaseContext) : base(hrDatabaseContext)
        {
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
        {
            var leaveRequests = await _hrDatabaseContext.LeaveRequests
                .Include(q => q.LeaveType)
                .ToListAsync();
            return leaveRequests;
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId)
        {
            var leaveRequests = await _hrDatabaseContext.LeaveRequests.Where(q=>q.RequestingEmplopyeeId==userId)
                .Include(q => q.LeaveType)
                .ToListAsync();
            return leaveRequests;
        }

        public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
        {
            var leaveRequest = await _hrDatabaseContext.LeaveRequests
                .Include(q => q.LeaveType)
                .FirstOrDefaultAsync(q=>q.Id==id);  
            return leaveRequest;
        }
    }

   
}
