
using System.ComponentModel.DataAnnotations;

namespace DapperMVCDemo.Data.Models.Domain
{
    public class Person
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }

        public string Address { get; set; }

        //public char InActive {  get; set; }
    }
}
