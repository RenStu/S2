using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace MvcService.Controllers
{
    [Authorize]
    public class GameController : BaseController
    {

        private string friendEndPoint;
        private string gameEndPoint;

        public GameController()
        {
            friendEndPoint = $"{GetEndPoint("fabric:/S2Core11/FriendService")}/api/Friends";
            gameEndPoint = $"{GetEndPoint("fabric:/S2Core11/GameService")}/api/Games";
        }


        // GET: Game
        public IActionResult Index()
        {
            List<Game> list = ContentToObject<List<Game>>(
                GetAsync(gameEndPoint));

            return View(list);
        }

        // GET: Friend/Devolve/5
        public IActionResult Devolve(int id)
        {
            var game = ContentToObject<Game>(
                    GetAsync($"{gameEndPoint}/{id}"));

            PutAsync($"{gameEndPoint}/devolve/{id}", ObjectToContent(game));

            return RedirectToAction("Index");
        }

        // GET: Game/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Game/Create
        [HttpPost]
        public IActionResult Create(Game game)
        {
            try
            {
                PostAsync(gameEndPoint, ObjectToContent(game));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Game/Edit/5
        public IActionResult Edit(int id)
        {
            var game = ContentToObject<Game>(
                GetAsync($"{gameEndPoint}/{id}"));

            return View(game);
        }

        // POST: Game/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, Game game)
        {
            try
            {
                PutAsync($"{gameEndPoint}/{id}", ObjectToContent(game));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Game/Delete/5
        public IActionResult Delete(int id)
        {
            DeleteAsync($"{gameEndPoint}/{id}");

            return RedirectToAction("Index");
        }

    }
}
