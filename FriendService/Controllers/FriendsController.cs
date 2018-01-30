using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FriendService.Data;
using FriendService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FriendService.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Friends")]
    public class FriendsController : Controller
    {
        private readonly FriendContext _context;

        public FriendsController(FriendContext context)
        {
            _context = context;
        }

        // GET: api/Friends
        [HttpGet]
        public async Task<IEnumerable<Friend>> Get()
        {
            return await _context.Friends.ToListAsync();
        }

        // GET: api/Friends/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<Friend> Get(int id)
        {
            return await _context.Friends.SingleOrDefaultAsync(f => f.Id == id);
        }

        // POST: api/Friends
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody]Friend friend)
        {
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                if (friend.IsValid)
                {
                    _context.Add(friend);
                    var result = _context.SaveChangesAsync().Result;

                    if (result != 0)
                        return new HttpResponseMessage(HttpStatusCode.OK);
                }

                return new HttpResponseMessage(HttpStatusCode.BadRequest);

            });
        }
        
        // PUT: api/Friends/5
        [HttpPut("{id}")]
        public async Task<HttpResponseMessage> Put(int id, [FromBody]Friend friend)
        {

            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                if (friend.IsValid)
                {
                    var friendToUpdate = _context.Friends.SingleOrDefaultAsync(f => f.Id == id).Result;
                    friendToUpdate.Update(friend);
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
                var friendToRemove = _context.Friends.SingleOrDefaultAsync(f => f.Id == id).Result;
                _context.Friends.Remove(friendToRemove);
                var result = _context.SaveChangesAsync().Result;

                if (result != 0)
                    return new HttpResponseMessage(HttpStatusCode.OK);

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            });

            
        }
    }
}
