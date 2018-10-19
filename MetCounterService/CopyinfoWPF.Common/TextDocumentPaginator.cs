using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        List<List<FormattedText>> pages = new List<List<FormattedText>>();

        public double MarginTop { get; set; } = 30;

        public double MarginLeft { get; set; } = 30;

        public double MarginRight { get; set; } = 30;

        public double MarginBottom { get; set; } = 30;

        private int _rowsPerPage = 0;

        private int _pageCount => pages.Count;

        public TextDocumentPaginator(string text, double printableAreaWidth, double printableAreaHeight)
            : this(text, new Size(printableAreaWidth, printableAreaHeight))
        {

        }

        public TextDocumentPaginator(IEnumerable<string> documents, double printableAreaWidth, double printableAreaHeight)
        {
            PageSize = new Size(printableAreaWidth, printableAreaHeight);
            foreach (string document in documents)
            {
                AddDocument(document);
            }
        }

        public TextDocumentPaginator(string text, Size size)
        {
            PageSize = size;
            AddDocument(text);
        }

        public void AddDocument(string text)
        {
            AddLines(text);
        }

        private void AddLines(string text)
        {
            var lines = text.Split(Environment.NewLine.ToCharArray());
            var formattedLines = RenderLines(lines);

            pages.AddRange(SplitToPages(formattedLines));

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


        public IEnumerable<FormattedText> RenderLines(IEnumerable<string> lines)
        {
            foreach (var line in lines)
                foreach (var renderredLines in RenderLine(line))
                    yield return renderredLines;
        }

        public IEnumerable<FormattedText> RenderLine(string text)
        {
            
            var fText = FormatText(text, 90000);
            int maxWidth = (int)(PageSize.Width - MarginLeft - MarginRight);    //double v = 1.999999;
                                                                                //int i = (int)v;
                                                                                //Console.WriteLine(i); 
                                                                                //Output: 1                                                         
            if (fText.Width > maxWidth)
            {
                var bestOption = (int)((maxWidth / fText.Width) * text.Length) - 4; // -4 just to be sure.

                foreach (var line in SplitTooLongLine(text, bestOption))
                {
                    yield return FormatText(line + "-", maxWidth);
                }
            }
            else
            {
                yield return fText;
            }
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
