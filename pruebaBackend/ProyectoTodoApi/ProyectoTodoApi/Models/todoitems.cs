using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoTodoApi.Models
{
    public class todoitems
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public bool iscompleted { get; set; }
  
    }


}
