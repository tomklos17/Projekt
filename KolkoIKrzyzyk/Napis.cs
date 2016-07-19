using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolkoIKrzyzyk
{
    public class Napis
    {
        public string Tresc { get; set; }
        public int X { get; set; }
        private int Y;

        public Napis(string tresc, int x, int y)
        {
            Tresc = tresc;
            X = x;
            Y = y;
        }

        public void RysujNapis(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.DrawString(font, Tresc, new Vector2(X, Y), Color.Black);
        }
    }
}
