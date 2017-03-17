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

        public static int calculateHeight(int formHeight, int freeSpaceAtTop)
        {
            int finalHeight = formHeight - freeSpaceAtTop;
            if (finalHeight > 0)
                return finalHeight;
            else
                return 0;
        }

        public static void calculateHeight(UserControl control, Form form, int freeSpaceAtTop)
        {
            control.Height = calculateHeight(form.Height, freeSpaceAtTop);
        }

        public static void calculateHeight(UserControl control, UserControl mainControl, int freeSpaceAtTop)
        {
            control.Height = calculateHeight(mainControl.Height, freeSpaceAtTop);
        }
    }
}
