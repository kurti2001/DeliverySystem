using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DAL.Entities;

namespace Common.DTO
{

    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
    public class PackageAddModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name should be more than 2 characters long")]
        public string Name { get; set; }

        [MaxLength(250)]
        [DisplayName("Enter package details")]
        public string SenderInformation { get; set; }

        [MaxLength(250)]
        [DisplayName("Enter your email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Add barcode")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Barcode should be more than 2 characters long")]
        public string BarcodePackage { get; set; }

        [Required(ErrorMessage = "Add the address where the package is sent, Length should be less than 500 characters")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = "SentAddress should be between 2 and 500 characters.")]
        public string SentAddress { get; set; }

        [Range(1000, 9999, ErrorMessage = "Zip Code must be 4 digits long.")]
        [DisplayName("Sent address ZipCode")]
        public int SentZipCode { get; set; }

        [Required(ErrorMessage = "Add the destination address, Length should be less than 500 characters")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = "DestinationAddress should be between 2 and 500 characters.")]
        public string DestinationAddress { get; set; }

        [Range(1000, 9999, ErrorMessage = "Zip Code must be 4 digits long.")]
        [DisplayName("Destination of the package Zip Code")]
        public int DestinationZipCode { get; set; }

        public PackageStatus Status { get; set; }
    }
}
