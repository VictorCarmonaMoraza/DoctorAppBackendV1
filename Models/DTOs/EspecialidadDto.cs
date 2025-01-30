using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class EspecialidadDto
    {
        //Solo propiedades que queremos mostrar
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre de la especialidad es requuerido")]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "El nombre debe ser minimo 1 y maximo 60 caracteres")]
        public string NombreEspecialidad { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "La descripcion debe ser minimo 1 y maximo 100 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El estado de la especialidad es requerido")]
        public int Estado { get; set; } //1-0
    }
}
