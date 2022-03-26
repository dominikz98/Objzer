namespace api.Models
{
    public class CTInterface : CTContract
    {
        public IList<CTInterfaceAssignment> Implementations { get; set; } = new List<CTInterfaceAssignment>();
    }

    public class CTInterfaceAssignment
    {
        public Guid ParentId { get; set; }
        public CTInterface? Parent { get; set; }
        public IList<CTInterface> Children { get; set; } = new List<CTInterface>();
    }
}
