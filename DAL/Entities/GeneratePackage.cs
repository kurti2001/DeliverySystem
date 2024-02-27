using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("Registerd Packages")]
    public class GeneratePackage : BaseEntity
    {
        public string Name { get; set; }
        public string SenderInformation { get; set; }
        public string Email {  get; set; }
        public DateTime CreatedDate { get; set; }
        public string DestinationAddress { get; set; }
        public int DestinationZipCode {  get; set; }
        public string SentAddress { get; set; }
        public int SentZipCode { get; set; }
        
        public float Weight { get; set; }
    }
}
