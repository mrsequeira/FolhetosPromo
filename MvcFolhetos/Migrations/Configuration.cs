namespace MvcFolhetos.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MvcFolhetos.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MvcFolhetos.Models.ApplicationDbContext >
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //ContextKey = "MvcFolhetos.Models.ApplicationDbContext ";
        }

        protected override void Seed(MvcFolhetos.Models.ApplicationDbContext  context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            // adiciona Users

            //*********************************************************************
            //var users = new List<Users> {
            //   new Users  {UserID = 1, Nome = "Miguel Dantas", Username = "MDantas", Password = "12345678", DataInsc = new DateTime(2017,03,31), DataNasc= new DateTime(1996,04,10), Tipo="Registado" },
            //   new Users  {UserID = 2, Nome = "Rúben Pimentel", Username = "RPimentel", Password = "12345678", DataInsc =  new DateTime(2016,01,05), DataNasc = new DateTime(1987,05,09), Tipo="Registado" },
            //   new Users  {UserID = 3, Nome = "Carlos Silva", Username = "CMSilva", Password = "12345678", DataInsc =  new DateTime(2016,01,01), DataNasc = new DateTime(1996,04,10), Tipo="Administrador" }
            //};

            //*********************************************************************
            // adiciona Tags
            var tags = new List<Tags> {
               new Tags {ID =1, Info ="Antevisão" },
               new Tags {ID =2, Info ="Dia da mãe" },
               new Tags {ID =3, Info ="Descontos" },
               new Tags {ID =4, Info ="Só hoje" },
               new Tags {ID =4, Info ="Pingo Salgado" }
            };
            tags.ForEach(tt => context.Tags.AddOrUpdate(t => t.ID, tt));
            context.SaveChanges();


            //*********************************************************************
            // adiciona Folhetos
            var folhetos = new List<Folhetos> {
               new Folhetos {
                   FolhetosID =1,
                   Titulo ="Antevisão Folheto PINGO DOCE Promoções de 1 a 7 maio ",
                   Descricao ="Só começa a 1 maio, mas como sempre em primeira mão no Blog!",
                   DataInic =new DateTime(2018,5,1),
                   DataFim =new DateTime(2018,5,7),
                   NomeEmpresa ="Pingo Doce",
                   ListaDeTags = new List<Tags>{ tags[0] }
               },
               new Folhetos {
                   FolhetosID =2,
                   Titulo ="Antevisão - 1 Maio PINGO DOCE só amanhã 25% em toda a loja",
                   Descricao ="Ora sempre se confirma o antecipado logo pela manhã de hoje AQUI, assim amanhã 1 maio teremos 25% de desconto em cartão Poupa Mais em toda loja Pingo Doce, o desconto acumula com as demais promoções, assim e de  forma a maximizar os descontos, consultem todos os folhetos abaixo, e o especial 1 maio, para verificarem as vossas necessidades e oportunidades, desejo um bom 1 de maio para todos, especialmente para os que neste dia feriado estão a trabalhar !",
                   DataInic =new DateTime(2018,5,1),
                   DataFim =new DateTime(2018,5,1),
                   NomeEmpresa ="PingoDoce",
                   ListaDeTags = new List<Tags>{ tags[0], tags[1], tags[2] }
               },
               new Folhetos {
                   FolhetosID =3,
                   Titulo ="Destaques e Só hoje! ",
                   Descricao ="Destaques e Só hoje! Continente especial 1 maio - só 1 maio,",
                   DataInic =new DateTime(2018,5,1),
                   DataFim =new DateTime(2018,5,1),
                   NomeEmpresa ="Continente",
                   ListaDeTags = new List<Tags>{ tags[3] }
               }
            };
            folhetos.ForEach(ff => context.Folhetos.AddOrUpdate(f => f.FolhetosID, ff));
            context.SaveChanges();

           
        }
    }
}
