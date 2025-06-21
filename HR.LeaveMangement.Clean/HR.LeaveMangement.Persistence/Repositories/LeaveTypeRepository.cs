using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveMangement.Domain;
using HR.LeaveMangement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveMangement.Persistence.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(HrDatabaseContext hrDatabaseContext) : base(hrDatabaseContext)
        {
        }

        public async Task<bool> IsLeaveTypeUnique(string name)
        {
            return await _hrDatabaseContext.LeaveTypes.AnyAsync(q=>q.Name == name);
        }
    }   
}
