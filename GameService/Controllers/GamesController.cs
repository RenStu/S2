using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GameService.Data;
using GameService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameService.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Games")]
    public class GamesController : Controller
    {
        private readonly GameContext _context;

        public GamesController(GameContext context)
        {
            _context = context;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<IEnumerable<Game>> Get()
        {
            return await _context.Games.ToListAsync();
        }

        // GET: api/Games/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<Game> Get(int id)
        {
            return await _context.Games.SingleOrDefaultAsync(f => f.Id == id);
        }


        // GET: api/Games/byfriend/5
        [HttpGet("byfriend/{friendid:int}", Name = "ByFriend")]
        public async Task<IEnumerable<Game>> ByFriend(int friendid)
        {
            return await _context.Games.Where(f => f.FriendId == friendid).ToListAsync();
        }

        // POST: api/Games
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody]Game Game)
        {
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                if (Game.IsValid)
                {
                    _context.Add(Game);
                    var result = _context.SaveChangesAsync().Result;

                    if (result != 0)
                        return new HttpResponseMessage(HttpStatusCode.OK);
                }

                return new HttpResponseMessage(HttpStatusCode.BadRequest);

            });
        }

        // PUT: api/Games/5
        [HttpPut("{id}")]
        public async Task<HttpResponseMessage> Put(int id, [FromBody]Game Game)
        {

            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                if (Game.IsValid)
                {
                    var GameToUpdate = _context.Games.SingleOrDefaultAsync(f => f.Id == id).Result;
                    GameToUpdate.Update(Game);
                    var result = _context.SaveChangesAsync().Result;

                    if (result != 0)
                        return new HttpResponseMessage(HttpStatusCode.OK);
                }

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            });
        }

        // PUT: api/Games/devolve/5
        [HttpPut("devolve/{id:int}", Name = "Devolve")]
        public async Task<HttpResponseMessage> Devolve(int id, [FromBody]Game Game)
        {

            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                if (Game.IsValid)
                {
                    var GameToUpdate = _context.Games.SingleOrDefaultAsync(f => f.Id == id).Result;
                    GameToUpdate.FriendId = (int?)null;
                    var result = _context.SaveChangesAsync().Result;

                    if (result != 0)
                        return new HttpResponseMessage(HttpStatusCode.OK);
                }

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            });
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                var GameToRemove = _context.Games.SingleOrDefaultAsync(f => f.Id == id).Result;
                _context.Games.Remove(GameToRemove);
                var result = _context.SaveChangesAsync().Result;

                if (result != 0)
                    return new HttpResponseMessage(HttpStatusCode.OK);

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            });

            
        }
    }
}
