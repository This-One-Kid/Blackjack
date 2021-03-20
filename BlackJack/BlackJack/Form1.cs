using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BlackJack
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool AIHit = false;
        bool pad = false;
        bool aad = false;
        public Stack<Card> deck;
        Bitmap backCardImage;
        Stack<Card> copymachine;
        int playval = 0;
        int AIval = 0;
        int pace = 0;
        int AIce = 0;
        bool roundend = false;
        Deck doubledecker = new Deck();

        private void Form1_Load(object sender, EventArgs e)
        {
            label2.Visible = false;
            DealDat.Visible = false;
            deck = new Stack<Card>();
            Reset(deck);
            copymachine = deck;
            deck = doubledecker.Shuffle(deck);
            DealStarte();




        }
        void Reset(Stack<Card> deck)
        {
            string folder = "images";
            bool directoryExists = Directory.Exists(folder);
            if (!directoryExists)
            {
                throw new Exception($"Make sure the images folder is there filled with the images.");

            }

            List<Suit> order = new List<Suit>() { Suit.Clubs, Suit.Diamonds, Suit.Hearts, Suit.Spades };
            int currentSuit = 0;

            int fileCounter = 0;
            string filePrefix = "gbCard";
            string extension = ".gif";

            int val = 1;
            while (fileCounter < 52)
            {


                string tempFile = filePrefix + fileCounter + extension;
                string fullpath = Path.Combine(folder, tempFile);

                Bitmap tempimage = new Bitmap(fullpath);
                Card tempCard;
                if (fileCounter % 13 == 0)
                {
                    tempCard = new Card(order[currentSuit], 11, tempimage);
                }
                else if (fileCounter % 13 == 1)
                {
                    tempCard = new Card(order[currentSuit], 2, tempimage);
                }
                else if (fileCounter % 13 == 2)
                {
                    tempCard = new Card(order[currentSuit], 3, tempimage);
                }
                else if (fileCounter % 13 == 3)
                {
                    tempCard = new Card(order[currentSuit], 4, tempimage);
                }
                else if (fileCounter % 13 == 4)
                {
                    tempCard = new Card(order[currentSuit], 5, tempimage);
                }
                else if (fileCounter % 13 == 5)
                {
                    tempCard = new Card(order[currentSuit], 6, tempimage);
                }
                else if (fileCounter % 13 == 6)
                {
                    tempCard = new Card(order[currentSuit], 7, tempimage);
                }
                else if (fileCounter % 13 == 7)
                {
                    tempCard = new Card(order[currentSuit], 8, tempimage);
                }
                else if (fileCounter % 13 == 8)
                {
                    tempCard = new Card(order[currentSuit], 9, tempimage);
                }
                else
                {
                    tempCard = new Card(order[currentSuit], 10, tempimage);
                }

                deck.Push(tempCard);

                fileCounter++;

                if (fileCounter % 13 == 0)
                {
                    currentSuit++;
                }
            }
            backCardImage = new Bitmap(Path.Combine(folder, filePrefix + fileCounter + extension));
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = playval.ToString();
            label2.Text = AIval.ToString();
            CheckAce();
            if (AIHit == true)
            {
                if (AIval > 16 && AIHit == true)
                {
                    roundend = true;
                    label3.Visible = true;
                    pictureBox6.SendToBack();
                    AIHit = false;
                }
                else
                {
                    Card temp = deck.Pop();
                    if (pictureBox4.Image == null)
                    {
                        pictureBox4.Image = temp.GetImage();
                    }
                    else if (pictureBox16.Image == null)
                    {
                        pictureBox16.Image = temp.GetImage();
                    }
                    else if (pictureBox17.Image == null)
                    {
                        pictureBox17.Image = temp.GetImage();
                    }
                    else if (pictureBox18.Image == null)
                    {
                        pictureBox18.Image = temp.GetImage();
                    }
                    else if (pictureBox19.Image == null)
                    {
                        pictureBox19.Image = temp.GetImage();
                    }
                    else if (pictureBox20.Image == temp.GetImage())
                    {
                        pictureBox20.Image = temp.GetImage();
                    }
                    else if (pictureBox21.Image == null)
                    {
                        pictureBox21.Image = temp.GetImage();
                    }
                    else if (pictureBox22.Image == null)
                    {
                        pictureBox22.Image = temp.GetImage();
                    }
                    else if (pictureBox23.Image == null)
                    {
                        pictureBox23.Image = temp.GetImage();
                    }
                    else
                    {
                        deck.Push(temp);
                    }
                    if (temp.value == 11)
                    {
                        AIce++;
                    }
                    AIval += temp.value;
                }

            }
            CheckAce();
            int checkWin = -1;
            if (playval > 21 || AIval > 21)
            {
                roundend = true;
            }
            if (roundend == true)
            {
                pictureBox6.SendToBack();
            }
            if (roundend == true || playval > 21 || AIval > 21)
            {
                checkWin = isWin();
            }
            if (checkWin == 2)
            {
                label3.Text = "Nice job winning against the AI!";
                timer1.Enabled = false;
                DealDat.Visible = true;
                button1.Visible = false;
                Hit.Visible = false;
            }
            else if (checkWin == 0)
            {
                label3.Text = "You lose...";
                timer1.Enabled = false;
                DealDat.Visible = true;
                button1.Visible = false;
                Hit.Visible = false;
            }
            else if (checkWin == 1)
            {
                label3.Text = "You tied.";
                DealDat.Visible = true;
                button1.Visible = false;
                Hit.Visible = false;
                timer1.Enabled = false;
            }
            if (roundend == false)
            {
                label3.Text = "Round in progress... Please wait for results.";
            }
        }
        private void CheckAce()
        {
            if (pace > 0 && playval > 21)
            {
                playval -= pace * 10;
                pad = true;
                pace = 0;
            }
            if (AIce > 0 && AIval > 21)
            {
                AIval -= AIce * 10;
                aad = true;
                AIce = 0;
            }
        }
        private int isWin ()
        {
            if (playval > 21)
            {
                return 0;
            }
            else if (AIval > 21)
            {
                return 2;
            }
            if (playval > AIval)
            {
                if (playval > 21)
                {
                    return 0;//0 = lose
                }
                else
                {
                    return 2;//playerwin
                }
            }
            else if (playval == AIval)
            {
                return 1;//tie
            }
            else
            {
                if (AIval > 21)
                {
                    return 2;
                }
                else
                {
                    return 0;
                }
            }
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void NANI_Click(object sender, EventArgs e)
        {
            if (playval < 21)
            {
                Card temp = deck.Pop();
                if (pictureBox3.Image == null)
                {
                    pictureBox3.Image = temp.GetImage();
                }
                else if (pictureBox7.Image == null)
                {
                    pictureBox7.Image = temp.GetImage();
                }
                else if (pictureBox8.Image == null)
                {
                    pictureBox8.Image = temp.GetImage();
                }
                else if (pictureBox9.Image == null)
                {
                    pictureBox9.Image = temp.GetImage();
                }
                else if (pictureBox10.Image == null)
                {
                    pictureBox10.Image = temp.GetImage();
                }
                else if (pictureBox11.Image == null)
                {
                    pictureBox11.Image = temp.GetImage();
                }
                else if (pictureBox12.Image == null)
                {
                    pictureBox12.Image = temp.GetImage();
                }
                else if (pictureBox13.Image == null)
                {
                    pictureBox13.Image = temp.GetImage();
                }
                else if (pictureBox14.Image == null)
                {
                    pictureBox14.Image = temp.GetImage();
                }
                else
                {
                    deck.Push(temp);
                }
                if (temp.value == 11)
                {
                    pace++;
                }
                playval += temp.value;
                //if (pad == true && temp.value == 11)
                //{
                //    playval -= 10;
                //}
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Hit.Visible = false;
            pictureBox6.SendToBack();
            AIHit = true;
        }
        void DealStarte()
        {
            Card temp = deck.Pop();
            pictureBox1.Image = temp.GetImage();
            playval += temp.value;
            if (temp.value == 11)
            {
                pace++;
            }
            temp = deck.Pop();
            pictureBox2.Image = temp.GetImage();
            playval += temp.value;
            if (temp.value == 11)
            {
                pace++;
            }
            temp = deck.Pop();
            AIval += temp.value;
            pictureBox5.Image = temp.GetImage();
            if (temp.value == 11)
            {
                AIce++;
            }
            pictureBox6.Image = Properties.Resources.gbCard52;
            temp = deck.Pop();
            pictureBox15.SendToBack();
            pictureBox15.Image = temp.GetImage();
            AIval += temp.value;
            if (temp.value == 11)
            {
                AIce++;
            }
        }
        private void DealDat_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox4.Image = null;
            pictureBox5.Image = null;
            pictureBox6.Image = null;
            pictureBox7.Image = null;
            pictureBox8.Image = null;
            pictureBox9.Image = null;
            pictureBox10.Image = null;
            pictureBox11.Image = null;
            pictureBox12.Image = null;
            pictureBox13.Image = null;
            pictureBox14.Image = null;
            pictureBox15.Image = null;
            pictureBox16.Image = null;
            pictureBox17.Image = null;
            pictureBox18.Image = null;
            pictureBox19.Image = null;
            pictureBox20.Image = null;
            pictureBox21.Image = null;
            pictureBox22.Image = null;
            pictureBox23.Image = null;
            Reset(deck);
            deck = doubledecker.Shuffle(deck);
            pictureBox6.BringToFront();
            timer1.Enabled = true;
            Hit.Visible = true;
            button1.Visible = true;
            DealDat.Visible = false;
            aad = true;
            pad = true;
            AIHit = false;
            roundend = false;
            playval = 0;
            AIval = 0;
            DealStarte();
            this.Focus();
        }
    }
}
