using System;
using System.Collections.Generic;

namespace Business
{
    public class Cards
    {
        private string[] cardNumber = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
        private string[] cardShape = new string[] { "S", "C", "H", "D" };

        private List<string> c = new List<string>();
        Random r = new Random();

        public Cards()
        {
            c.Clear();
            for(int i = 0; i < 13; i++)
            {
                for(int j=0; j < 4; j++)
                {
                    c.Add(cardNumber[i] + cardShape[j]);
                }
            }
        }
        public string drawRandomCard()
        {
            int n = r.Next(0, c.Count);
            string cardname = c[n];
            c.Remove(c[n]);
            return cardname;

        }
        
    }
}
