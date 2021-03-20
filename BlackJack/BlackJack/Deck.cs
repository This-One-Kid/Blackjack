using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class Deck
    {
        Random gen = new Random();

        public Stack<Card> Shuffle(Stack<Card> deck)
        {
            Stack<Card> stackOfCards = new Stack<Card>();
            bool[] isCardDealed = new bool[52];
            int numchosen = -1;
            Card[] cAmEl = new Card[52]; 
            for (int count = 0; count < 52; count++)
            {
                isCardDealed[count] = false;
            }
            for (int count = 0; count < 52; count++)
            {

                numchosen = gen.Next(0, 52);
                while (isCardDealed[numchosen] == true)
                {
                    numchosen = gen.Next(0, 52);
                }
                cAmEl[numchosen] = deck.Pop();
                isCardDealed[numchosen] = true;
            }
            for (int i = 0;i < 52;i ++)
            {
                stackOfCards.Push(cAmEl[i]);
            }


            deck = stackOfCards;
            return stackOfCards;
        }
    }
}
