using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using IdentitySample.Models;
using MvcFolhetos.Models;

namespace MvcFolhetos.Controllers
{
    public class FolhetosController : Controller
    {
        private ApplicationDbContext  db = new ApplicationDbContext ();

        #region CRUD - READ folhetos

        // GET: Folhetos
        // um user anónimo consegue aceder
        [AllowAnonymous]
        public ActionResult Index(string searchString)
        {
            //LINQ query
            var folhetos = from m in db.Folhetos
                         select m;
            //Caso a searchbox tiver algo é filtrado o conteudo a mostrar
            if (!String.IsNullOrEmpty(searchString))
            {
                folhetos = folhetos.Where(s => s.NomeEmpresa.Contains(searchString));
            }

            return View(folhetos);


            //return View(db.Folhetos.ToList());
        }
        [AllowAnonymous]
        // GET: Folhetos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Folhetos folhetos = db.Folhetos.Find(id);
            if (folhetos == null)
            {
                return HttpNotFound();
            }
            
            var path = HttpContext.Server.MapPath("~/imagens/folheto" + id);
            //var path = "~/imagens/folheto" + id ;
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
            int count = dir.GetFiles().Length;
            
            //int fCount = Directory.GetFiles(path).Length;
            
            // gerar a lista de objetos de Tags que podem ser associados aos folhetos
            ViewBag.ListaDeTags = db.Tags.OrderBy(b => b.Info).ToList();
            ViewBag.nPaginas = count;
            ViewBag.id = id;
            return View(folhetos);
        }

        #endregion

        #region CRUD - CREAT folhetos

        [Authorize(Roles = "GestaoDeFolhetos")]
        // GET: Folhetos/Create
        public ActionResult Create()
        {
            // gerar a lista de tags que podem ser escolhidos
            ViewBag.ListaDeTags = db.Tags.OrderBy(b => b.Info).ToList();

            return View();
        }

