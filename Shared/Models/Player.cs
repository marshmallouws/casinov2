namespace Casino.Shared.Models
{
    public class Player
    {
        public string Name { get; set; }
        public List<Card> Points { get; private set; }

        public Player(string name)
        {
            Name = name;
            Points = new List<Card>();
        }

        public Player() { }

        public void AddPoints(List<Card> cardsWon)
        {
            Points.AddRange(cardsWon);
        }
    }
}
