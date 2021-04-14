using System.Collections.Generic;
using System.Windows.Forms;

namespace CommisVoyageur
{
    public partial class MainForm : Form
    {
        int minLength;
        int countElems = 0;
        bool FindKP(int indexFirst,int indexSecond,int length, List<Path> blockedPaths)
        {
            textBox2.Text += "Entered point: " + indexFirst + " L:" + length;
            foreach (Path pathQWE in blockedPaths)
            {
                textBox2.Text += " {" + pathQWE.StartPointIndex + ";" + pathQWE.EndPointIndex + "} ";
            }
            textBox2.Text += "\r\n";

            /////////////////////////////////////////////////////////////////////////////////////////
            bool isFork = false;
            foreach (Path path in points[indexFirst].paths)
            {
                textBox2.Text += "Entered path: " + path.StartPointIndex + " -> " + path.EndPointIndex + " L: " + length + "\r\n";
                if (isFork)
                {
                    for (int i = 0; i < countElems; i++)
                    {
                        if (blockedPaths.Count > 0)
                        {
                            blockedPaths.RemoveAt(blockedPaths.Count - 1);
                        }
                    }
                    length -= path.Length;
                    countElems = 0;
                }
                if (!CheckPathRepeat(blockedPaths, path))
                {
                    FinishPath(path, ref length, ref blockedPaths);
                    if ((path.EndPointIndex == indexSecond) && ((minLength == -1) || (minLength > length)))
                    {
                        minLength = length;
                    }
                    else
                    {
                        FindKP(path.EndPointIndex, indexSecond, length, blockedPaths);
                    }
                }
                isFork = true;
                textBox2.Text += "Quited path: " + path.StartPointIndex + " -> " + path.EndPointIndex + " L: " + length + "\r\n";
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            textBox2.Text += "Quited point: " + indexFirst + " L:" + length;
            foreach (Path pathQWE in blockedPaths)
            {
                textBox2.Text += " {" + pathQWE.StartPointIndex + ";" + pathQWE.EndPointIndex + "} ";
            }
            textBox2.Text += "\r\n";


            return true;
        }


        private void FinishPath(Path path, ref int length, ref List<Path> blockedPaths)
        {
            length += path.Length;
            blockedPaths.Add(path);
            countElems++;
        }

        public bool CheckPathRepeat(List<Path> blockedPaths, Path pathToCheck)
        {
            foreach(Path pathFromList in blockedPaths)
            {
                if (pathToCheck.isEqualTo(pathFromList) || pathToCheck.isEqualTo(pathToCheck.GetReversedPath(points)))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
