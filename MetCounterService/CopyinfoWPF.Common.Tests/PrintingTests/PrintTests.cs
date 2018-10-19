using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Media;
using System.Globalization;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CopyinfoWPF.Common.Tests.PrintingTests
{
    [TestClass]
    public class PrintTests
    {

        // 1654 A4 size

        const string Value = "sjhdlkajhwdlkjalkdjaklwjdhaklwjdhaoiwudhopiauhcdmoiuahwicudahnwiocdngawuiyncgiauywngdciuaygdiucaygniduyganciuwygaiuycgaiuywgdiauwygdiuaygwdiuyagwiduygaiudycagwiuydgcaiuywgdiacuywdgihfisdjghsiuefoiusheofuisheiufhsegfisugfsehfiusfivvsoiehosiufeH";

        [TestMethod]
        public void TestSplitingLine()
        {
            var fText = FormatText(Value, 90000);
            int maxWidth = 1654 - 30 - 30; // page size - maring - margin

            IEnumerable<string> lines= null;

            if (fText.Width > maxWidth)
            {
                var bestOption = ((maxWidth / fText.Width) * ((double)Value.Length)) - 4;
                lines = SplitTooLongLine(Value, (int)bestOption);
            }

            var sb = new StringBuilder();
            foreach (var str in lines)
                sb.Append(str);

            Assert.AreEqual(sb.ToString(), Value);
        }

        private IEnumerable<string> SplitTooLongLine(string text, int maxCharactersInLine)
        {
            int tail = maxCharactersInLine;
            int pointer;

            for (pointer = 0; pointer < text.Length; pointer += maxCharactersInLine)
            {
                if (pointer + maxCharactersInLine > text.Length)
                    tail = text.Length - pointer;

                yield return text.Substring(pointer, tail);
            }
        }

        private FormattedText FormatText(string text, double maxTextWidth = 90000)
        {
            return new FormattedText(
            text,
            CultureInfo.CurrentCulture,
            FlowDirection.LeftToRight,
            new Typeface("Calibry"), 20, Brushes.Black)
            {
                MaxTextWidth = maxTextWidth
            };
        }

    }
}
