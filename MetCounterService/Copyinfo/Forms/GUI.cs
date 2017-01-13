using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace Copyinfo.Forms
{
    class GUI
    {
        public static void AlignTextBoxes(int[] widith, TextBox[] textBoxes, int y, int padding = 0)
        {
            for(int i = 0; i < widith.Length; i ++)
            {
                textBoxes[i].Width = widith[i];
                textBoxes[i].Location = new System.Drawing.Point(
                    GetSumOfPadding(textBoxes, i) + padding,
                    y);
            }
        }

        private static int GetSumOfPadding(TextBox[] textBoxes, int lastTextBox)
        {
            int padding = 0;
            for (int i = 0; i < lastTextBox; i++)
            {
                padding += textBoxes[i].Width;
            }

            return padding;
        }
    }
}
