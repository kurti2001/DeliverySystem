using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("PostalOffices")]
    public class PostalOffice : BaseEntity
    {
        public string OfficeName { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }


        public int AreaId { get; set; }
        public Area Area { get; set; }

    }
}
