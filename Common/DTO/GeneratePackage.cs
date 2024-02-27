using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Common.DTO
{
    public class GeneratePackageModel
    {
        public int Id { get; set; }
        [MaxLength(250)]
        [DisplayName("Full Name")]
        public string Name { get; set; }
        [MaxLength(250)]
        [DisplayName("Package Details")]
        public string SenderInformation { get; set; }

        [MaxLength(250)]
        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }

        [MaxLength(250)]
        [DisplayName("Destination Address")]
        public string DestinationAddress { get; set; }

        [Range(1000, 9999, ErrorMessage = "Zip Code must be 4 digits long.")]
        [DisplayName("Zip Code (Destination Address)")]
        public int DestinationZipCode { get; set; }

        [MaxLength(250)]
        [DisplayName("Sent Address")]
        public string SentAddress { get; set; }

        [Range(1000, 9999, ErrorMessage = "Zip Code must be 4 digits long.")]
        [DisplayName("ZipCode (Sent Address)")]
        public int SentZipCode { get; set; }

        [Range(1, 200)]
        [DisplayName("Weight")]
        public float Weight { get; set; }
    } 

}
