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
        private ContaRepository respository = new ContaRepository();
        // GET: Conta
        public ActionResult Index()
        {
            return View(respository.GetAll());
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
                respository.Save(conta);
                return RedirectToAction("Index");
            }
            else
            {
                return View(conta);
            }
        }

        // GET: Conta/Edit/5
        public ActionResult Edit(int id)
        {
            var conta = respository.GetById(id);

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
                respository.Update(conta);
                return RedirectToAction("Index");
            }
            else
            {
                return View(conta);
            }
        }

        // POST: Conta/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            respository.DeleteById(id);
            return Json(respository.GetAll());
        }
    }

}