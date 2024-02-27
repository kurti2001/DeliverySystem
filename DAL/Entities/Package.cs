using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("Packages")]
    public class Package : BaseEntity
    {
        public string Name { get; set; }
        public string BarcodePackage { get; set; }
        public string SenderInformation { get; set; }
        public string Email { get; set; }
        public string DestinationAddress { get; set; }
        public int DestinationZipCode { get; set; }
        public string SentAddress { get; set; }
        public int SentZipCode { get; set; }
        public PackageStatus Status { get; set; }
    }

    public enum PackageStatus
    {
        Created,
        Transported,
        Accepted,
        Rejected
    }
}
