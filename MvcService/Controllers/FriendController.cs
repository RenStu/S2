
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcService.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;


namespace MvcService.Controllers
{
    [Authorize]
    public class FriendController : BaseController
    {
        private string friendEndPoint;
        private string gameEndPoint;

        public FriendController()
        {
            friendEndPoint = $"{GetEndPoint("fabric:/S2Core11/FriendService")}/api/Friends";
            gameEndPoint = $"{GetEndPoint("fabric:/S2Core11/GameService")}/api/Games";
        }

        // GET: Friend
        public IActionResult Index()
        {
            List<Friend> list = ContentToObject<List<Friend>>(
                GetAsync(friendEndPoint));

            return View(list);
        }

        // GET: Friend/Games/5
        public IActionResult Games(int id)
        {
            var friend = ContentToObject<Friend>(
                GetAsync($"{friendEndPoint}/{id}"));

            friend.Games = ContentToObject<List<Game>>(
                GetAsync($"{gameEndPoint}/byfriend/{friend.Id}"));

            return View(friend.Games);
        }

        // GET: Friend/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Friend/Create
        [HttpPost]
        public IActionResult Create(Friend friend)
        {
            try
            {
                PostAsync(friendEndPoint, ObjectToContent(friend));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Friend/Edit/5
        public IActionResult Edit(int id)
        {
            var friend = ContentToObject<Friend>(
                GetAsync($"{friendEndPoint}/{id}"));

            return View(friend);
        }

        // POST: Friend/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, Friend friend)
        {
            try
            {
                PutAsync($"{friendEndPoint}/{id}", ObjectToContent(friend));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Friend/Delete/5
        public ActionResult Delete(int id)
        {
            DeleteAsync($"{friendEndPoint}/{id}");

            return RedirectToAction("Index");
        }

        
    }
}
