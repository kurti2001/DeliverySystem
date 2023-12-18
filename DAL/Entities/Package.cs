using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("Packages")]
    public class Package
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPackage { get; set; }
        [StringLength(12)]
        [Required]
        public string Name { get; set; }
        public int BarcodePackage { get; set; }
        public string SentAddress { get; set; }
        public string DestinationAddress { get; set; }

    }
}
