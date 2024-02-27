using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("Areas")]
    public class Area : BaseEntity
    {
        public string Name { get; set; }
        public int ZipCode { get; set; }
        public string AreaInformation { get; set; }

        public ICollection<PostalOffice> PostalOffices { get; set; }
    }
}
