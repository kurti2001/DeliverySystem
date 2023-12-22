using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("Packages")]
    public class Package
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPackage { get; set; }
        [Required]
        public string Name { get; set; }
        public int BarcodePackage { get; set; }
        public string SentAddress { get; set; }
        public string DestinationAddress { get; set; }
        public List<Shipment> Shipments { get; set; }
    }
}
