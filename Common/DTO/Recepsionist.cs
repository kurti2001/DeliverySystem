using System.ComponentModel.DataAnnotations;

namespace Common.DTO
{
    public class Recepsionist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

    }
    public class RecepsionistAddModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Add recepsionist name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name should be more than 2 characters long")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Add the email of the recepsionist")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Email should be more than 2 characters long")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Add the phone number of the recepsionist")]
        [StringLength(500, MinimumLength = 5)]
        public string PhoneNumber { get; set; }
    }
}
