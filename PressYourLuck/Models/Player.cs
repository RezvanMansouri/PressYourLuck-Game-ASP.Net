using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PressYourLuck.Models
{
    public class Player
    {
        //Player object that keeps track of the player’s name
        //and the starting number of coins

        [Required(ErrorMessage = "Please enter player name")]
        public string PlayerName { get; set; }

        [Required(ErrorMessage = "Please enter starting number of coins")]
        [Range(1.00, 10000.00, ErrorMessage = "starting number of coins should be an integer between $1.00 and $10,000.00")]
        public double? TotalCoins { get; set; }
    }
}
