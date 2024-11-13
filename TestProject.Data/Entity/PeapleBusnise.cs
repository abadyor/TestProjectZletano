using System.ComponentModel.DataAnnotations;

namespace TestProject.Data.Entity
{
    public class PeapleBusnise
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name_user { get; set; }

        public string? Email { get; set; }
        [Required, StringLength(50)]
        public string Password { get; set; }
        [MaxLength(50)]
        public string? Address { get; set; }
        [StringLength(50)]
        public string? Phone { get; set; }

        [Required]
        [StringLength(50)]
        public string Name_Shope { get; set; }

        [MaxLength(100)]
        public string region_shope { get; set; }








    }
}
