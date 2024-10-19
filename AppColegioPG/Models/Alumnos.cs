using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AppColegioPG.Models
{
    public class Alumnos
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }
        public string Correo { get; set; }

        public int Id_Cursos { get; set; }
        
        [ForeignKey("Id_Cursos")]
        public virtual Cursos? NavegacionCursos { get; set; }
    }
}
