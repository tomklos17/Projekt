using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading;

namespace KolkoIKrzyzyk
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D krzyzyk;
        private Texture2D kolko;
        private Texture2D tlo;
        private SpriteFont font;
        private bool ruchGracza;
        private SortedList<int, Figura> plansza;

        private int[] tablicaSymboli;

        private int[] WspolrzedneX = new int[9];
        private int[] WspolrzedneY = new int[9];

        private Napis Linia1;
        private Napis Linia2;
        private Napis Linia3;
        private Napis Linia4;

        private int licznik;

        private Kolko FiguraKolko;
        private Krzyzyk FiguraKrzyzyk;
        private bool koniecGry = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 500; this.graphics.PreferredBackBufferHeight = 500;
            Window.Position = new Point(390, 190);
            Window.Title = "Kółko i krzyżyk";
            graphics.ApplyChanges();

            Linia1 = new Napis("Twoj ruch.", 220, 50);
            Linia2 = new Napis("Wybierz numer pola, na ktore chcesz wstawic symbol.", 65, 80);
            Linia3 = new Napis("", 65, 110);
            Linia4 = new Napis("", 65, 400);
            ruchGracza = true;
            plansza = new SortedList<int, Figura>();

            tablicaSymboli = new int[9];

            for (int i = 0; i < 9; i++)
            {
                tablicaSymboli[i] = 4;
            }

            licznik = 0;

            WyznaczWspolrzedneXIY(ref WspolrzedneX, ref WspolrzedneY);

            FiguraKolko = new Kolko();
            FiguraKrzyzyk = new Krzyzyk();


            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tlo = Content.Load<Texture2D>("tlo");
            kolko = Content.Load<Texture2D>("o");
            krzyzyk = Content.Load<Texture2D>("x");
            font = Content.Load<SpriteFont>("Font");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(tlo, new Vector2(0, 0), Color.White);
            Linia1.RysujNapis(spriteBatch, font);
            Linia2.RysujNapis(spriteBatch, font);
            Linia3.RysujNapis(spriteBatch, font);
            Linia4.RysujNapis(spriteBatch, font);
            RysujPlansze(plansza);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        protected override void Update(GameTime gameTime)
        {
            licznik++;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (!koniecGry)
            {
                if (ruchGracza)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.D0))
                    {
                        RuchGracza(0);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.D1))
                    {
                        RuchGracza(1);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.D2))
                    {
                        RuchGracza(2);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.D3))
                    {
                        RuchGracza(3);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.D4))
                    {
                        RuchGracza(4);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.D5))
                    {
                        RuchGracza(5);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.D6))
                    {
                        RuchGracza(6);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.D7))
                    {
                        RuchGracza(7);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.D8))
                    {
                        RuchGracza(8);
                    }
                }
                else
                {
                    RuchPrzeciwnika();
                }
            }
            if (SprawdzKoniecGry(0))
            {
                koniecGry = true;
                Linia1.Tresc = "KONIEC GRY";
                Linia1.X = 200;
                Linia2.Tresc = "Przegrales :(";
                Linia2.X = 205;

            }
            if (SprawdzKoniecGry(3))
            {
                koniecGry = true;
                Linia1.Tresc = "KONIEC GRY";
                Linia1.X = 200;
                Linia2.Tresc = "Brawo! Wygrales!";
                Linia2.X = 185;
            }


            base.Update(gameTime);
        }

        void RysujPlansze(SortedList<int, Figura> plansza)
        {
            foreach (var item in plansza)
            {
                item.Value.RysujFigure(krzyzyk, kolko, spriteBatch, WspolrzedneX[item.Key], WspolrzedneY[item.Key]);
            }
        }

        void WyznaczWspolrzedneXIY(ref int[] X, ref int[] Y)
        {
            X[0] = 144;
            Y[0] = 144;
            int i = 1;
            while (i < 3)
            {
                X[i] = X[0] + i * 78;
                Y[i] = Y[0];
                i++;
            }
            while (i < 6)
            {
                X[i] = X[i - 3];
                Y[i] = Y[0] + 78;
                i++;
            }
            while (i < 9)
            {
                X[i] = X[i - 6];
                Y[i] = Y[0] + 2 * 78;
                i++;
            }
        }

        private void RuchGracza(int j)
        {
            try
            {
                FiguraKrzyzyk.DodajDoPlanszy(plansza, ref tablicaSymboli, j);
                ruchGracza = false;
                Linia1.Tresc = "Ruch przeciwnika";
                Linia1.X = 190;
                Linia2.Tresc = "";
                Linia3.Tresc = "";
                licznik = 0;
            }
            catch
            {
                Linia3.Tresc = "Wybrane przez Ciebie pole jest zajete. Sprobuj jeszcze raz.";
                Linia3.X = 40;
            }
        }

        private void RuchPrzeciwnika()
        {
            if (licznik > 200)
            {
                licznik = 0;
                try
                {
                    FiguraKolko.DodajDoPlanszy(plansza, ref tablicaSymboli);
                    ruchGracza = true;
                    Linia1.Tresc = "Twoj ruch.";
                    Linia1.X = 220;
                    Linia2.Tresc = "Wybierz numer pola, na ktore chcesz wstawic symbol.";
                    Linia2.X = 65;
                    Linia3.Tresc = "";
                }
                catch
                {
                    Linia2.Tresc = "Przeciwnik wybral zajete pole. Za chwile sprobuje jeszcze raz.";
                    Linia2.X = 40;
                }
            }
        }

        private bool SprawdzKoniecGry(int wynikDodawaniaWLini)
        {
            if (SprawdzLinie(tablicaSymboli, 1, 3, 0, wynikDodawaniaWLini)) return true;
            if (SprawdzLinie(tablicaSymboli, 4, 9, 0, wynikDodawaniaWLini)) return true;
            if (SprawdzLinie(tablicaSymboli, 3, 7, 0, wynikDodawaniaWLini)) return true;
            if (SprawdzLinie(tablicaSymboli, 3, 8, 1, wynikDodawaniaWLini)) return true;
            if (SprawdzLinie(tablicaSymboli, 2, 7, 2, wynikDodawaniaWLini)) return true;
            if (SprawdzLinie(tablicaSymboli, 3, 9, 2, wynikDodawaniaWLini)) return true;
            if (SprawdzLinie(tablicaSymboli, 1, 6, 3, wynikDodawaniaWLini)) return true;
            if (SprawdzLinie(tablicaSymboli, 1, 9, 6, wynikDodawaniaWLini)) return true;
            return false;
        }

        private bool SprawdzLinie(int[] tab, int skok, int zakres, int start, int wynik)
        {
            int j = start;
            int spr = 0;
            while (j < zakres)
            {
                spr += tab[j];
                j += skok;
            }
            if (wynik == spr) return true;
            else return false;
        }
    }
}
