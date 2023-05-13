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
        public ActionResult Index(int? pagina, string sortOrder)
        {
            int paginaTamanho = 8;
            int paginaNumero = (pagina ?? 1);
            ViewBag.NomeParm = sortOrder == "nome" ? "nome_desc" : "nome";
            ViewBag.CodigoParm = sortOrder == "codigo" ? "codigo_desc" : "codigo";
            var todasContas = new List<Conta>();

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

            return View(todasContas.ToPagedList(paginaNumero, paginaTamanho));
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

        // POST: Conta/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            repository.DeleteById(id);
            return Json(repository.GetAll());
        }
    }

}