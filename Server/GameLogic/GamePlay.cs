using Casino.Shared.Models;

namespace Casino.Server.GameLogic
{
    public class GamePlay
    {
        private Game board;
        private Deck deck;
        private Player? player1;
        private Player? player2;
        private static Random rnd = new Random();
        private Player? playerturn;
        public Dictionary<string, List<Card>>? CardBuilds;

        public GamePlay()
        {
            deck = InitializeShuffledDeck();
        }

        public GamePlay initializeGame()
        {
            //var game = new Game
            return null;
        }

        public void AddPlayers(Player p1, Player p2)
        {
            player1 = p1;
            player2 = p2;
            playerturn = p1;
        }

        public List<Card> DealCards()
        {
            var cards = deck.RemainingCards.Take(4).ToList();

            foreach(Card card in cards)
            {
                deck.RemainingCards.Remove(card);
            }

            return cards;
        }

        public bool GameIsReady()
        {
            return player1 != null && player2 != null;
        }

        public Player CreatePlayer(string name)
        {
            return new Player(name);
        }

        private Deck InitializeShuffledDeck()
        {
            var deck = new Deck();
            var name = "";
            for (int i = 0; i < 4; i++)
            {
                Suit suit;
                switch (i)
                {
                    case 0:
                        suit = Suit.Heart;
                        name += "s";
                        break;
                    case 1:
                        suit = Suit.Spade;
                        name += "p";
                        break;
                    case 2:
                        suit = Suit.Club;
                        name += "k";
                        break;
                    case 3:
                        suit = Suit.Diamond;
                        name += "l";
                        break;
                    default:
                        suit = Suit.Heart;
                        break;
                }

                for (int j = 1; j <= 13; j++)
                {
                    var points = 0;

                    if (j == 1)
                    {
                        name += "a";
                        points = 1;
                    }
                    else if (suit.Equals(Suit.Spade) && j == 2)
                        points = 1;
                    else if (suit.Equals(Suit.Diamond) && j == 10)
                        points = 2;

                    if (j == 11)
                        name += "j";
                    else if (j == 12)
                        name += "q";
                    else if (j == 13)
                        name += "k";
                    
                    deck.RemainingCards.Add(new Card(suit, j, points, name));
                }
            }
            // shuffle deck
            deck.RemainingCards.OrderBy(c => rnd.Next()).ToList();
            return deck;
        }

        public void incrementPlayerTurn()
        {
            if (playerturn == player1)
                playerturn = player2;
            else
                playerturn = player1;
        }
    }
}