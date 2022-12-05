namespace Casino.Shared.Models
{
    public class Card
    {
        public Suit Suit { get; }
        public int Number { get; }
        public int Points { get; }

        // Used to match name of picture when visualizing card
        public string Name { get; }

        public Card(Suit suit, int number, int points, string name)
        {
            Suit = suit;
            Number = number;
            Points = points;
            Name = name;
        }
    }
}
