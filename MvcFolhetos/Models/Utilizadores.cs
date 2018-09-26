using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcFolhetos.Models
{ //: IdentityUser
    public class Utilizadores 
    {
        [Key]
        public int ID { get; set; }


        public string NomeProprio { get; set; }


        public string Apelido { get; set; }


        //*********************************************************
        // o atributo seguinte vai criar uma chave forasteira
        // para a tabela da 'autenticação'
        //*********************************************************
        public string UserName { get; set; }
    }
}