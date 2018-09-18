using System;
using System.Data.Entity;

namespace MvcFolhetos.Models
{
    public class Folhetos
    {

        public int FolhetosID { get; set; }
        
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Pasta { get; set; }
        
        public DateTime DataInic { get; set; }
        
        public DateTime DataFim { get; set; }
        
        public string NomeEmpresa { get; set; }
    }

    public class FolhetosDBContext : DbContext
    {
        public DbSet<Folhetos> Folhetos { get; set; }
    }
}