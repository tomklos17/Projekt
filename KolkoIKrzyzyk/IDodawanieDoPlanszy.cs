using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolkoIKrzyzyk
{
    public interface IDodawanieDoPlanszy
    {
        void DodajDoPlanszy(SortedList<int, Figura> plansza, ref int[] tablicaSymboli, int i);

        void DodajDoPlanszy(SortedList<int, Figura> plansza, ref int[] tablicaSymboli);
    }
}
