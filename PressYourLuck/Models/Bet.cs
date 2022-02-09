using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace PressYourLuck.Models
{
    public class Bet
    {
        [Required(ErrorMessage = "Please enter an amount")]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter an amount greater than 0")]
        public Double? BetAmount { get; set; }
    }
}
