using System.Collections.Generic;
using System.Windows.Forms;

namespace CommisVoyageur
{
    public partial class MainForm : Form
    {
        int minLength;
        List<int> test1 = new List<int>();
        bool FindKP(int indexFirst,int indexSecond,int length, List<int> indexs1)
        {
            foreach (Path path in points[indexFirst].paths)
            {
               /*toolStripStatusLabel1.Text += ("&"+ indexFirst);
                toolStripStatusLabel1.Text += "!";
                foreach (int index in indexs1)
                {
                    toolStripStatusLabel1.Text += (index+";");
                }
                toolStripStatusLabel1.Text += "!";*/
                if (path.IndexPoint==indexSecond)
                {
                    length += path.Length;
                    if (minLength == -1)
                    {
                        indexs1.Add(path.IndexPoint);
                        test1 = indexs1;
                        minLength = length;
                    }
                    else if (minLength>length)
                    {
                        indexs1.Add(path.IndexPoint);
                        test1 = indexs1;
                        minLength = length;
                    }
                }
                else
                {
                    if(!CheckPointRepeat(indexs1, path.IndexPoint))
                    {
                        length += path.Length;
                        indexs1.Add(path.IndexPoint);
                        FindKP(path.IndexPoint, indexSecond, length, new List<int>(indexs1));
                    }
                }
                toolStripStatusLabel1.Text += indexFirst;
                toolStripStatusLabel1.Text += "!!";
                foreach (int index in indexs1)
                {
                    toolStripStatusLabel1.Text += (index + ";");
                }
                toolStripStatusLabel1.Text += "!!";
            }
            return true;
        }

        public bool CheckPointRepeat(List<int> indexs,int second)
        {
            foreach(int index in indexs)
            {
                if (second == index)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
