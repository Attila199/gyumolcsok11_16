using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gyumolcsok1
{
    internal class gyumolcsok22
    {
        int id;
        string nev;
        double ar;
        int db;

        public int Id { get => id; set => id = value; }
        public string Nev { get => nev; set => nev = value; }
        public double Ar { get => ar; set => ar = value; }
        public int Db { get => db; set => db = value; }

        public gyumolcsok22(int id, string nev, double ar, int db)
        {
            Id = id;
            Nev = nev;
            Ar = ar;
            Db = db;
        }
    }
}
