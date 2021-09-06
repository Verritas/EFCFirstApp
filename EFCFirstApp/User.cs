using System.ComponentModel.DataAnnotations;

#nullable disable

namespace EFCFirstApp
{
    public partial class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
