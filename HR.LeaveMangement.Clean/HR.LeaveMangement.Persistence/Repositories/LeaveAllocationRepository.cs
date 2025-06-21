using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveMangement.Domain;
using HR.LeaveMangement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveMangement.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        public LeaveAllocationRepository(HrDatabaseContext hrDatabaseContext) : base(hrDatabaseContext)
        {
        }

        public async Task AddAllocations(List<LeaveAllocation> allocations)
        {
            await _hrDatabaseContext.AddRangeAsync(allocations);
            await _hrDatabaseContext.SaveChangesAsync();
        }

        public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
        {
            return await _hrDatabaseContext.LeaveAllocations.AnyAsync(
                q=>q.EmployeeId==userId
                && q.LeaveTypeId==leaveTypeId 
                && q.Period==period);
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
        {
            var leaveAllocation = await _hrDatabaseContext.LeaveAllocations
                .Include(q => q.LeaveType)
                .ToListAsync();
            return leaveAllocation;
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
        {
            var leaveAllocations = await _hrDatabaseContext.LeaveAllocations.Where(q => q.EmployeeId == userId)
                .Include(q => q.LeaveType)
                .ToListAsync();
            return leaveAllocations;
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            var leaveAllocation = await _hrDatabaseContext.LeaveAllocations
                .Include(q => q.LeaveType)
                .FirstOrDefaultAsync(q => q.Id == id);
            return leaveAllocation;
        }

        public async Task<LeaveAllocation> GetUserAllocations(string userId, int LeaveTypeId)
        {
            return await _hrDatabaseContext.LeaveAllocations.FirstOrDefaultAsync(q=>q.EmployeeId==userId && q.LeaveTypeId==LeaveTypeId);
        }
    }
}
