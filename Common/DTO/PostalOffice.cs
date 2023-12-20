using System.ComponentModel.DataAnnotations;

namespace Common.DTO
{
    public class PostalOffice
    {
        public int PostalOfficeId { get; set; }
        public string OfficeName { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class AddPostalOfficeModel
    {
        public int PostalOfficeId { get; set; }
        [Required(ErrorMessage = "Add Office Name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name should be more than 2 characters long")]
        public string OfficeName { get; set; }
        [Required(ErrorMessage = "Add the region location of the office")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Location should be more than 2 characters long")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Add the full Address of the office")]
        [StringLength(500)]
        public string Address { get; set; }
        [Required(ErrorMessage = "Add the phonenumber of the office")]
        [StringLength(500, MinimumLength = 5)]
        public string PhoneNumber { get; set; }
    }
}
