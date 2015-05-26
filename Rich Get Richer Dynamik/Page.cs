using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rich_Get_Richer_Dynamik
{
    public class Page
    {
        public int miPageNumber;
        public int miInlinks;
        public int miDecision;
        public int miCopiedFrom;
        public double mdProb;
        public Page mPointToPage;

        private static readonly Random rng = new Random();
        private static readonly object SyncLock = new object();

        public Page(int PageNumber)
        {
            miPageNumber = PageNumber;
            miInlinks = 0;
            mdProb = 0.00f;
        }

        public int getPageNumber()
        {
            return miPageNumber;
        }

        public int getPointedPageNumber()
        {
            return mPointToPage.miPageNumber;
        }

        public int getInlinks()
        {
            return miInlinks;
        }

        public string getDecision()
        {
            if(miDecision == 0){
                return "Eigene Entscheidung";
            }
            else
            {
                return "Kopierte Entscheidung";
            }
        }

        public double getProb()
        {
            return mdProb;
        }

        public void CalcNewProb()
        {
            mdProb = Math.Round((double)miInlinks / MainWindow.AllPages.Count, 2);
        }

        public void IncrementInlinks()
        {
            miInlinks++;
        }

        public static int getRNG(int min, int max)
        {
            lock (SyncLock)
            {
                return rng.Next(min, max);
            }
        }

        public void CreatNewPage()
        {
            #region MOD = a+b
            if (MainWindow.sMod == "a+b")
            {
                //2. Random Number
                int iRnd = getRNG(1, 101);
                int iGlobalP = Convert.ToInt32(Math.Round(MainWindow.P * 100, 2));

                if(iRnd <= iGlobalP)
                {
                    //Eigene Entscheidung
                    MainWindow.iOwnDecs++;
                    miDecision = 0;

                    //newpage wählt die Page aus, auf die newpage verlinken wird
                    int iRndPage = getRNG(1, MainWindow.AllPages.Count);
                    mPointToPage = MainWindow.AllPages[iRndPage - 1];
                    mPointToPage.IncrementInlinks();
                }
                else
                {
                    //Kopierte Entscheidung
                    MainWindow.iCopyDecs++;
                    miDecision = 1;

                    //newpage wählt die Page aus, dessen Entscheidung kopiert wird
                    int iRndPage = getRNG(1, MainWindow.AllPages.Count);
                    mPointToPage = MainWindow.AllPages[iRndPage - 1].mPointToPage;
                    mPointToPage.IncrementInlinks();
                    miCopiedFrom = iRndPage;
                }
            }
            #endregion

            #region MOD = c
            if (MainWindow.sMod == "c")
            {

            }
            #endregion
        }
    }
}
