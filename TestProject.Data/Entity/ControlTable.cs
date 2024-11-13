using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject.Data.Entity
{
    public class ControlTable
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Vendor))]
        public int? id_vendor { get; set; }
        public Vendor Vendor { get; set; }
        [StringLength(8)]
        public string? M_Code { get; set; }
        [StringLength(8)]
        public string? Last_sore { get; set; }

        public Int64? visitor { get; set; }

        [StringLength(50)]
        public string? shopeName { get; set; }
        [StringLength(200)]
        public string? Address { get; set; }
        [StringLength(20)]
        public string? region { get; set; }

        [StringLength(20)]
        public string? city { get; set; }


        [StringLength(20)]
        public string? Street { get; set; }
        [StringLength(80)]
        public string? NerestPoint { get; set; }
    }
}
