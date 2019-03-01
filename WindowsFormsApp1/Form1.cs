using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        int noOfPlayers;
        Business.Game game;
        Panel[] playerPanels = new Panel[4];
        Panel paneli;
        PictureBox[,] cardPictureBox = new PictureBox[4,3];
        Button[] packBtn = new Button[4];
        Button[] hitBtn = new Button[4];
        Button[] seeBtn = new Button[4];
        Label[] playerMoney = new Label[4];
        Label[] playerName = new Label[4];
        Label totalMoney;
        Button showAll;

        public Form1()
        {
            InitializeComponent();
            pnlIntro.Parent = this;
            menuStrip1.Parent = this;
            btnPlay.Parent = this;
            pnlIntro.Left = this.Left;
            pnlIntro.Top = this.Top;
            
         }
        private Panel createPanel()
        {
            paneli = new Panel();
            paneli.AutoSize = true;
            //paneli.Controls.Add(this.btnPlay);
            paneli.Dock = System.Windows.Forms.DockStyle.Fill;
            paneli.Location = new System.Drawing.Point(0, 0);
            paneli.Size = new System.Drawing.Size(1920, 1080);
            paneli.Parent = this;
            paneli.BorderStyle= System.Windows.Forms.BorderStyle.FixedSingle;

            showAll = new Button();
            showAll.Visible = false;
            showAll.AutoEllipsis = true;
            showAll.Parent = paneli;
            showAll.Text = "Show All";
            showAll.Location = new System.Drawing.Point(this.Width / 2 - 65, 780);
            showAll.AutoSize = true;
            showAll.Click += delegate
            {
                showAllCards();
            };
            totalMoney = new Label();
            totalMoney.Parent = paneli;
            totalMoney.Location = new System.Drawing.Point(620, 360);
            totalMoney.AutoSize = true;
            totalMoney.Font = new System.Drawing.Font("Monotype Corsiva", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            return paneli;
        }

        private Panel createPlayerPanel(int playerNo)
        {
            Panel p = new Panel();
            p.Parent = paneli;
            p.Size = new System.Drawing.Size(400, 300);
            packBtn[playerNo] = new Button();
            hitBtn[playerNo] = new Button();
            playerMoney[playerNo] = new Label();
            seeBtn[playerNo] = new Button();
            playerName[playerNo] = new Label();

            playerName[playerNo].Parent = paneli;
            playerName[playerNo].Location = new System.Drawing.Point(p.Left,p.Top-20);
            playerName[playerNo].Text = "Player " + playerNo;
            

            packBtn[playerNo].Parent = p;
            packBtn[playerNo].Location = new System.Drawing.Point(260,10);
            packBtn[playerNo].AutoSize = true;
            packBtn[playerNo].Text = "Pack :(";
            packBtn[playerNo].UseVisualStyleBackColor = true;
            packBtn[playerNo].Click += delegate
            {
                game.playerPacked(playerNo);
                hitBtn[playerNo].Visible = false;
                PlayerPacked(playerNo);
                updatePlayers();
                NextTurn(playerNo);
                
            };

            hitBtn[playerNo].Parent = p;
            hitBtn[playerNo].Location = new System.Drawing.Point(10,240);
            hitBtn[playerNo].AutoSize = true;
            hitBtn[playerNo].Font = new System.Drawing.Font("Mistral", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            hitBtn[playerNo].Text = "HIT !!";
            hitBtn[playerNo].UseVisualStyleBackColor = true;
            hitBtn[playerNo].Click += delegate
            {
                game.hitmoney(playerNo);
                updatePlayers();
                NextTurn(playerNo);
            };

            playerMoney[playerNo].Parent = p;
            playerMoney[playerNo].Location = new System.Drawing.Point(100,240);
            playerMoney[playerNo].Font= new System.Drawing.Font("Mistral", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            playerMoney[playerNo].Text = Convert.ToString(game.getMoney(playerNo));
            playerMoney[playerNo].AutoSize = true;


            seeBtn[playerNo].Parent = p;
            seeBtn[playerNo].Location = new System.Drawing.Point(10, 200);
            seeBtn[playerNo].AutoSize = true;
            seeBtn[playerNo].Font = new System.Drawing.Font("Mistral", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            seeBtn[playerNo].Text = "See";
            seeBtn[playerNo].MouseDown += delegate
             {
                 game.setSeen(playerNo);
                 //seeBtn[playerNo].Visible = false;
                 displayCards(playerNo,true);
             };
            seeBtn[playerNo].MouseUp += delegate
            {
                displayCards(playerNo, false);
            };
            return p;
        }

        private void NextTurn(int playerNo)
        {
            playerPanels[playerNo].BorderStyle = System.Windows.Forms.BorderStyle.None;
            game.nextTurn();           
            if (game.playerPackedorNot(game.getTurn()))
            {
                NextTurn(game.getTurn());
            }
            playerPanels[game.getTurn()].BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            return;
        }

        private void updatePlayers()
        {
            for(int i = 0; i < noOfPlayers; i++)
            {
                playerMoney[i].Text = Convert.ToString(game.getMoney(i));
            }
            totalMoney.Text = "Winner Gets : \n       " + Convert.ToString(game.getTotalMoney());
        }
       

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void pnlIntro_Paint(object sender, PaintEventArgs e)
        {
            noOfPlayer.Focus();
            noOfPlayer.Minimum = 2;
            noOfPlayer.Maximum = 4;
        }

        private void btnOk_MouseDown(object sender, MouseEventArgs e)
        {
            noOfPlayers = Convert.ToInt32(noOfPlayer.Value);
        }

        private void btnOk_MouseUp(object sender, MouseEventArgs e)
        {
            paneli = createPanel();
            pnlIntro.Hide();
        }
       
        private void btnPlay_Click(object sender, EventArgs e)
        {
            btnPlay.Hide();
            game = new Game(noOfPlayers);
            showAll.Visible = true;
            totalMoney.Text = "Winner Gets : \n       "+Convert.ToString(game.getTotalMoney());

            for (int i = 0; i < noOfPlayers; i++)
            {
                playerPanels[i] = createPlayerPanel(i);
                
                if (i % 2 == 0)
                {
                    playerPanels[i].Location = new System.Drawing.Point(40, 50+(i*200));
                }
                else
                {
                    playerPanels[i].Location = new System.Drawing.Point(1000 , 50 +(i-1)*200);
                }
                
                
                for (int j = 0; j < 3; j++)
                {
                    cardPictureBox[i, j] = new PictureBox();
                    string cardFirst = "..\\cards png\\purple_back.png";
                    cardPictureBox[i, j].BackgroundImage = Image.FromFile(cardFirst);
                    cardPictureBox[i, j].BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    cardPictureBox[i, j].Parent = paneli;
                    cardPictureBox[i, j].Location = new System.Drawing.Point(10+playerPanels[i].Left + j * 100, 10+playerPanels[i].Top + j * 50);
                    cardPictureBox[i, j].Size = new System.Drawing.Size(100, 162);
                    playerPanels[i].Show();
                    cardPictureBox[i, j].Show();
                    cardPictureBox[i, j].BringToFront();
                }

                playerPanels[game.getTurn()].BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            }            
        }

       private void displayCards(int playerNum, bool y)
        {           
            string[] x = game.getCard(playerNum);
            for (int j = 0; j < 3; j++)
            {
                string cardInit = "..\\cards png\\" + x[j] + ".png";
                string cardFirst = "..\\cards png\\purple_back.png";
                if (y)
                {
                    cardPictureBox[playerNum, j].BackgroundImage = Image.FromFile(cardInit);
                }
                else
                {
                    cardPictureBox[playerNum, j].BackgroundImage = Image.FromFile(cardFirst);
                }
                
            }                       
        }

        private void PlayerPacked(int playernm)
        {
            for (int j = 0; j < 3; j++)
            {
                string cardInit = "..\\cards png\\gray_back.png";
                cardPictureBox[playernm, j].BackgroundImage = Image.FromFile(cardInit);
            }
        }

        private void showAllCards()
        {
            for (int i = 0; i < noOfPlayers; i++)
            {
                if (!game.playerPackedorNot(i))
                {
                    string[] x = game.getCard(i);
                    for (int j = 0; j < 3; j++)
                    {
                        string cardInit = "..\\cards png\\" + x[j] + ".png";
                        cardPictureBox[i, j].BackgroundImage = Image.FromFile(cardInit);
                    }
                }
                
            }
        }  

            private void startAgainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paneli.Dispose();
            paneli = createPanel();
            btnPlay.Show();
        }
        


        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
