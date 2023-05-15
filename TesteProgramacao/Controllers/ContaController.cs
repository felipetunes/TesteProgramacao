using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesteProgramacao.Models;
using TesteProgramacao.Repository;

namespace TesteProgramacao.Controllers
{
    public class ContaController : Controller
    {
        private ContaRepository repository = new ContaRepository();
        // GET: Conta
        public ActionResult Index(int? pagina, string sortOrder, string tipoString, string searchString, int paginaTamanho = 10)
        {
            int paginaNumero = (pagina ?? 1);
            var todasContas = new List<Conta>();
            var contasComFiltro = new List<Conta>();
            ViewBag.NomeParm = sortOrder == "nome" ? "nome_desc" : "nome";
            ViewBag.CodigoParm = sortOrder == "codigo" ? "codigo_desc" : "codigo";

            if(paginaTamanho < 0)
            {
                TempData["MensagemErro"] = $"Ops, informe uma quantidade válida de registros";
                return RedirectToAction("Index");
            }

            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentFilter2 = tipoString;
            ViewBag.CurrentFilter3 = paginaTamanho;

            switch (sortOrder)
            {
                case "nome":
                    todasContas = repository.GetAll().OrderByDescending(s => s.Nome).ToList();
                    break;
                case "nome_desc":
                    todasContas = repository.GetAll().OrderBy(s => s.Nome).ToList();
                    break;
                case "codigo":
                    todasContas = repository.GetAll().OrderByDescending(s => s.Codigo).ToList();
                    break;
                case "codigo_desc":
                    todasContas = repository.GetAll().OrderBy(s => s.Codigo).ToList();
                    break;
                default:
                    todasContas = repository.GetAll().ToList();
                    break;
            }

            contasComFiltro = todasContas;
            if (searchString != null)
            {
                if (tipoString == "Nome")
                {
                    contasComFiltro = todasContas.Where(x => x.Nome.Contains(searchString)).ToList();
                }
                if (tipoString == "Codigo")
                {
                    contasComFiltro = todasContas.Where(x => x.Codigo.Contains(searchString)).ToList();
                }
            }
            else
            {
                pagina = 1;
            }

            return View(contasComFiltro.ToPagedList(paginaNumero, paginaTamanho));
        }

        // GET: Conta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Conta/Create
        [HttpPost]
        public ActionResult Create(Conta conta)
        {
            if (ModelState.IsValid)
            {
                repository.Save(conta);
                return RedirectToAction("Index");
            }
            else
            {
                return View(conta);
            }
        }

        // GET: Conta/Edit/5
        public ActionResult Edit(Guid id)
        {
            var conta = repository.GetById(id);

            if (conta == null)
            {
                return HttpNotFound();
            }

            return View(conta);
        }

        // POST: Conta/Edit/5
        [HttpPost]
        public ActionResult Edit(Conta conta)
        {
            if (ModelState.IsValid)
            {
                repository.Update(conta);
                return RedirectToAction("Index");
            }
            else
            {
                return View(conta);
            }
        }

        public ActionResult Delete(Guid id)
        {
            repository.DeleteById(id);
            return Json(repository.GetAll());
        }

        public ActionResult ApagarConfirmacao(Guid id)
        {
            Conta conta = repository.GetById(id);
            return View(conta);
        }

        public ActionResult Apagar(Guid id)
        {
            try
            {
                repository.DeleteById(id);

                //if (apagado)
                //{
                //    TempData["MensagemSucesso"] = "Conta apagado com sucesso!";
                //}
                //else
                //{
                //    TempData["MensagemErro"] = "Ops, não foi possível apagar a conta";
                //}

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos apagar sua conta, mais detalhes do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }

}