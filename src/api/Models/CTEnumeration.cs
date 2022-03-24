namespace api.Models
{
    public class CTEnumeration : CTContract
    {
        public List<string> Values { get; set; }

        public CTEnumeration()
        {
            Values = new List<string>();
        }
    }
}
