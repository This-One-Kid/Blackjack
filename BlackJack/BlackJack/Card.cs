using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public  class Card
    {
        Suit suit;
        public int value;
        Bitmap image;

        public Card(Suit suit, int val, Bitmap image)
        {
            this.suit = suit;
            this.value = val;
            this.image = image;
        }

        public Bitmap GetImage()
        {
            return image;
        }

    }
}
