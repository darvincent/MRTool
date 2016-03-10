using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace MRTools
{
    public class ListViewItemsComparer : IComparer
    {
        private int col;
        private bool m_Asc;
        private int dataType = 0;
        public ListViewItemsComparer()
        {
        }
        public ListViewItemsComparer(int column,int dataType, bool p_Asc)
        {
            col = column;
            m_Asc = p_Asc;
            this.dataType = dataType;
        }
        public int Compare(object x, object y)
        {
            string compStrX = ((ListViewItem)x).SubItems[col].Text.Trim(' ');
            string compStrY = ((ListViewItem)y).SubItems[col].Text.Trim(' ');
            if (m_Asc)
            {
                return myStrCmp(compStrX.Trim(' '), compStrY.Trim(' '));
            }
            else
            {
                return myStrCmp(compStrY.Trim(' '), compStrX.Trim(' '));
            }
        }
        private int myStrCmp(string strA, string strB)
        {
            try
            {
                switch (dataType)
                {
                        // string
                    case 0:
                        {
                            if (String.Compare(strA, strB) != 0)
                            {
                                return String.Compare(strA, strB);
                            }
                            else
                            {
                                return 0;
                            }
                        }
                        //int
                    case 1:
                        {
                            int A = Int32.Parse(strA);
                            int B = Int32.Parse(strB);
                            if (A <= B)
                            {
                                return 0;
                            }
                            else
                            {
                                return 1;
                            }
                        }
                        //datetime
                    case 2:
                        {
                            DateTime d1 = Convert.ToDateTime(strA);
                            DateTime d2 = Convert.ToDateTime(strB);
                            if (d1.CompareTo(d2) <= 0)
                            {
                                return 0;
                            }
                            else
                            {
                                return 1;
                            }
                        }
                    default:
                        return 0;
                }
            }
            catch(Exception ex)
            {
                utility.logWriter.writeErrorLog(ex);
                return 0;
            }
        }
    }
}
