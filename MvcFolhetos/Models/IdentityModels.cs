using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MvcFolhetos.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentitySample.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    /// <summary>
    /// classe para efetuar a geração de utilizadores
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public string NomeProprio { get; set; }
        public string Apelido { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ApplicationDbContext", throwIfV1Schema: false)
        { }

        static ApplicationDbContext()
        {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        // identificar as tabelas da base de dados
        public virtual DbSet<Folhetos> Folhetos { get; set; }
        public virtual DbSet<Categorias> Categorias { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<Utilizadores> Utilizadores { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();  // impede a EF de 'pluralizar' os nomes das tabelas
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();  // força a que a chave forasteira não tenha a propriedade 'on delete cascade'
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();  // força a que a chave forasteira não tenha a propriedade 'on delete cascade'

            base.OnModelCreating(modelBuilder);
        }

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

    }
}