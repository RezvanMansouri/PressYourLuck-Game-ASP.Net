using Microsoft.AspNetCore.Mvc;
using PressYourLuck.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PressYourLuck.Models;
using Microsoft.AspNetCore.Http;
using AS3_RM5831.Models;

namespace PressYourLuck.Controllers
{
    public class GameController : Controller
    {
        private AuditContext _auditContext;
        public GameController(AuditContext auditContext)
        {
            _auditContext = auditContext;
        }

        public IActionResult Index()
        {
            var tileList = GameHelper.GetCurrentGame(HttpContext);

            //some viewbag for changing the button and color of it 
            if (CoinsHelper.GetCurrentBet(HttpContext) <= 0.0)
            {
                ViewBag.Action = "TryAgain";
                ViewBag.Button = "TRY AGAIN";
                ViewBag.Color = "btn-danger";
            }
            else
            {
                ViewBag.Action = "TakeTheCoins";
                ViewBag.Button = "TAKE THE COINS";
                ViewBag.Color = "btn-success";
            }

            return View(tileList);
        }


        public IActionResult Reveal(int id)
        {
            double tileValue = GameHelper.PickTileAndUpdateGame(HttpContext, id);

            string playerName = HttpContext.Request.Cookies["playerName"];
            double currentBet = CoinsHelper.GetCurrentBet(HttpContext);
            double originalBet = CoinsHelper.GetOriginalBet(HttpContext);

            //if the new revealed value equal to 0 or not then show the msgs
            if (tileValue != 0.0)
            {
                TempData["messages"] = $"Congrats you’ve found a {tileValue} multipler!  All remaining values have doubled.  Will you Press Your Luck?";

                //Add the record to the table as Win and save
                Audit audit = new() { PlayerName = playerName, Amount = currentBet, AuditTypeId = "Win" };
                _auditContext.Audits.Add(audit);
                _auditContext.SaveChanges();
            }
            else
            {
                TempData["messages"] = "On no!You busted out. Better luck next time!";

                //Add the record to the table as lose and save
                Audit audit = new() { PlayerName = playerName, Amount = originalBet, AuditTypeId = "Lose" };
                _auditContext.Audits.Add(audit);
                _auditContext.SaveChanges();
            }
            return RedirectToAction("Index", "Game");
        }
        public IActionResult TakeTheCoins()
        {
            double total = CoinsHelper.GetCurrentBet(HttpContext) + CoinsHelper.GetTotalCoins(HttpContext);

            CoinsHelper.SaveTotalCoins(HttpContext, total.ToString());
            GameHelper.ClearCurrentGame(HttpContext);

            TempData["messages"] = $"BIG WINNER! You chased out for {total:c2} coins!  Care to press your luck again?";

            return RedirectToAction("Index", "Home");
        }
        public IActionResult TryAgain()
        {
            if (CoinsHelper.GetTotalCoins(HttpContext) <= 0)
            {
                GameHelper.ClearCurrentGame(HttpContext);

                return RedirectToAction("Index", "Player");
            }
            else
            {
                GameHelper.ClearCurrentGame(HttpContext);

                return RedirectToAction("Index", "Home");
            }

        }
    }
}
