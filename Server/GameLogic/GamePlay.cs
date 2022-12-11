using Casino.Shared.Models;

namespace Casino.Server.GameLogic
{
    public class GamePlay
    {
        private Game board;
        private Deck deck;
        private List<Player> players;
        private Player? player1;
        private Player? player2;
        private static Random rnd = new Random();
        private Player? currentPlayer;
        private CardInitializer cardInit = new CardInitializer();
        public Dictionary<string, List<Card>>? CardBuilds;
        public List<Card> TableCards { get; set; }

        public GamePlay(List<Player> _players)
        {
            players = _players;
            currentPlayer = players[0];
            deck = cardInit.InitializeDeck();
            deck.RemainingCards = cardInit.ShuffleDeck(deck.RemainingCards);
            TableCards = DealCards();
        }

        public void AddPlayer(Player p)
        {
            if (player1 == null)
            {
                player1 = p;
                currentPlayer = player1;
            }
            else if (player2 == null)
            {
                player2 = p;
            }
        }

        public void AddPlayers(Player p1, Player p2)
        {
            player1 = p1;
            player2 = p2;
            currentPlayer = p1;
        }

        public List<Card> DealCards()
        {
            var cards = deck.RemainingCards.Take(4).ToList();
            foreach (Card card in cards)
            {
                deck.RemainingCards.Remove(card);
            }
            return cards;
        }

        public void IncrementPlayerTurn()
        {
            for(int i = 0; i < players.Count; i++)
            {
                if (players[i].Equals(currentPlayer))
                {
                    try
                    {
                        currentPlayer = players[i + 1];
                    } catch (Exception e)
                    {
                        currentPlayer = players[0];
                    }
                    break;
                }
            }
        }

        public int GetDeckSize()
        {
            return deck.RemainingCards.Count;
        }

        public void CountPoints(CardBuild build)
        {
            if(build.TableCards.Count == 0)
            {
                TableCards.Add(build.PlayerCard); // Player lays down a card
            } else
            {
                currentPlayer.AddPoints(build.TableCards);
                currentPlayer.AddPoints(new List<Card> { build.PlayerCard });

                var toRemove = new List<Card>();
                foreach (var c in TableCards)
                {
                    foreach (var ca in build.TableCards)
                    {
                        if (c.Name == ca.Name)
                            toRemove.Add(c);
                    }
                }

                foreach(var c in toRemove)
                {
                    TableCards.Remove(c);
                }
            }
        }

        public Player GetPlayerTurn()
        {
            return currentPlayer;
        }
    }
}