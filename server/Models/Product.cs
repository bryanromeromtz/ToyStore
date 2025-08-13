using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;   

namespace ToyStore.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no debe exceder 50 caracteres.")]
        public string Name { get; set; } = null!;

        [StringLength(100, ErrorMessage = "La descripción no debe exceder 100 caracteres.")]
        public string? Description { get; set; }

        [Range(0, 100, ErrorMessage = "La edad mínima debe estar entre 0 y 100.")]
        public int? AgeRestriction { get; set; }

        [Required(ErrorMessage = "La compañía es obligatoria.")]
        [StringLength(50, ErrorMessage = "La compañía no debe exceder 50 caracteres.")]
        public string Company { get; set; } = null!;

        [Range(1.0, 1000.0, ErrorMessage = "El precio debe estar entre $1 y $1000.")]
        [Precision(18, 2)] 
        public decimal Price { get; set; }
    }
}
