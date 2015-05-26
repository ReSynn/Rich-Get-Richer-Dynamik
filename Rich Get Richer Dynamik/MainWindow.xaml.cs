using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rich_Get_Richer_Dynamik
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Page> AllPages = new List<Page>();
        public static List<Page> PoolPages = new List<Page>();
        public static List<Page> NewPages = new List<Page>();

        //P <=> p;
        public static double P;

        public static int iCreatedPages;
        public static int iCreatedInlinks;
        public static int iOwnDecs;
        public static int iCopyDecs;

        public static int iInitialPoolPages;
        public static int iNewPages;

        public static string sMod;
        public static string sP;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAllStep_Click(object sender, RoutedEventArgs e)
        {
            setStartValues();

            for (int i = PoolPages.Count+1; i < iNewPages+1; i++)
            {
                CalcP();

                Page newpage = new Page(i);
                newpage.CreatNewPage();

                AllPages.Add(newpage);
                NewPages.Add(newpage);

                iCreatedPages++;
                iCreatedInlinks++;
            }

            setEndResults();
        }

        private void btnOneStep_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            AllPages.Clear();
            PoolPages.Clear();
            NewPages.Clear();

            iCreatedPages = 0;
            iCreatedInlinks = 0;
            iOwnDecs = 0;
            iCopyDecs = 0;
            
            iInitialPoolPages = 0;
            iNewPages = 0;

            txtboxDecisions.Clear();
            txtboxPointsTo.Clear();
            txtboxInlinks.Clear();
            txtboxqofSite.Clear();

            txtboxPages.Clear();
            txtboxInlinksEnd.Clear();
            txtboxOwnDecs.Clear();
            txtboxCopyDecs.Clear();
            txtboxp.Clear();
        }


        public void CalcP()
        {
            if (sP == "9/10")
            {
                P = (double)0.9;
            }

            if(sP == "1/2")
            {
                P = (double)0.5;
            }

            if (sP == "1/10")
            {
                P = (double)0.1;
            }

            if(sP == "1/N")
            {
                P = (double)1 / AllPages.Count;
            }
            txtboxp.Text = P.ToString();
        }


        public void setStartValues() 
        {
            iInitialPoolPages = Convert.ToInt32(txtboxPool.Text);
            iNewPages = Convert.ToInt32(txtboxNewPages.Text);
            sMod = cmbboxMod.Text;
            sP = cmbboxP.Text;

            for (int a = 1; a < iInitialPoolPages+1; a++)
            {
                Page newPoolPage = new Page(a);
                newPoolPage.mPointToPage = newPoolPage;
                newPoolPage.miDecision = 0;
                newPoolPage.IncrementInlinks();

                AllPages.Add(newPoolPage);
                PoolPages.Add(newPoolPage);
              
                //FillAllTextBoxes(newPoolPage);

                iCreatedPages++;
                iCreatedInlinks++;
                iOwnDecs++;
            }
            /*
            txtboxqofSite.Clear();
            foreach (Page b in AllPages)
            {
                b.CalcNewProb();
                FillqTextBox(b);
            }
             * */
        }

        public void setEndResults()
        {
            txtboxPages.Text = iCreatedPages.ToString();
            txtboxInlinksEnd.Text = iCreatedInlinks.ToString();
            txtboxOwnDecs.Text = iOwnDecs.ToString();
            txtboxCopyDecs.Text = iCopyDecs.ToString();

            foreach (Page p in AllPages)
            {
                p.CalcNewProb();
                FillAllTextBoxes(p);
            }
        }


        public void FillAllTextBoxes(Page p)
        {
            FillDecisionTextBox(p);
            FillPointsToTextBox(p);
            FillInlinksTextBox(p);
            FillqTextBox(p);
        }

        public void ClearAllTextBoxes()
        {
            txtboxDecisions.Clear();
            txtboxPointsTo.Clear();
            txtboxInlinks.Clear();
            txtboxqofSite.Clear();
        }



        #region TextBoxesFilling
        public void FillDecisionTextBox(Page a)
        {
            int iPageNumber = a.getPageNumber();
            string sPageNumber = iPageNumber.ToString();
            string sDecision = a.getDecision();

            txtboxDecisions.Text += "Page " + sPageNumber + ": " + sDecision + "\n";
        }

        public void FillPointsToTextBox(Page a)
        {
            int iPageNumber = a.getPageNumber();
            string sPageNumber = iPageNumber.ToString();
            int iPagePointerNumber = a.mPointToPage.getPageNumber();
            string sPagePointerNumber = iPagePointerNumber.ToString();

            if(a.getDecision() == "Eigene Entscheidung")
            {
                txtboxPointsTo.Text += "Page " + sPageNumber + " zeigt auf: Page " + sPagePointerNumber + " \n";
            }

            if (a.getDecision() == "Kopierte Entscheidung")
            {
                int iCopiedPage = a.miCopiedFrom;
                string s = iCopiedPage.ToString();
                txtboxPointsTo.Text += "Page " + sPageNumber + " zeigt auf: Page " + sPagePointerNumber + " [C" + s + "]" +  " \n";
            }

            
        }

        public void FillInlinksTextBox(Page a)
        {
            int iPageNumber = a.getPageNumber();
            string sPageNumber = iPageNumber.ToString();
            int iInlinks = a.getInlinks();
            string sInlinks = iInlinks.ToString();

            if(iInlinks <= 1)
            {
                txtboxInlinks.Text += "Page " + sPageNumber + " bestitzt " + sInlinks + " Inlink" + " \n";
            }
            else
            {
                txtboxInlinks.Text += "Page " + sPageNumber + " bestitzt " + sInlinks + " Inlinks" + " \n";
            }
        }
        
        public void FillqTextBox(Page a)
        {
            int iPageNumber = a.getPageNumber();
            string sPageNumber = iPageNumber.ToString();
            double dProb = a.getProb();
            string sProb = dProb.ToString();

            txtboxqofSite.Text += "q von Page " + sPageNumber + ":    " + sProb + " \n";
        }
        #endregion

    }
}
