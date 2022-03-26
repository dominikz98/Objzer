namespace api.Models
{
    public class CTEnumeration : CTContract
    {
        public IList<string> Values { get; set; } = new List<string>();
    }
}
