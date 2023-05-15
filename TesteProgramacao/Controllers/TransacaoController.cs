using PagedList;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        private CategoriaRepository repositoryCategoria = new CategoriaRepository();
        private ContaRepository repositoryConta = new ContaRepository();

        // GET: Transacao
        public ActionResult Index(int? pagina, string sortOrder, DateTime? filtroInicio, DateTime? filtroFim, int paginaTamanho = 10)
        {
            int paginaNumero = (pagina ?? 1);
            
            if (paginaTamanho < 0)
            {
                TempData["MensagemErro"] = $"Ops, informe uma quantidade válida de registros";

                return RedirectToAction("Index");
            }

            ViewBag.CurrentFilter3 = paginaTamanho;

            var extrato = ExtratoTransacoes(null, pagina, sortOrder, filtroInicio, filtroFim, paginaTamanho);

            return View(extrato.ToPagedList(paginaNumero, paginaTamanho));
        }

        // GET: Transacao/ExtratoComId
        public ActionResult ExtratoComId(Guid id, int? pagina, string sortOrder, DateTime? filtroInicio, DateTime? filtroFim, int paginaTamanho = 10)
        {
            int paginaNumero = (pagina ?? 1);

            if (paginaTamanho < 0)
            {
                TempData["MensagemErro"] = $"Ops, informe uma quantidade válida de registros";

                return RedirectToAction("Index");
            }

            ViewBag.CurrentFilter3 = paginaTamanho;

            var extrato = ExtratoTransacoes(id, pagina, sortOrder, filtroInicio, filtroFim, paginaTamanho);

            return View(extrato.ToPagedList(paginaNumero, paginaTamanho));
        }

        // GET: Transacao/Create
        public ActionResult Create()
        {
            ViewBag.NomesCategorias = (from c in repositoryCategoria.GetAll()
             select c.Nome).Distinct();

            return View();
        }

        // POST: Transacao/Create
        [HttpPost]
        public ActionResult Create(Transacao transacao, string nomesCategorias, string contaCodigo)
        {
            ViewBag.NomesCategorias = (from c in repositoryCategoria.GetAll()
                                       select c.Nome).Distinct();

            var conta = repositoryConta.GetAll().Where(x => x.Codigo == contaCodigo).ToList().FirstOrDefault();
            var categoria = repositoryCategoria.GetAll().Where(x => x.Nome == nomesCategorias).ToList().FirstOrDefault();

            if (conta == null)
            {
                TempData["MensagemErro"] = $"Ops, o código da conta informado não é válido";

                return RedirectToAction("Create");
            }

            if (transacao.Valor.ToString().Contains("-"))
            {
                TempData["MensagemErro"] = $"Ops, informe um valor positivo! O sistema indicará se é débito ou crédito de acordo à Categoria.";

                return RedirectToAction("Create");
            }

            if (ModelState.IsValid)
            {
                transacao.Data = DateTime.Now;
                transacao.Conciliado = 0;
                transacao.ContaID = conta.Id;
                transacao.CategoriaID = categoria.Id;
                transacao.Credito = categoria.Tipo == 1 ? transacao.Valor : 0;
                transacao.Debito = categoria.Tipo == 0 ? transacao.Valor : 0;
                transacao.Historico = categoria.Nome;
                transacao.Notas = transacao.Notas != null ? transacao.Notas : "";

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

        public List<Transacao> ExtratoTransacoes(Guid? id, int? pagina, string sortOrder, DateTime? filtroInicio, DateTime? filtroFim, int paginaTamanho = 10)
        {
            var todasTransacoes = new List<Transacao>();
            var transacoesComFiltro = new List<Transacao>();

            ViewBag.DataParm = sortOrder == "data" ? "data_desc" : "data";
            ViewBag.HistoricoParm = sortOrder == "hist" ? "hist_desc" : "hist";
            ViewBag.CurrentFilter = filtroInicio;
            ViewBag.CurrentFilter2 = filtroFim;

            if (id == null)
            {
                switch (sortOrder)
                {
                    case "data":
                        todasTransacoes = repository.GetAll().OrderByDescending(s => s.Data).ToList();
                        break;
                    case "hist":
                        todasTransacoes = repository.GetAll().OrderByDescending(s => s.Historico).ToList();
                        break;
                    default:
                        todasTransacoes = repository.GetAll().OrderBy(s => s.Data).ToList();
                        break;
                }
            }
            else
            {
                switch (sortOrder)
                {
                    case "data":
                        todasTransacoes = repository.GetTransacaoByContaId(id.Value).OrderByDescending(s => s.Data).ToList();
                        break;
                    case "hist":
                        todasTransacoes = repository.GetTransacaoByContaId(id.Value).OrderByDescending(s => s.Historico).ToList();
                        break;
                    default:
                        todasTransacoes = repository.GetTransacaoByContaId(id.Value).OrderBy(s => s.Data).ToList();
                        break;
                }
            }

            if (filtroInicio != null && filtroFim != null)
            {
                transacoesComFiltro = todasTransacoes.Where(x => x.Data >= filtroInicio && x.Data <= filtroFim).ToList();
            }
            else if (filtroInicio != null)
            {
                transacoesComFiltro = todasTransacoes.Where(x => x.Data >= filtroInicio).ToList();
            }
            else if (filtroFim != null)
            {
                transacoesComFiltro = todasTransacoes.Where(x => x.Data <= filtroFim).ToList();
            }
            else
            {
                transacoesComFiltro = todasTransacoes;
            }

            ViewBag.Total = Convert.ToDouble(transacoesComFiltro.Sum(x => x.Credito) - transacoesComFiltro.Sum(x => x.Debito)).ToString("C2", CultureInfo.CurrentCulture);

            return transacoesComFiltro;
        }
    }
}