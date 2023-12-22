using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Shipment
    {
        [Key]
        public int ShipmentId { get; set; }

        [ForeignKey("Package")]
        public int IdPackage { get; set; }
        public Package Package { get; set; }

        [ForeignKey("PostalOffice")]
        public int PostalOfficeId { get; set; }
        public PostalOffice PostalOffice { get; set; }

        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
    }
}
