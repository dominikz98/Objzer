namespace api.Models
{
    public class CTAbstraction : CTContract
    {
        public IList<CTAbstractionAssignment> Inheritances { get; set; } = new List<CTAbstractionAssignment>();
    }

    public class CTAbstractionAssignment
    {
        public Guid ParentId { get; set; }
        public CTAbstraction? Parent { get; set; }
        public IList<CTAbstraction> Children { get; set; } = new List<CTAbstraction>();
    }
}
