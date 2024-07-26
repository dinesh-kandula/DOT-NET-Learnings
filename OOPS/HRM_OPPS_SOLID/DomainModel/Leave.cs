namespace HRM_OPPS_SOLID.DomainModel
{
    public enum LeaveType
    {
        Privilege,
        Special,
        Floater,
        Unpaid
    }

    public class Leave
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public LeaveType LeaveType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Reason { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
