using System.ComponentModel.DataAnnotations;

namespace TestProject.Data.Entity
{
    public class T2
    {
        [Key]
        public int id { get; set; }
        [MaxLength(50)]
        public string name { get; set; }
        [MaxLength]
        public string des { get; set; }
    }
}
