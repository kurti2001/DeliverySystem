using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Common.DTO
{
    public class AddAreaModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Add Area Name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name should be more than 2 characters long")]
        [DisplayName("Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Add the ZipCode of the area")]
        [DisplayName("Zip Code")]
        public int ZipCode { get; set; }
        [Required(ErrorMessage = "Add the full address of the area")]
        [StringLength(500)]
        [DisplayName("Information")]
        public string AreaInformation { get; set; }
    }
}
