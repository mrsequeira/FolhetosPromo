using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcFolhetos.Models
{
    public class Tags
    {
        //Relação M-N
        public Tags(){
            ListaDeFolhetos = new HashSet<Folhetos>();
        }

        [Key]
        public int ID { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage = "Tens de preencher o campo {0} , dumb.")]
        public string Info { get; set; }

        // definição do atributo que será utilizado para exprimir o relacionamento
        // com os objetos da classe Folhetos
        public virtual ICollection<Folhetos> ListaDeFolhetos { get; set; }
    }

}