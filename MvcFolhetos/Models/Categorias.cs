using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcFolhetos.Models
{
    public class Categorias
    {
        //Relação M-N
        public Categorias()
        {
            Folheto = new HashSet<Folhetos>();
        }

        [Key]
        public int ID { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage = "Tens de preencher o campo {0} , dumb.")]
        public string Nome { get; set; }

        // definição do atributo que será utilizado para exprimir o relacionamento
        // com os objetos da classe Folhetos
        public virtual ICollection<Folhetos> Folheto { get; set; }
    }
}