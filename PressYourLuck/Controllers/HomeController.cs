using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PressYourLuck.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PressYourLuck.Helpers;
using AS3_RM5831.Models;



namespace PressYourLuck.Controllers
{
    public class HomeController : Controller
    {
        private AuditContext _auditContext;

        public HomeController(AuditContext auditContext)
        {
            _auditContext = auditContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //if the player-name cookie is empty,
            //go the player controller in order to enter in new information
            if (!(this.Request.Cookies.ContainsKey("playerName")))

                return RedirectToAction("Index", "Player");
            else
            {
                ViewBag.Name = this.Request.Cookies["playerName"];
                ViewBag.totalCoins = CoinsHelper.GetTotalCoins(HttpContext);
            }
            return View();
        }

        [HttpPost]

        public IActionResult Index(Bet bet)
        {
            if (ModelState.IsValid)
            {
                //save the original total coin amount
                double originalTotalCoin = CoinsHelper.GetTotalCoins(HttpContext);
                CoinsHelper.SaveOriginalBet(HttpContext, (double)bet.BetAmount);

                //save total coin amount
                double newTotalCoin = originalTotalCoin - (double)bet.BetAmount;
                CoinsHelper.SaveTotalCoins(HttpContext, newTotalCoin.ToString());

                //save current Bet
                CoinsHelper.SaveCurrentBet(HttpContext, (double)bet.BetAmount);

                return RedirectToAction("Index", "Game");
            }
            else
                return View(bet);
        }
        //clear the cookies and send the user back to the screen to enter a new player
        public IActionResult Cashout()
        {
            string playerName = HttpContext.Request.Cookies["playerName"];
            double totalCoins = CoinsHelper.GetTotalCoins(HttpContext);

            //add to the database as Cashout and save it
            Audit audit = new() { PlayerName = playerName, Amount = totalCoins, AuditTypeId = "CashOut" };
            _auditContext.Audits.Add(audit);
            _auditContext.SaveChanges();
            
            Response.Cookies.Delete("playerName");
            Response.Cookies.Delete("totalCoins");

            GameHelper.ClearCurrentGame(HttpContext);

            TempData["messages"] = $"You cashed out for {totalCoins:c2} coins";

            return RedirectToAction("Index", "Player");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
