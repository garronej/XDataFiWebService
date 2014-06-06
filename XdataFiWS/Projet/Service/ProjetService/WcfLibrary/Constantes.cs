using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfLibrary
{
    static public class Constantes
    {
        static public int DEBUG = 6;

        static public void displayDEBUG(string msg, int lvl)
        {
            if (DEBUG > lvl)
            {
                string s = "";
                for (int i = 0; i < lvl; i++)
                {
                    s += "      ";
                }
                s += msg;

                Console.WriteLine(s);
            }
        }
    }

}
