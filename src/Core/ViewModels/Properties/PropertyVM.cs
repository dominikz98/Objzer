using Core.Models.Enumerations;

namespace Core.ViewModels.Properties
{
    public class PropertyVM
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public PropertyType Type { get; set; } = PropertyType.String;
        public bool Required { get; set; } = true;
    }
}
