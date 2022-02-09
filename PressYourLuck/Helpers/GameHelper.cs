using Microsoft.AspNetCore.Http;
using PressYourLuck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PressYourLuck.Helpers
{
    public static class GameHelper
    {

        //This creates a collection of 12 tiles with randomly generated values
        public static List<Tile> GenerateNewGame()
        {
            var tileList = new List<Tile>();
            Random r = new();
            for (int i = 0; i < 12; i++)
            {
                double randomValue = 0;
                if (r.Next(1, 4) != 1)
                {
                    randomValue = (r.NextDouble() + 0.5) * 2;
                }

                var tile = new Tile()
                {
                    TileIndex = i,
                    Visible = false,
                    Value = randomValue.ToString("N2")
                };

                tileList.Add(tile);
            }
            return tileList;
        }

        // - GetCurrentGame - If there is no current game state("<Tille>" == string.empty) in session Generate a New Game
        // and store it in session, otherwise deserialize the List<Tile> from session
        public static List<Tile> GetCurrentGame(HttpContext httpContext)
        {
            var tileList = httpContext.Session.GetObject<List<Tile>>("tile") ??
                            GenerateNewGame();

            SaveCurrentGame(httpContext, tileList);

            return tileList;
        }

        // - SaveCurrentGame - Save the current state of the game to session. 
        public static void SaveCurrentGame(HttpContext httpContext, List<Tile> tileList)
        {
            httpContext.Session.SetObject("tile", tileList);
        }

   
        public static double PickTileAndUpdateGame(HttpContext httpContext, int index)
        {
            double value = 0.0;
            double currentBet;

            List<Tile> tileList = GetCurrentGame(httpContext);

            foreach (Tile t in tileList)
            {
                if (t.TileIndex == index)
                {
                    if (double.Parse(t.Value) == 0.0)
                    {
                        currentBet = 0.0;
                        CoinsHelper.SaveCurrentBet(httpContext, currentBet);

                        RevealGame(tileList);
                        SaveCurrentGame(httpContext, tileList);

                        value = double.Parse(t.Value);
                    }
                    else
                    {
                        t.Visible = true;
                        double bet = CoinsHelper.GetCurrentBet(httpContext);
                        currentBet = bet * double.Parse(t.Value);
                        CoinsHelper.SaveCurrentBet(httpContext, currentBet);

                        DoubleUpValue(tileList);
                        SaveCurrentGame(httpContext, tileList);

                        value = double.Parse(t.Value);
                    }
                }
            }
            return value;
        }

        //to reaveal the cards
        public static void RevealGame(List<Tile> tileList)
        {
            foreach (Tile t in tileList)
            {
                t.Visible = true;
            }
        }

        //to double up the values
        public static void DoubleUpValue(List<Tile> tileList)
        {
            foreach (Tile t in tileList)
            {
                if (t.Visible == false)
                {
                    t.Value = (double.Parse(t.Value) * 2).ToString("N2");
                }
            }
        }

        // - ClearCurrentGame - clear the current game state from session
        public static void ClearCurrentGame(HttpContext httpContext)
        {
            httpContext.Session.Remove("tile");
        }
    }
}
