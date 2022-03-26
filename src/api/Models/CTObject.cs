namespace api.Models
{
    public class CTObject : CTEntity
    {
        public IList<CTContract> Contracts { get; set; } = new List<CTContract>();
        public IList<CTProperty> Properties { get; set; } = new List<CTProperty>();
    }
}
