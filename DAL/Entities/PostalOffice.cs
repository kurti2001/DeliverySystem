using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    [Table("PostalOffices")]
    public class PostalOffice
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostalOfficeId { get; set; }

        public string OfficeName { get; set; }

        public string Location { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }
        public List<Shipment> Shipments { get; set; }
    }
}
