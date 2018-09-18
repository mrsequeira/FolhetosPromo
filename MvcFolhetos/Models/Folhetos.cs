using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace MvcFolhetos.Models
{
    public class Folhetos
    {
        
        public int FolhetosID { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Tens de preencher o campo {0} , dumb.")]
        public string Titulo { get; set; }

        public string Descricao { get; set; }
        public string Pasta { get; set; }

        [Display(Name = "Data de Inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataInic { get; set; }

        [Display(Name = "Data Final")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataFim { get; set; }

        [Display(Name = "Nome da empresa")]
        [Required(ErrorMessage = "Tens de preencher o campo {0} , dumb.")]
        public string NomeEmpresa { get; set; }
    }

    public class FolhetosDBContext : DbContext
    {
        public DbSet<Folhetos> Folhetos { get; set; }
    }
}