using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PressYourLuck.Models;
using PressYourLuck.Helpers;
using AS3_RM5831.Models;

namespace PressYourLuck.Controllers
{
    public class PlayerController : Controller
    {
        //The controller that create and cashes out player data.

        private AuditContext _auditContext;
        public PlayerController(AuditContext auditContext)
        {
            _auditContext = auditContext;
        }

        [HttpGet()]
        public IActionResult Index()
        {
            return View();
        }

          [HttpPost()]
        public IActionResult Index(Player player)
        {
            if (ModelState.IsValid)
            {
                //make the cookie persistent for 30 days
                var option = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(30)
                };

                string playerName = player.PlayerName;
                double? totalCoins = player.TotalCoins;

                //Add a record to the database as CashIn and save it
                 Audit audit = new() { PlayerName = playerName, Amount = totalCoins, AuditTypeId ="CashIn"};
                _auditContext.Audits.Add(audit);
                _auditContext.SaveChanges();


                HttpContext.Response.Cookies.Append("playerName", playerName ,option);

                CoinsHelper.SaveTotalCoins(HttpContext, totalCoins.ToString());

                return RedirectToAction("Index", "Home");
            }
            else
                return View("Index", player);

        }
    }
}