        //[Authorize(Roles = "GestaoDeFolhetos")]
        // POST: Folhetos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FolhetosID,Titulo,Descricao,Pasta,DataInic,DataFim,NomeEmpresa")] Folhetos folhetos, IEnumerable<HttpPostedFileBase> files,  string[] opcoesEscolhidasDeTags )
        {
            // ModelState.IsValid --> confronta os dados fornecidos com o modelo
            // se não respeitar as regras do modelo, rejeita os dados
            if (!ModelState.IsValid)
            {
                return View(folhetos);
            }
            /// avalia se o array com a lista das escolhas de objetos de B associados ao objeto do tipo A 
            /// é nula, ou não.
            /// Só poderá avanção se NÃO for nula
            if (opcoesEscolhidasDeTags == null)
            {
                ModelState.AddModelError("", "Necessita escolher pelo menos um valor de B para associar ao seu objeto de A.");
                // gerar a lista de objetos de B que podem ser associados a A
                ViewBag.ListaDeTags = db.Tags.OrderBy(b => b.Info).ToList();
                // devolver controlo à View
                return View(folhetos);
            }

            //// **************** Guardar páginas dos folhetos ****************
            //  [DatabaseGenerated(DatabaseGeneratedOption.Identity)] -> Efetua criação do ID através do SEQUENCE
            //var maxIdFolheto = db.Folhetos.Max(x => x.FolhetosID) + 1;
            ////Criar pasta para poderem ser guardadas as fotografias do folheto
            //var path = "C:\\Users\\AlexandredosSantosSe\\dev\\MvcFolhetos\\MvcFolhetos\\imagens";
            var newfolheto = new Folhetos {
                FolhetosID = db.GetIdFolheto(),
                Titulo = folhetos.Titulo,
                Descricao = folhetos.Descricao,
                DataInic = folhetos.DataInic,
                DataFim = folhetos.DataFim,
                NomeEmpresa = folhetos.NomeEmpresa
            };
            string subPath = "";
            subPath = HttpContext.Server.MapPath("~/imagens/folheto" + newfolheto.FolhetosID);
            System.IO.Directory.CreateDirectory(subPath);
     
            ////Percorrer todas as imagens recebidas
            ////Efetua validaçãp
            int i = 1;
            foreach (var imagens in files)
            {
                //Alterar estes dois ifs para um unico só (Deixar para debugging apenas)
                if (imagens != null)
                {
                    if (imagens.ContentLength > 0)
                    {
                        //depois colocar nomes especificos, mas por enquanto guardar fotos
                        if (Path.GetExtension(imagens.FileName).ToLower() == ".jpeg")
                        {
                            //string imgDestino = Path.Combine(pathString, imagens.FileName);
                            string filename = "pagina" + i+ ".jpeg";
                            imagens.SaveAs(HttpContext.Server.MapPath("~/imagens/folheto" + newfolheto.FolhetosID + "/" ) + filename);
                            i++;
                            ViewBag.UploadSuccess = true;
                        }
                    }
                }
            }

            // **************** Guardar tags associadas ao folheto ****************
            // criar uma lista com os objetos escolhidos de B

            List<Tags> listaDeObjetosDeTagsEscolhidos = new List<Tags>();
            foreach (string item in opcoesEscolhidasDeTags)
            {
                //procurar o objeto de B
                Tags b = db.Tags.Find(Convert.ToInt32(item));
                // adicioná-lo à lista
                listaDeObjetosDeTagsEscolhidos.Add(b);
            }

            // adicionar a lista ao objeto de A
            newfolheto.ListaDeTags = listaDeObjetosDeTagsEscolhidos;
            //newfolheto.FolhetosID = newID;
            if (ModelState.IsValid)
            {
                db.Folhetos.Add(newfolheto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(folhetos);
        }

        #endregion

        #region CRUD - Update folhetos

        [Authorize(Roles = "GestaoDeFolhetos")]
        // GET: Folhetos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Folhetos folhetos = db.Folhetos.Find(id);
            if (folhetos == null)
            {
                return HttpNotFound();
            }
            var path = HttpContext.Server.MapPath("~/imagens/folheto" + id); 
            //var path = "~/imagens/folheto" + id ;
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
            int count = dir.GetFiles().Length;
            ViewBag.nPaginas = count;
            // gerar a lista de objetos de B que podem ser associados a A
            ViewBag.ListaDeTags = db.Tags.OrderBy(b => b.Info).ToList();
            ViewBag.id = id;
            return View(folhetos);
        }

        [Authorize(Roles = "GestaoDeFolhetos")]
        // POST: Folhetos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FolhetosID,Titulo,Descricao,Pasta,DataInic,DataFim,NomeEmpresa")] Folhetos folhetos, IEnumerable<HttpPostedFileBase> files, string[] opcoesEscolhidasDeTags)
        {
            // ler da BD o objeto que se pretende editar
            var aa = db.Folhetos.Include(b => b.ListaDeTags).Where(b => b.FolhetosID == folhetos.FolhetosID).SingleOrDefault();

            //Por equanto apenas estes 3 podem ser alterados
            if (ModelState.IsValid)
            {
                aa.Titulo = folhetos.Titulo;
                aa.Descricao = folhetos.Descricao;
                aa.NomeEmpresa = folhetos.NomeEmpresa;
                aa.DataInic = folhetos.DataInic;
                aa.DataFim = folhetos.DataFim;
            }
            else
            {
                // gerar a lista de objetos de B que podem ser associados a A
                ViewBag.ListaDeTags = db.Tags.OrderBy(b => b.Info).ToList();
                // devolver o controlo à View
                return View(folhetos);
            }

            // tentar fazer o UPDATE
            if (TryUpdateModel(aa, "", new string[] { nameof(aa.Titulo), nameof(aa.NomeEmpresa), nameof(aa.ListaDeTags) }))
            {
                // obter a lista de elementos de B
                var elementosDeTags = db.Tags.ToList();

                if (opcoesEscolhidasDeTags != null)
                {
                    // se existirem opções escolhidas, vamos associá-las
                    foreach (var bb in elementosDeTags)
                    {
                        if (opcoesEscolhidasDeTags.Contains(bb.ID.ToString()))
                        {
                            // se uma opção escolhida ainda não está associada, cria-se a associação
                            if (!aa.ListaDeTags.Contains(bb))
                            {
                                aa.ListaDeTags.Add(bb);
                            }
                        }
                        else
                        {
                            // caso exista associação para uma opção que não foi escolhida, 
                            // remove-se essa associação
                            aa.ListaDeTags.Remove(bb);
                        }
                    }
                }
                else
                {
                    // não existem opções escolhidas!
                    // vamos eliminar todas as associações
                    foreach (var bb in elementosDeTags)
                    {
                        if (aa.ListaDeTags.Contains(bb))
                        {
                            aa.ListaDeTags.Remove(bb);
                        }
                    }
                }
                // guardar as alterações
                db.SaveChanges();

                // devolver controlo à View
                return RedirectToAction("Index");
            }

            // se cheguei aqui, é pq alguma coisa correu mal
            ModelState.AddModelError("", "Alguma coisa correu mal...");

            // gerar a lista de objetos de B que podem ser associados a A
            ViewBag.ListaDeTags = db.Tags.OrderBy(b => b.Info).ToList();

            // visualizar View...
            return View(folhetos);
        }

        #endregion

        #region CRUD - Delete folhetos

        [Authorize(Roles = "GestaoDeFolhetos")]
        // GET: Folhetos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Folhetos folhetos = db.Folhetos.Find(id);
            if (folhetos == null)
            {
                return HttpNotFound();
            }

            ViewBag.ListaDeTags = db.Tags.OrderBy(b => b.Info).ToList();

            return View(folhetos);
        }


        //https://stackoverflow.com/questions/17723626/entity-framework-remove-vs-deleteobject
        //[Authorize(Roles = "GestaoDeFolhetos")]
        // POST: Folhetos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id , string[] opcoesEscolhidasDeTags)
        {
            Folhetos folhetos = db.Folhetos.Find(id);

            List<Tags> listaDeObjetosDeTagsEscolhidos = new List<Tags>();

            var aa = db.Folhetos.Include(b => b.ListaDeTags).Where(b => b.FolhetosID == folhetos.FolhetosID).SingleOrDefault();
            System.Diagnostics.Debug.WriteLine(aa);
            string subPath = "";
            subPath = HttpContext.Server.MapPath("~/imagens/folheto" + id);
            System.IO.Directory.Delete(subPath, true);

            try {
                //db.Folhetos.Remove(aa);
                db.Folhetos.Remove(folhetos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch {
                // gerar uma mensagem de erro, a ser apresentada ao utilizador
                ModelState.AddModelError("",
                           string.Format("Não foi possível remover o folheto porque existem tags: '{0}' e categorias: {1}  associadas a ele.",
                                          folhetos.ListaDeTags, folhetos.ListaDeCategorias));
            }
            // reenviar os dados para a View
            return View(folhetos);

        }

        #endregion


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
