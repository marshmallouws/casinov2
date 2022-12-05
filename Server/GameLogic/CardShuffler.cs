using System.Security.Cryptography;

namespace Backend.GameLogic
{
    public static class CardShuffler {

        // https://stackoverflow.com/questions/273313/randomize-a-listt
        private static Random rng = new Random();
        public static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
