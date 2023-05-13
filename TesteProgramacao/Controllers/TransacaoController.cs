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
    public class TransacaoController : Controller
    {
        private TransacaoRepository repository = new TransacaoRepository();
        // GET: Transacao
        public ActionResult Index(Guid? id, int? pagina, string sortOrder, DateTime? filtroInicio, DateTime? filtroFim, string currentFilter, string currentFilter2, int? page)
        {
            int paginaTamanho = 10;
            int paginaNumero = (pagina ?? 1);
            ViewBag.DataParm = sortOrder == "data" ? "data_desc" : "data";
            ViewBag.HistoricoParm = sortOrder == "hist" ? "hist_desc" : "hist";
            var todasTransacoes = new List<Transacao>();
            var transacoesComFiltro = new List<Transacao>();

            if (id == null)
            {
                switch (sortOrder)
                {
                    case "data":
                        todasTransacoes = repository.GetAll().OrderBy(s => s.Data).ToList();
                        break;
                    case "data_desc":
                        todasTransacoes = repository.GetAll().OrderByDescending(s => s.Data).ToList();
                        break;
                    case "hist":
                        todasTransacoes = repository.GetAll().OrderByDescending(s => s.Historico).ToList();
                        break;
                    case "hist_desc":
                        todasTransacoes = repository.GetAll().ToList();
                        break;
                    default:
                        todasTransacoes = repository.GetAll().ToList();
                        break;
                }
            }
            else
            {
                switch (sortOrder)
                {
                    case "data":
                        todasTransacoes = repository.GetTransacaoByContaId(id.Value).OrderBy(s => s.Data).ToList();
                        break;
                    case "data_desc":
                        todasTransacoes = repository.GetTransacaoByContaId(id.Value).OrderByDescending(s => s.Data).ToList();
                        break;
                    case "hist":
                        todasTransacoes = repository.GetTransacaoByContaId(id.Value).OrderByDescending(s => s.Historico).ToList();
                        break;
                    case "hist_desc":
                        todasTransacoes = repository.GetTransacaoByContaId(id.Value).ToList();
                        break;
                    default:
                        todasTransacoes = repository.GetTransacaoByContaId(id.Value).ToList();
                        break;
                }
            }

            if (filtroInicio != null && filtroFim != null)
            {
                transacoesComFiltro = todasTransacoes.Where(x => x.Data >= filtroInicio && x.Data <= filtroFim).ToList();
            }
            else
            {
                transacoesComFiltro = todasTransacoes;
            }

            decimal debitos = transacoesComFiltro.Sum(x => x.Debito);
            decimal creditos = transacoesComFiltro.Sum(x => x.Credito);
            ViewBag.Total = creditos - debitos;

            return View(transacoesComFiltro.ToPagedList(paginaNumero, paginaTamanho));

        }


        // GET: Transacao/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Transacao/Create
        [HttpPost]
        public ActionResult Create(Transacao transacao)
        {
            if (ModelState.IsValid)
            {
                repository.Save(transacao);
                return RedirectToAction("Index");
            }
            else
            {
                return View(transacao);
            }
        }

        // GET: Transacao/Edit/5
        public ActionResult Edit(Guid id)
        {
            var transacao = repository.GetById(id);

            if (transacao == null)
            {
                return HttpNotFound();
            }

            return View(transacao);
        }

        // POST: Transacao/Edit/5
        [HttpPost]
        public ActionResult Edit(Transacao transacao)
        {
            if (ModelState.IsValid)
            {
                repository.Update(transacao);
                return RedirectToAction("Index");
            }
            else
            {
                return View(transacao);
            }
        }

        // POST: Transacao/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            repository.DeleteById(id);
            return Json(repository.GetAll());
        }
    }
}