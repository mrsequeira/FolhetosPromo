using MvcFolhetos.Models;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Owin;
using IdentitySample.Models;

namespace IdentitySample
{
    public partial class Startup
    {
        /// <summary>
        /// este método é executado apenas 1 (uma) vez
        /// quando a aplicação arranca
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            // invocar o método para iniciar a configuração
            // dos ROLES
            //IniciaAplicacao();
        }

        /// <summary>
        /// cria, caso não existam, as Roles de suporte à aplicação: GestaoDeFolhetos
        /// cria também um utilizador e efetuada a associação à role
        /// </summary>
        //private void IniciaAplicacao()
        //{

        //    FolhetosDBContext db = new FolhetosDBContext();

        //    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
        //    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));


        //    // criar a Role 'GestaoDePessoal'
        //    if (!roleManager.RoleExists("GestaoDeFolhetos"))
        //    {
        //        // não existe a 'role'
        //        // então, criar essa role
        //        var role = new IdentityRole();
        //        role.Name = "GestaoDeFolhetos";
        //        roleManager.Create(role);
        //    }

        //    // criar um utilizador 'Agente'
        //    var user = new ApplicationUser();
        //    user.UserName = "alex@mail.pt";
        //    user.Email = "alex@mail.pt";
        //    //  user.Nome = "Luís Freitas";
        //    string userPWD = "123_Asd";
        //    var chkUser = userManager.Create(user, userPWD);

        //    //Adicionar o Utilizador à respetiva Role-Agente-
        //    if (chkUser.Succeeded){
        //        var result1 = userManager.AddToRole(user.Id, "GestaoDeFolhetos");
        //    }
        //}

        // https://code.msdn.microsoft.com/ASPNET-MVC-5-Security-And-44cbdb97



    }
}
