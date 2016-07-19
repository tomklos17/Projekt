using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolkoIKrzyzyk
{
    public class Figura : IDodawanieDoPlanszy
    {
        public KrojFigury JakaFigura { get; }

        public Figura(KrojFigury kroj)
        {
            JakaFigura = kroj;
        }

        public void RysujFigure(Texture2D teksturaKrzyzyk, Texture2D teksturaKolko, SpriteBatch spriteBatch, int x, int y)
        {
            switch (JakaFigura)
            {
                case KrojFigury.Krzyżyk:
                    spriteBatch.Draw(teksturaKrzyzyk, new Vector2(x, y), Color.White);
                    break;
                case KrojFigury.Kółko:
                    spriteBatch.Draw(teksturaKolko, new Vector2(x, y), Color.White);
                    break;
            }
        }

        public void DodajDoPlanszy(SortedList<int, Figura> plansza, ref int[] tablicaSymboli, int i)
        {
            Exception PoleZajete = new Exception();
            foreach (var item in plansza)
            {
                if (item.Key == i)
                {
                    throw PoleZajete;
                }
            }
            plansza.Add(i, this);
            tablicaSymboli[i] = 1;
        }

        public void DodajDoPlanszy(SortedList<int, Figura> plansza, ref int[] tablicaSymboli)
        {
            var rand = new Random();
            int pole = rand.Next(0, 8);
            Exception PoleZajete = new Exception();
            foreach (var item in plansza)
            {
                if (item.Key == pole)
                {
                    throw PoleZajete;
                }
            }
            plansza.Add(pole, this);
            tablicaSymboli[pole] = 0;
        }

    }
}
