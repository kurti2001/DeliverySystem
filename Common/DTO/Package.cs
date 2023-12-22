using System.ComponentModel.DataAnnotations;

namespace Common.DTO
{
    public class Package
    {
        public int IdPackage { get; set; }
        public string Name { get; set; }
        public int BarcodePackage { get; set; }
        public string SentAddress { get; set; }
        public string DestinationAddress { get; set; }
    }

    public class PackageAddModel
    {
        public int IdPackage { get; set; }
        [Required(ErrorMessage = "Add package name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name should be more than 2 characters long")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Add barcode")]
        [Range(1, int.MaxValue, ErrorMessage = "BarcodePackage must be a positive integer.")]
        public int BarcodePackage { get; set; }

        [Required(ErrorMessage = "Add the address where the package is sent, Length should be less than 500 characters")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = "SentAddress should be between 2 and 500 characters.")]
        public string SentAddress { get; set; }

        [Required(ErrorMessage = "Add the destination address, Length should be less than 500 characters")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = "DestinationAddress should be between 2 and 500 characters.")]
        public string DestinationAddress { get; set; }
    }

}