using AppLoginOswald.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppLoginOswald.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        Cliente objCliente = new Cliente();
        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }

        // GET: Cliente/Details/5
        public ActionResult Details(int id)
        {
            var cliente = new Cliente() { ClienteID = id };
           cliente = objCliente.SelectID(cliente);
            return View(cliente);
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        public ActionResult Create(Cliente cliente)
        {

            // TODO: Add insert logic here

            if (ModelState.IsValid)
            {
                objCliente.InsertCliente(cliente);
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Cliente/Edit/5
        public ActionResult Edit(int id)
        {
            var cliente = new Cliente() { ClienteID = id };
            cliente = objCliente.SelectID(cliente);
            return View(cliente);
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        public ActionResult Edit(Cliente cliente)
        {
            objCliente.Update(cliente);
            return RedirectToAction("Select");


        }

        // GET: Cliente/Delete/5
        public ActionResult Delete(int id)
        {
            var cliente = new Cliente() { ClienteID = id };
           
            cliente = objCliente.SelectID(cliente);
            return View(cliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmaDelete(int id)
        {
            var cliente = new Cliente() { ClienteID=id };
            objCliente.Delete(cliente);
                return RedirectToAction("Select");
            
        }
        [HttpGet]
        public ActionResult Select()
        {
            
            return View(objCliente.SelectCliente());
        }

        [HttpGet]
        public ActionResult EditBusca()
        {
          var clientes = objCliente.SelectCliente();
            SelectList lista = new SelectList(clientes, "ClienteID", "Nome");
            ViewBag.Lista = lista;
            return View();
        }
        [HttpPost]
        public ActionResult EditBusca(Cliente cliente)
        {
            int ID = cliente.ClienteID;
            return RedirectToAction("Edit","Cliente",new {id = ID});
        }

        [HttpGet]
        public ActionResult DeleteBusca()
        {
            var clientes = objCliente.SelectCliente();
            SelectList lista = new SelectList(clientes, "ClienteID", "Nome");
            ViewBag.Lista = lista;
            return View();
        }
        [HttpPost]
        public ActionResult DeleteBusca(Cliente cliente)
        {
            int ID = cliente.ClienteID;
            return RedirectToAction("Delete", "Cliente", new { id = ID });
        }

        public ActionResult CpfBusca(string Cpf)
        {
            bool CpfExists = false;
           string cpf = objCliente.BuscaCpfCliente(Cpf);
            if(cpf.Length == 0)
            {
                CpfExists = false;
            }
            else
            {
                CpfExists = true;
            }
            return Json(!CpfExists, JsonRequestBehavior.AllowGet);
        }
    }
}
