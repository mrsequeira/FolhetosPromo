namespace MvcFolhetos.Migrations
{
    using MvcFolhetos.Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public partial class Sequencefolhetos : DbMigration
    {
        // Adicionar uma sequência para obter IDs dos folhetos de forma
        // atómica.
        public override void Up()
        {
            int maxIdFolheto = 0;

            // Obter o último ID dos agentes, caso já existam agentes na BD...
            // Aqui é seguro fazer isto, a não ser que a BD esteja a ser usada
            // por outra aplicação.
            using (var db = new ApplicationDbContext())
            {
                if (db.Folhetos.Any())
                {
                    maxIdFolheto = db.Folhetos.Max(x => x.FolhetosID) + 1;
                }
                else
                {
                    throw new Exception("Não foram encontrados Folhetos...\n\nPor favor, comenta o código todo deste ficheiro (está na pasta das Migrations, Ctrl-A seguido de Ctrl-K e Ctrl-C), faz Update-Database, descomenta o código, e faz Update-Database novamente.\n\nSorry! :(\n\n-André Carvalho");
                }
            }

            // Sequências são uma forma atómica de obter números a partir de uma BD.
            // https://docs.microsoft.com/en-us/sql/t-sql/statements/create-sequence-transact-sql?view=sql-server-2017

            // ATENÇÃO: Só estou a fazer concatenação porque T-SQL (SQL do SQL Server) 
            // não suporta parameters com comandos DDL!
            // NUNCA se deve fazer concatenação de strings com variáveis
            // quando se quer fazer uma query SQL, especialmente se os valores são user-provided!!!!
            Sql(@"Create Sequence [dbo].[SeqIdFolheto] As Int Start With " + maxIdFolheto + ";");
        }

        // Se se fizer rollback a esta migração, apagar a sequência criada no upgrade (Up)
        public override void Down()
        {
            Sql("Drop Sequence [dbo].[SeqIdFolheto]");
        }
    }
}
