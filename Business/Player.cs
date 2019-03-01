using System;
using System.Collections.Generic;
using System.Text;
//using WindowsForms;


namespace Business
{
    public class Player
    {

        private int money;
        private bool seenStatus;
        private string[] card;
        private bool active;
        
        public Player()
        {
            active = true;
            seenStatus = false;
            money = 5;
            card = new string[3];
        }

        public int getMoney()
        {
            return money;
        }
        public void putMoney(int hitMoney)
        {
            money = money + hitMoney;
        }
        public void setCard(int thCard,string cardName)
        {
            card[thCard] = cardName;
        }
        public string[] getCard()
        {
            
            return card;
        }

        public bool getSeenStatus()
        {
            return this.seenStatus;
        }
        public void setSeenStatus()
        {
            seenStatus = true;
        }
        public bool getActiveStatus()
        {
            return active;
        }
        public void setPacked()
        {
            active = false;
        }
    }
}
