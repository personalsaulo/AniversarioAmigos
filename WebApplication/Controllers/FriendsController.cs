using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Domain;
using WebApplication.Models;
using WebApplication.Repository;
using WebApplication.Repository.Conexao;

namespace WebApplication.Controllers
{
    public class FriendsController : Controller
    {
        // GET: Friends
        private static FriendRepository repository = new FriendRepository();
        public ActionResult Index()
        {
            var listaOrdenada = repository.GetAllFriends()
                                .OrderByDescending(o => DateTime.Now.Subtract(
                                 new DateTime(DateTime.Now.Year, o.BirthDate.Month, o.BirthDate.Day))
                                 .TotalSeconds).ToList();

            return View(listaOrdenada);
        }

        public ViewResult Search(string name = "")
        {
            var lista = repository.GetOneFriendByName(name);
            return View(lista);
        }

        // GET: Friends/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Friends/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Friends/Create
        [HttpPost]
        public RedirectToRouteResult Create(FriendViewModel friend)
        {
            var repository = new FriendRepository();

            repository.InsertFriends(friend);
            return RedirectToAction("Index");
        }

        // GET: Friends/Edit/5
        public ActionResult Edit(int id)
        {
            return View(repository.GetOneFriendById(id));
        }

        // POST: Friends/Edit/5
        [HttpPost]
        public ActionResult Edit(FriendViewModel _friendViewModel, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                repository.UpdateAFriend(_friendViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Friends/Delete/5
        public ActionResult Delete(int id)
        {
            return View(repository.GetOneFriendById(id));
        }

        // POST: Friends/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                repository.DeleteAFriendById(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
