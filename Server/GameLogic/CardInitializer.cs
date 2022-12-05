using Casino.Shared.Models;

namespace Casino.Server.GameLogic
{
    public class CardInitializer {
        private static Random rnd = new Random();

        public Deck InitializeDeck()
        {
            var deck = new Deck();
            for (int i = 0; i < 4; i++)
            {
                var suitName = "";
                Suit suit;
                switch (i)
                {
                    case 0:
                        suit = Suit.Heart;
                        suitName += "s";
                        break;
                    case 1:
                        suit = Suit.Spade;
                        suitName += "p";
                        break;
                    case 2:
                        suit = Suit.Club;
                        suitName += "k";
                        break;
                    case 3:
                        suit = Suit.Diamond;
                        suitName += "l";
                        break;
                    default:
                        suit = Suit.Heart;
                        suitName += "s";
                        break;
                }

                for (int j = 1; j <= 13; j++)
                {
                    var name = suitName;
                    var points = 0;

                    if (suit.Equals(Suit.Spade) && j == 2)
                        points = 1;
                    else if (suit.Equals(Suit.Diamond) && j == 10)
                        points = 2;

                    if (j == 11)
                        name += "j";
                    else if (j == 12)
                        name += "q";
                    else if (j == 13)
                        name += "k";
                    else if (j == 1)
                    {
                        name += "a";
                        points = 1;
                    }
                    else
                        name += j;

                    deck.RemainingCards.Add(new Card(suit, j, points, name));

                }
            }
            return deck;
        }

        public List<Card> ShuffleDeck(List<Card> cards)
        {
            var shuffled = cards.OrderBy(c => rnd.Next()).ToList();
            foreach(var c in shuffled)
            {
                System.Diagnostics.Debug.WriteLine("DealCards." + c.Name);
            }
            return shuffled;
        }
    }
}
