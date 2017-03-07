using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Globalization;

namespace Copyinfo.Forms.Controls.ListView
{
    public class TBListView : System.Windows.Forms.ListView
    {
        IEnumerable<TBListViewItem> allListViewBuffered;

        TBListViewItem[] buffor_array;

        ListViewSorter sorter = new ListViewSorter();

        int[] columnsWithDateSorting = new int[] {};

        Copyinfo.Forms.Controls.ListView.TBListViewItem.AdditionalItemClassType additionalItemClass = TBListViewItem.AdditionalItemClassType.None;

        string[] filters;
        bool filtersWasClear = true;

        public TBListView() : base()
        {
            
            this.Font = Style.listViewFont;
            this.View = View.Details;
            this.FullRowSelect = true;

            filters = new string[this.Columns.Count];
            allListViewBuffered = this.Items.Cast<TBListViewItem>();
            this.ColumnClick += listView1_ColumnClick;

            filters = new string[9];
            for (int i = 0; i < filters.Length; i++)
                filters[i] = "";
        }

        public void setAdditionalClass(TBListViewItem.AdditionalItemClassType additionalClass)
        {
            this.additionalItemClass = additionalClass;
        }

        public void setSorter (ListViewSorter sorter)
        {
            this.sorter = sorter;
            this.ListViewItemSorter = this.sorter;
        }

        public void setColumnsWithDate(int[] columns)
        {
            this.columnsWithDateSorting = columns;
        }

        private bool filterByDate(int indexOfFilter, object itemToMatch)
        {
            DateTime itemDatetime = new DateTime();

            switch(additionalItemClass)
            {
                case TBListViewItem.AdditionalItemClassType.Device:
                    Database.Device convertedDevice = (Database.Device)itemToMatch;
                    itemDatetime = convertedDevice.instalation_datetime;
                    break;

                case TBListViewItem.AdditionalItemClassType.MachineRecord:
                    Database.MachineRecord record = (Database.MachineRecord)itemToMatch;
                    itemDatetime = record.datetime;
                    break;
                case TBListViewItem.AdditionalItemClassType.None:
                    throw new Exception("Błąd podczas sortowania. Przy sortowaniu podano kolumnę z datą ale nie ustawiono jaki typ obiektu jest zawarty w dodatkowym przedmiocie.");
            }

            if (filters[indexOfFilter].Contains("-"))
            {
                string[] parts = filters[indexOfFilter].Split('-');

                DateTime olderDate = DateTime.ParseExact(parts[0], Style.DateTimeFormat, Style.cultureInfo);
                DateTime youngerDate = DateTime.ParseExact(parts[1], Style.DateTimeFormat, Style.cultureInfo);

                olderDate = DateTime.SpecifyKind(olderDate, DateTimeKind.Utc);
                youngerDate = DateTime.SpecifyKind(youngerDate, DateTimeKind.Utc);      

                if (DateTime.Compare(olderDate.Date, itemDatetime.Date) <= 0 && DateTime.Compare(youngerDate.Date, itemDatetime.Date) >= 0)
                    return true;
                else
                    return false;
            }

            return false;
        }

        private bool checkFilter(TBListViewItem item)
        {
            bool match = false;

            for (int i = 0; i < item.SubItems.Count; i++)
                if (filters[i] != "")
                    if (columnsWithDateSorting.Contains(i))
                        match = filterByDate(i, item.additionalItem);
                    else
                        if (item.SubItems[i].Text.Contains(filters[i]))
                            match = true;
                        else
                            return false;

            return match;
        }

        public void Clear()
        {
            this.Items.Clear();
            this.buffor_array = new TBListViewItem[0];
        }

        private bool filtersAreClear()
        {
            bool areThyeReallyClear = true;
            foreach (string filter in filters)
            {
                if (filter == "")
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }

            return areThyeReallyClear;
        }

        private void clearFilters()
        {
                this.Items.Clear();
                this.Items.AddRange(this.buffor_array);
                filtersWasClear = true;
        }

        public void filter(int filterNumber, string Value)
        {
            filters[filterNumber] = Value;
            if (Value == "")
            {
                if (filtersAreClear())
                {
                    clearFilters();
                    return;
                }
            }

            if (filtersWasClear)
            {
                filtersWasClear = false;
                buffor_array = new TBListViewItem[Items.Count];
                //Items.CopyTo(buffor_array, 0);

                for(int itemIndex = 0; itemIndex < Items.Count; itemIndex ++)
                {
                    //TBListViewItem source = (TBListViewItem)Items[itemIndex];
                    TBListViewItem item = (TBListViewItem)Items[itemIndex].Clone();
                    buffor_array[itemIndex] = item;
                }

                allListViewBuffered = buffor_array.Cast<TBListViewItem>();
                
            }

            Items.Clear();

            var matchedItems = from TBListViewItem in allListViewBuffered
                               where
                               checkFilter(TBListViewItem)
                                //((filters[1] != "") && TBListViewItem.subitems[0].text.contains(filters[0]))
                                //|| ((filters[1] != "") && TBListViewItem.subitems[1].text.contains(filters[1]))
                                //|| ((filters[1] != "") && TBListViewItem.subitems[2].text.contains(filters[2]))
                                //|| ((filters[1] != "") && TBListViewItem.subitems[3].text.contains(filters[3]))
                                //|| ((filters[1] != "") && TBListViewItem.subitems[4].text.contains(filters[4]))
                                //|| ((filters[1] != "") && TBListViewItem.subitems[5].text.contains(filters[5]))
                                //|| ((filters[1] != "") && TBListViewItem.subitems[6].text.contains(filters[6]))
                               select TBListViewItem;


            foreach (TBListViewItem item in matchedItems)
                this.Items.Add((TBListViewItem)item.Clone());

        }

        public int[] getColumnSizeHeaders()
        {
            int[] headersSize = new int[this.Columns.Count];
            for (int i = 0; i < this.Columns.Count; i++)
            {
                headersSize[i] = this.Columns[i].Width;
            }

            return headersSize;
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == sorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (sorter.Order == SortOrder.Ascending)
                {
                    sorter.Order = SortOrder.Descending;
                }
                else
                {
                    sorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                sorter.SortColumn = e.Column;
                sorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.Sort();
        }
    }
}
