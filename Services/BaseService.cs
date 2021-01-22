using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PunsApi.Data;
using PunsApi.Models;
using PunsApi.Services.Interfaces;

namespace PunsApi.Services
{
    public class BaseService
    {
        protected readonly AppDbContext _context;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        public BaseService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

            if (Params.IsTest == true)
            {
                var httpContext = new DefaultHttpContext()
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, "p10trek"),
                    //Paste corect Player in tetsts
                    new Claim(ClaimTypes.Name, "F21FB55F-7492-43A6-4C70-08D8BEF82257"),
                    new Claim(ClaimTypes.Email, "p10trek@o2.pl"),
                    new Claim(ClaimTypes.Role, "Admin")
                }))
                };
                _httpContextAccessor.HttpContext = httpContext;
            }
        }
        [HttpPost]
        public async Task<Player> GetPlayer()
        {
            var playerId =  _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            return await _context.Players.FirstOrDefaultAsync(x => x.Id.ToString() == playerId);
        }

    }
}
