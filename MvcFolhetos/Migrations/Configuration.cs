namespace MvcFolhetos.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MvcFolhetos.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MvcFolhetos.Models.FolhetosDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "MvcFolhetos.Models.FolhetosDBContext";
        }

        protected override void Seed(MvcFolhetos.Models.FolhetosDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            // adiciona Folhetos
            var folhetos = new List<Folhetos> {
               new Folhetos {
                   FolhetosID =1,
                   Titulo ="Antevis�o Folheto PINGO DOCE Promo��es de 1 a 7 maio ",
                   Descricao ="S� come�a a 1 maio, mas como sempre em primeira m�o no Blog!",
                   Pasta ="folheto1",
                   DataInic =new DateTime(2018,5,1),
                   DataFim =new DateTime(2018,5,7),
                   NomeEmpresa ="PingoDoce"
               },
               new Folhetos {
                   FolhetosID =2,
                   Titulo ="Antevis�o - 1 Maio PINGO DOCE s� amanh� 25% em toda a loja",
                   Descricao ="Ora sempre se confirma o antecipado logo pela manh� de hoje AQUI, assim amanh� 1 maio teremos 25% de desconto em cart�o Poupa Mais em toda loja Pingo Doce, o desconto acumula com as demais promo��es, assim e de  forma a maximizar os descontos, consultem todos os folhetos abaixo, e o especial 1 maio, para verificarem as vossas necessidades e oportunidades, desejo um bom 1 de maio para todos, especialmente para os que neste dia feriado est�o a trabalhar !",
                   Pasta ="folheto2",
                   DataInic =new DateTime(2018,5,1),
                   DataFim =new DateTime(2018,5,1),
                   NomeEmpresa ="PingoDoce"
               },
               new Folhetos {
                   FolhetosID =3,
                   Titulo ="Destaques e S� hoje! ",
                   Descricao ="Destaques e S� hoje! Continente especial 1 maio - s� 1 maio,",
                   Pasta ="folheto3",
                   DataInic =new DateTime(2018,5,1),
                   DataFim =new DateTime(2018,5,1),
                   NomeEmpresa ="Continente"
               },
            };
            folhetos.ForEach(ff => context.Folhetos.AddOrUpdate(f => f.Titulo, ff));
            context.SaveChanges();


        }
    }
}
