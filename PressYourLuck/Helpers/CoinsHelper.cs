using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PressYourLuck.Helpers
{
    public static class CoinsHelper
    {
     
        public static void SaveCurrentBet(HttpContext httpContext, double bet)
        {
            httpContext.Session.SetString("current-bet", bet.ToString("N2"));
        }

        //Return the current bet stored in session
        public static double GetCurrentBet(HttpContext httpContext)
        {
           double currentBet= Double.Parse(httpContext.Session.GetString("current-bet"));
            return currentBet;
        }

        //Save the original bet into session
        public static void SaveOriginalBet(HttpContext httpContext, double bet)
        {
            httpContext.Session.SetString("original-bet", bet.ToString("N2"));
        }

        //Get the original bet from session
        public static double GetOriginalBet(HttpContext httpContext)
        {
            double originalBet = double.Parse( httpContext.Session.GetString("original-bet"));
            return originalBet;
        }

        //Save the players total number of coins into a cookie.  Don't forget to
        // persist the cookie (Chapter 9!)
        public static void SaveTotalCoins(HttpContext httpContext, string totalCoins)
        {
            var option = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(30)
            };
            httpContext.Response.Cookies.Append("totalCoins", totalCoins, option);
        }

        //Get the players total number of coins from a cookie.
        public static double GetTotalCoins(HttpContext httpContext)
        {
            double totalCoins = double.Parse( httpContext.Request.Cookies["totalCoins"]);
            return totalCoins;
        }

       

    }
}
