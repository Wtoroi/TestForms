using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsForms
{
    class FormColors
    {
        private static String[] colorList = { "#008aff", "#ff005a", "#ff9e2b", "#10df00", "#851df0" };

        public static Color GetRandomColor()
        {
            Random rnd = new Random();
            return ColorTranslator.FromHtml(colorList[rnd.Next(0, colorList.Length)]);
        }
    }
}
