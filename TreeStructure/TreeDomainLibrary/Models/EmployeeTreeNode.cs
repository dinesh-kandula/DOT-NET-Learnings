namespace TreeDomainLibrary.Models
{
    public class EmployeeTreeNode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmployeeId { get; set; }
        public string JobTitle { get; set; }
        public string Role { get; set; }
        public int? ParentId { get; set; }
        public bool HasChildren { get; set; }
        public List<EmployeeTreeNode> Children { get; set; }
    }
}
