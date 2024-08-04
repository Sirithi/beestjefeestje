using System.ComponentModel.DataAnnotations;

namespace BeestjeFeestje.Data.Entities
{
    public class AType
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public AType(string name)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
        }
    }
}
