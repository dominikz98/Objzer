namespace api.Models
{
    public abstract class CTContract : CTEntity
    {
        public IList<CTObject> Objects { get; set; } = new List<CTObject>();
    }
}
