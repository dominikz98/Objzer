using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace api.Models
{
    public abstract class CTEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IList<CTHistory> History { get; set; } = new List<CTHistory>();
        public bool Deleted { get; set; }
    }
}
