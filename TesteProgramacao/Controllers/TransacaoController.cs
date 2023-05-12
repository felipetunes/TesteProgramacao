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
        public ActionResult Index(Guid? id)
        {
            if (id == null)
            {
                return View(repository.GetAll());
            }
            else
            {
                return View(repository.GetTransacaoByContaId(id.Value));
            }
        }


        // GET: Transacao/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Transacao/Create
        [HttpPost]
        public ActionResult Create(Transacao conta)
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