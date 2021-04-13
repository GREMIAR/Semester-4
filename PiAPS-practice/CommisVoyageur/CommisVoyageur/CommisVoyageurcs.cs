using System.Collections.Generic;
using System.Windows.Forms;

namespace CommisVoyageur
{
    public partial class MainForm : Form
    {
        int minLength;
        public bool FindKP(int indexFirst,int indexSecond,int length, List<int> indexs)
        {
            foreach (Path path in points[indexFirst].paths)
            {
                
                length += path.Length;
                if (path.IndexPoint==indexSecond)
                {
                    if (minLength == -1)
                    {
                        minLength = length;
                    }
                    else if (minLength>length)
                    {
                        
                        minLength = length;
                    }
                }
                else
                {
                    if(!CheckPointRepeat(indexs,indexSecond))
                    {
                        indexs.Add(indexSecond);
                        FindKP(path.IndexPoint, indexSecond, length, indexs);
                    }
                }
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
