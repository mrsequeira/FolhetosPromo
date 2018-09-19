using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcFolhetos.Models;

namespace MvcFolhetos.Controllers
{
    public class FolhetosController : Controller
    {
        private FolhetosDBContext db = new FolhetosDBContext();

        // GET: Folhetos
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

            // gerar a lista de objetos de Tags que podem ser associados aos folhetos
            ViewBag.ListaDeTags = db.Tags.OrderBy(b => b.Info).ToList();

            return View(folhetos);
        }

        // GET: Folhetos/Create
        public ActionResult Create()
        {
            // gerar a lista de objetos de B que podem ser associados a A
            ViewBag.ListaDeTags = db.Tags.OrderBy(b => b.Info).ToList();

            return View();
        }

        // POST: Folhetos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FolhetosID,Titulo,Descricao,Pasta,DataInic,DataFim,NomeEmpresa")] Folhetos folhetos, string[] opcoesEscolhidasDeTags)
        {

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
            folhetos.ListaDeTags = listaDeObjetosDeTagsEscolhidos;  

            if (ModelState.IsValid)
            {
                db.Folhetos.Add(folhetos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(folhetos);
        }

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

            // gerar a lista de objetos de B que podem ser associados a A
            ViewBag.ListaDeTags = db.Tags.OrderBy(b => b.Info).ToList();

            return View(folhetos);
        }

        // POST: Folhetos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FolhetosID,Titulo,Descricao,Pasta,DataInic,DataFim,NomeEmpresa")] Folhetos folhetos, string[] opcoesEscolhidasDeTags)
        {
            // ler da BD o objeto que se pretende editar
            var aa = db.Folhetos.Include(b => b.ListaDeTags).Where(b => b.FolhetosID == folhetos.FolhetosID).SingleOrDefault();

            //Por equanto apenas estes 3 podem ser alterados
            if (ModelState.IsValid){
                aa.Titulo = folhetos.Titulo;
                aa.Descricao = folhetos.Descricao;
                aa.NomeEmpresa = folhetos.NomeEmpresa;
            }
            else {
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
            return View(folhetos);
        }

        // POST: Folhetos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Folhetos folhetos = db.Folhetos.Find(id);
            db.Folhetos.Remove(folhetos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
