using System.Collections;
using System.Windows.Forms;
using System;

/// <summary>
/// This class is an implementation of the 'IComparer' interface.
/// </summary>
public class ListViewSorter : IComparer
{
    /// <summary>
    /// Specifies the column to be sorted
    /// </summary>
    private int ColumnToSort;
    /// <summary>
    /// Specifies the order in which to sort (i.e. 'Ascending').
    /// </summary>
    private SortOrder OrderOfSort;
    /// <summary>
    /// Case insensitive comparer object
    /// </summary>
    private CaseInsensitiveComparer ObjectCompare;

    private int[] columnsWithInt = new int[] { };
    private int[] columnsWithDate = new int[] { };
    Copyinfo.Forms.Controls.ListView.TBListViewItem.AdditionalItemClassType additionalItemType = Copyinfo.Forms.Controls.ListView.TBListViewItem.AdditionalItemClassType.None;

    /// <summary>
    /// Class constructor.  Initializes various elements
    /// </summary>
    public ListViewSorter()
    {
        // Initialize the column to '0'
        ColumnToSort = 0;

        // Initialize the sort order to 'none'
        OrderOfSort = SortOrder.None;

        // Initialize the CaseInsensitiveComparer object
        ObjectCompare = new CaseInsensitiveComparer();
    }

    public void SetIntegers(int[] columnsWithInt)
    {
        this.columnsWithInt = columnsWithInt;
    }

    public void SetDates(int[] columnsWithDateTime)
    {
        this.columnsWithDate = columnsWithDateTime;
    }

    public void SetAdditionalItemClass(Copyinfo.Forms.Controls.ListView.TBListViewItem.AdditionalItemClassType classType)
    {
        this.additionalItemType = classType;
    }


    /// <summary>
    /// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
    /// </summary>
    /// <param name="x">First object to be compared</param>
    /// <param name="y">Second object to be compared</param>
    /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
    public int Compare(object x, object y)
    {
        int compareResult = 0;
        Copyinfo.Forms.Controls.ListView.TBListViewItem listviewX, listviewY;

        // Cast the objects to be compared to ListViewItem objects
        listviewX = (Copyinfo.Forms.Controls.ListView.TBListViewItem)x;
        listviewY = (Copyinfo.Forms.Controls.ListView.TBListViewItem)y;

        string a = listviewX.SubItems[ColumnToSort].Text;
        string b = listviewY.SubItems[ColumnToSort].Text;

        // Compare the two items
        if (ItsColumnWithInt(ColumnToSort))
            compareResult = ObjectCompare.Compare(int.Parse(a), int.Parse(b));
        else
        if (ItsColumnWithDate(ColumnToSort))
        {
            switch(this.additionalItemType)
            {
                case Copyinfo.Forms.Controls.ListView.TBListViewItem.AdditionalItemClassType.None:
                    compareResult = 0;
                    break;
                case Copyinfo.Forms.Controls.ListView.TBListViewItem.AdditionalItemClassType.MachineRecord:
                    Copyinfo.Database.MachineRecord recordX = (Copyinfo.Database.MachineRecord)listviewX.additionalItem;
                    Copyinfo.Database.MachineRecord recordY = (Copyinfo.Database.MachineRecord)listviewY.additionalItem;
                    compareResult = DateTime.Compare(recordX.datetime, recordY.datetime);
                    break;
                case Copyinfo.Forms.Controls.ListView.TBListViewItem.AdditionalItemClassType.Device:
                    Copyinfo.Database.Device deviceX = (Copyinfo.Database.Device)listviewX.additionalItem;
                    Copyinfo.Database.Device deviceY = (Copyinfo.Database.Device)listviewY.additionalItem;
                    compareResult = DateTime.Compare(deviceX.instalation_datetime, deviceY.instalation_datetime);
                    break;
            }
        }
        else
        {
            compareResult = ObjectCompare.Compare(a, b);
        }

        // Calculate correct return value based on object comparison
        if (OrderOfSort == SortOrder.Ascending)
        {
            // Ascending sort is selected, return normal result of compare operation
            return compareResult;
        }
        else if (OrderOfSort == SortOrder.Descending)
        {
            // Descending sort is selected, return negative result of compare operation
            return (-compareResult);
        }
        else
        {
            // Return '0' to indicate they are equal
            return 0;
        }

        //int compareResult;
        //System.Windows.Forms.ListViewItem listviewX, listviewY;

        //// Cast the objects to be compared to ListViewItem objects
        //listviewX = (System.Windows.Forms.ListViewItem)x;
        //listviewY = (System.Windows.Forms.ListViewItem)y;

        //string a = listviewX.SubItems[ColumnToSort].Text;
        //string b = listviewY.SubItems[ColumnToSort].Text;

        //// Compare the two items
        //if (itsColumnWithInt(ColumnToSort))
        //    compareResult = ObjectCompare.Compare(int.Parse(a), int.Parse(b));
        //else
        //if (itsColumnWithDate(ColumnToSort))
        //{
        //    Copyinfo.Forms.TBListViewItem_MachineRecord recordA = (Copyinfo.Forms.TBListViewItem_MachineRecord)x;
        //    Copyinfo.Forms.TBListViewItem_MachineRecord recordB = (Copyinfo.Forms.TBListViewItem_MachineRecord)y;
        //    compareResult = DateTime.Compare(recordA.record.datetime, recordB.record.datetime);
        //}
        //else
        //{
        //    compareResult = ObjectCompare.Compare(a, b);
        //}

        //// Calculate correct return value based on object comparison
        //if (OrderOfSort == SortOrder.Ascending)
        //{
        //    // Ascending sort is selected, return normal result of compare operation
        //    return compareResult;
        //}
        //else if (OrderOfSort == SortOrder.Descending)
        //{
        //    // Descending sort is selected, return negative result of compare operation
        //    return (-compareResult);
        //}
        //else
        //{
        //    // Return '0' to indicate they are equal
        //    return 0;
        //}
    }


    private bool ItsColumnWithInt(int index)
    {
        for (int i = 0; i < this.columnsWithInt.Length; i++)
        {
            if (columnsWithInt[i] == index)
                return true;
        }
        return false;
    }

    private bool ItsColumnWithDate(int index)
    {
        for (int i = 0; i < this.columnsWithDate.Length; i++)
            if (columnsWithDate[i] == index)
                return true;
        return false;
    }

    /// <summary>
    /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
    /// </summary>
    public int SortColumn
    {
        set
        {
            ColumnToSort = value;
        }
        get
        {
            return ColumnToSort;
        }
    }

    /// <summary>
    /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
    /// </summary>
    public SortOrder Order
    {
        set
        {
            OrderOfSort = value;
        }
        get
        {
            return OrderOfSort;
        }
    }

}