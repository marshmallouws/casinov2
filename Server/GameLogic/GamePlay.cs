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
        private CardInitializer cardInit = new CardInitializer();
        public Dictionary<string, List<Card>>? CardBuilds;

        public GamePlay()
        {
            deck = cardInit.InitializeDeck();
            deck.RemainingCards = cardInit.ShuffleDeck(deck.RemainingCards);
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
            
            foreach (Card card in cards)
            {
                System.Diagnostics.Debug.WriteLine("DealCards." + card.Name);
                deck.RemainingCards.Remove(card);
            }
            System.Diagnostics.Debug.WriteLine("DealCards." + deck.RemainingCards.Count);
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

        public void incrementPlayerTurn()
        {
            if (playerturn == player1)
                playerturn = player2;
            else
                playerturn = player1;
        }
    }
}