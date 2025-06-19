using HR.LeaveMangement.Domain.Common;

namespace HR.LeaveMangement.Domain;

public class LeaveAllocation : BaseEntity
{
    public int NumberOfDays { get; set; }

    public LeaveType? LeaveType { get; set; }
    public int LeaveTypeId { get; set; }

    public int Period { get; set; }
}
