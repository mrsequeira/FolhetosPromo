using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MvcFolhetos.Models
{
    public class Folhetos
    {
        //Relação M-N através de listas
        //uma 'lista' de objetos de uma das classes na outra classe,
        // e vice-versa.
        public Folhetos(){
            ListaDeTags = new HashSet<Tags>();
            ListaDeCategorias = new HashSet<Categorias>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FolhetosID { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Tens de preencher o campo {0} , dumb.")]
        public string Titulo { get; set; }

        public string Descricao { get; set; }

        //[FileExtensions(FileTypes = new string[] { "image/png", "image/jpeg", "image/jpg", "image/gif" })]
        //public IEnumerable<HttpPostedFileBase> Files { get; set; }

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



        public virtual ICollection<Tags> ListaDeTags { get; set; }
        public virtual ICollection<Categorias> ListaDeCategorias { get; set; }
    }
    //Database context
    public class ApplicationDbContext  : DbContext
    {

        public virtual DbSet<Folhetos> Folhetos { get; set; }
        public virtual DbSet<Categorias> Categorias { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<Utilizadores> Utilizadores { get; set; }
       
        /// <summary>
        /// Usa a sequência definida em <see cref="Multas_tA.Migrations.SequenciaIdAgentes"/>
        /// para obter, de forma atómica, o ID de um agente.
        /// </summary>
        /// <returns>O próximo ID do agente.</returns>
        public int GetIdFolheto()
        {
            // Um objeto que derive da classe "DbContext" (como o MultasDb)
            // permite que seja executado SQL "raw", como no exemplo abaixo.
            return this.Database
                // <int> define o tipo de dados. Pode ser uma classe, os valores dos campos
                // do SELECT serão copiados para o objeto.
                .SqlQuery<int>("Select Next Value For [dbo].[SeqIdFolheto]")
                // Single() é um operador do Linq. 
                // Uso este porque só me interessa a primeira (e única) linha.
                // Usaria ToList() se existissem várias, e First()/Last() se só quisesse
                // a primera/última linha de muitas.
                .Single();
        }
        /// <summary>
        /// configura a forma como as tabelas são criadas
        /// </summary>
        /// <param name="modelBuilder"> objeto que referencia o gerador de base de dados </param>      
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }

    }
}