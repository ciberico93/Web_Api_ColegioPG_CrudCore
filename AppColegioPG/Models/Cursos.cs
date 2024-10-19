using System.ComponentModel.DataAnnotations;

namespace AppColegioPG.Models
{
    public class Cursos
    {
        [Key]
        public int Id_cursos { get; set; }

        public string Nombre { get; set; }
    }
}
