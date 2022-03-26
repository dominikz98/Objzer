namespace api.Models
{
    public class CTProperty : CTEntity
    {
        public bool Key { get; set; }
        public PropertyTypes Type { get; set; } = PropertyTypes.String;
        public string Column { get; set; } = string.Empty;
        public bool Required { get; set; } = true;
        public int? MaxLength { get; set; }
        public int? StringLength { get; set; }

        public CTObject? Object { get; set; }
    }

    public enum PropertyTypes
    {
        Bool,
        Byte,
        Char,
        String,
        Double,
        Short,
        Int,
        Long
    }
}
