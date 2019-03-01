using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public class Game
    {
        private Player[] player;
        private Cards cards;
        private int BlindMoney;
        private int totalMoney;
        private int noOfPlayers;
        private int turn;

        public Game(int x)
        {
            player = new Player[4];
            noOfPlayers = x;
            cards = new Cards();
            BlindMoney = 5;
            turn = 0;
            for(int i = 0; i < x; i++)
            {
                player[i] = new Player();
                totalMoney = totalMoney + BlindMoney;
                for(int j = 0; j < 3; j++)
                {
                    player[i].setCard(j, cards.drawRandomCard());
                }
            }
            
        }

        public void nextTurn()
        {
            turn = (turn + 1) % noOfPlayers;
        }
        public int getTurn()
        {
            return turn;
        }

        public string[] getCard(int p)
        {
            return player[p].getCard();
        }
            

        public void hitmoney(int playerNumber)
        {
            if (player[playerNumber].getSeenStatus())
            {
                player[playerNumber].putMoney(BlindMoney * 2);
                totalMoney = totalMoney+ (BlindMoney * 2);
            }
            else
            {
                player[playerNumber].putMoney(BlindMoney);
                totalMoney = totalMoney+ BlindMoney;
            }
            
        }

        public int getMoney(int playerNumber)
        {
            return player[playerNumber].getMoney();
        }
        
        public bool getSeen(int playerNm)
        {
            return player[playerNm].getSeenStatus();
        }
        public void setSeen(int playerNm)
        {
            player[playerNm].setSeenStatus();
        }

        public void playerPacked(int playerNm)
        {
            player[playerNm].setPacked();
        }
        public bool playerPackedorNot(int playerNumber)
        {
            return !(player[playerNumber].getActiveStatus());
        }

        public int getTotalMoney()
        {
            return totalMoney;
        }

    }
}
