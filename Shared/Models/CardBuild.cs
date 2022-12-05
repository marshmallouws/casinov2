using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.Shared.Models
{
    public class CardBuild
    {
        private int Number { get; set; }
        List<Card> Cards { get; set; }
        private bool IsDouble { get; set; }
        private Player? Builder { get; set;  }
         
    }
}
