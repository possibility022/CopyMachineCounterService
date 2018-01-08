using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace CopyinfoWPF.Common
{
    public class TextDocumentPaginator : DocumentPaginator
    {

        #region DocumentPaginator

        public override bool IsPageCountValid => true;

        public override int PageCount => _pageCount;

        public override Size PageSize { get; set; }

        public override IDocumentPaginatorSource Source => null;

        public override DocumentPage GetPage(int pageNumber) => RenderPage(pageNumber);

        #endregion

        private const string AllCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        List<List<FormattedText>> pages;

        List<FormattedText> formattedLines;

        public double MarginTop { get; set; } = 10;

        public double MarginLeft { get; set; } = 10;

        public double MarginRight { get; set; } = 10;

        public double MarginBottom { get; set; } = 10;

        private int _rowsPerPage = 0;

        private int _pageCount => pages.Count;

        public TextDocumentPaginator(string text, double printableAreaWidth, double printableAreaHeight)
        {
            PageSize = new Size(printableAreaWidth, printableAreaHeight);
            List<string> lines = SplitLines(text);
            formattedLines = RenderLines(lines);
            pages = SplitToPages(formattedLines);
        }

        private List<string> SplitLines(string text)
        {
            List<string> lines = new List<string>();

            int lastIndex = 0;
            int newLineIndex = text.IndexOf(Environment.NewLine);

            while (newLineIndex >= 0)
            {
                lines.Add(text.Substring(lastIndex, newLineIndex - lastIndex));
                lastIndex = newLineIndex + Environment.NewLine.Length;
                newLineIndex = text.IndexOf(Environment.NewLine, lastIndex);
            }

            return lines;
        }

        private List<List<FormattedText>> SplitToPages(IEnumerable<FormattedText> formattedTexts)
        {
            List<List<FormattedText>> pages = new List<List<FormattedText>>();
            List<FormattedText> page = new List<FormattedText>();

            double currentPageHeight = 0;

            foreach (FormattedText formattedText in formattedTexts)
            {
                if (currentPageHeight + formattedText.Height > PageSize.Height - MarginTop - MarginBottom)
                {
                    pages.Add(page);
                    page = new List<FormattedText>();
                    currentPageHeight = 0;
                }

                currentPageHeight += formattedText.Height;
                page.Add(formattedText);
            }

            pages.Add(page);

            return pages;
        }

        private List<FormattedText> RenderLines(IEnumerable<string> lines)
        {
            List<FormattedText> fLines = new List<FormattedText>();

            foreach (string line in lines)
            {
                fLines.Add(GetFormattedText(line));
            }

            return fLines;
        }

        private FormattedText GetFormattedText(string text)
        {
            return new FormattedText(
            text,
            CultureInfo.CurrentCulture,
            FlowDirection.LeftToRight,
            new Typeface("Calibry"), 20, Brushes.Black)
            {
                MaxTextWidth = PageSize.Width - MarginLeft - MarginRight
            };
        }

        private DocumentPage RenderPage(int pageNumber)
        {
            DrawingVisual visual = new DrawingVisual();

            Point point = new Point(MarginTop, MarginLeft);

            using (DrawingContext dc = visual.RenderOpen())
            {
                int i = 0;
                foreach (FormattedText text in pages[pageNumber])
                {
                    dc.DrawText(pages[pageNumber][i], point);
                    point.Y += text.Height;
                    i++;
                }
            }

            return new DocumentPage(visual, PageSize, new Rect(PageSize), new Rect(PageSize));
        }
    }
}
