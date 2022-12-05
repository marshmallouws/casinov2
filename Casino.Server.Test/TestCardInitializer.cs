using Casino.Server.GameLogic;
namespace Casino.Server.Test
{
    public class TestCardInitializer
    {
        [Fact]
        public void TestCardInitialization()
        {
            var cardInit = new CardInitializer();

            var cardDeck = cardInit.InitializeDeck().RemainingCards;

            Assert.True(cardDeck.Count == 52);
            Assert.True(cardDeck[0].Name == "sa");
            Assert.True(cardDeck.Distinct().Count() == cardDeck.Count);
        }
        [Fact]
        public void TestShuffleDeck()
        {
            var cardInit = new CardInitializer();

            var cardDeck = cardInit.InitializeDeck().RemainingCards;
            var shuffled = cardInit.ShuffleDeck(cardDeck);
            Assert.True(cardDeck.Count == 52);
            Assert.True(cardDeck.Distinct().Count() == cardDeck.Count);
        }
    }
}