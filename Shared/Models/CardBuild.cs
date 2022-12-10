using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.Shared.Models
{
    public class CardBuild
    {
        // The build number eg. 3, 6
        public int Number { get; set; }
        public Card PlayerCard { get; set; }
        public List<Card> TableCards { get; set; }
        public bool IsDouble { get; set; }
        public Player? Builder { get; set;  }

        public CardBuild() { }

        public CardBuild(Player _builder)
        {
            Builder = _builder;
            TableCards = new List<Card>();
        }

        public void UpdateSelectedTableCards(Card card)
        { 
            Card? foundCard = null;
            foreach(var c in TableCards)
            {
                if (c.Name == card.Name)
                {
                    foundCard = c;
                }
            }

            if(foundCard == null)
            {
                TableCards.Add(card);
            } else
            {
                TableCards.Remove(foundCard);
            }
        }

        public void UpdatePlayerCard(Card card)
        {
            PlayerCard = card;
        }
    }
}
