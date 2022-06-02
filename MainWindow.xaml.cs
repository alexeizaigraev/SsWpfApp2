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
using System.Diagnostics;

namespace SsWpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        delegate void delegateMemu();

        void WorkMenu(delegateMemu parDelegate)
        {
            #region
            ClearMe();
            try
            {
                parDelegate();
                textBox1.Text = Papa.info;
                textBoxFname.Text = Papa.infoFname;
                textBoxErr.Text = Papa.infoErr;
            }
            catch (Exception ex) { textBox1.Text = ex.Message; }

            #endregion
        }

        public MainWindow()
        {
            #region #Init
            InitializeComponent();
            Papa.MainKind = "partners";
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var items = new List<string>() { "item1", "item2" };
            listBox1.ItemsSource = items;

            textBox1.Text = "";
            try
            {
                listBox1.ItemsSource = DbDepMeth.GetLisPartner();
            }
            catch (Exception ex) { textBox1.Text += ex.Message + "\n"; }
            labeInfo.Content = Papa.MainKind;
            #endregion
        }

        private void ClearMe()
        {
            #region
            textBox1.Text = "";
            textBoxFname.Text = "";
            textBoxErr.Text = "";
            Papa.info = "";
            Papa.infoFname = "";
            Papa.infoErr = "";
            #endregion
        }


        private void ButtonShowDep_Click(object sender, RoutedEventArgs e)
        {
            #region
            WindowDep windowDep = new WindowDep();
            windowDep.Show();
            #endregion
        }


        private void ButtonShowTerm_Click(object sender, RoutedEventArgs e)
        {
            #region
            WindowTerm windowTerm = new WindowTerm();
            windowTerm.Show();
            #endregion
        }


        // refresh __________________________________________________________

        private void RefreshFromAccess_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = RefreshFromAccess.MainRefreshFromAccess;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void RefreshDbToGrive_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = DbToGdrive.MainDbToGdrive;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void RefreshGdriveToDb_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = RefreshFromGdrive.MainRefreshFromGdrive;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void ButtonOtborShow_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = OtborShow.MainOtborShow;
            WorkMenu(myDelegatemenu);
            #endregion
        }


        private void ButtonOtborDepAllTerm_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = OtborDepAllTerm.MainOtborDepAllTerm;
            WorkMenu(myDelegatemenu);
            #endregion
        }


        private void ButtonOtborTextTerm_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = OtborTerm.MainOtborTerm;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void ButtonOtborFiscal_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = OtborFiscal.MainOtborFiscal;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void ButtonOtborSerial_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = OtborSerial.MainOtborSerial;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void ButtonOtborPartner_Click(object sender, RoutedEventArgs e)
        {
            #region
            textBox1.Text = "start...";
            string partner = listBox1.SelectedItem.ToString();
            OtborPartner.MainOtborPartner(partner);
            textBox1.Text = Papa.info;
            #endregion
        }


        private void ButtonChoise_Click(object sender, RoutedEventArgs e)
        {
            if (Papa.MainKind == "terminals")
            {
                #region
                try
                {
                    ClearMe();
                    var selectedItems = new List<string>();
                    var terms = listBox1.SelectedItems;
                    foreach (var item in terms)
                    {
                        selectedItems.Add(item.ToString());
                    }
                    DbOtborMet.AddOtborSome(selectedItems);
                    textBox1.Text = Papa.info;
                    listBox1.SelectedItems.Clear();
                    labeInfo.Content = Papa.MainKind;
                }
                catch (Exception ex) { textBox1.Text = ex.Message + "\n"; }
                #endregion
            }
            else if (Papa.MainKind == "partners")
            {
                #region
                try
                {
                    Papa.selectedItem = listBox1.SelectedItem.ToString();
                    textBox1.Text = Papa.selectedItem;
                    Papa.partnerChoised = Papa.selectedItem;


                    listBox1.ItemsSource = DbDepMeth.GetLisPartner();
                    Papa.MainKind = "departments";

                    labeInfo.Content = Papa.MainKind;
                }
                catch (Exception ex) { textBox1.Text = ex.Message + "\n"; }
                listBox1.SelectedItems.Clear();
                #endregion
            }
            else if (Papa.MainKind == "departments")
            {
                #region
                try
                {
                    ClearMe();
                    Papa.selectedItems = new List<string[]>();
                    var deps = listBox1.SelectedItems;
                    List<string> outArr = new List<string>();
                    foreach (var item in deps)
                    {
                        var dep = item.ToString();
                        var term = $"{dep}1";
                        var line = $"{term};{dep}".Split(';');
                        outArr.Add(dep);
                        Papa.selectedItems.Add(line);
                        //textBox1.Text += dep + "\n";
                    }
                    DbOtborMet.AddManyOtbor(Papa.selectedItems);
                    //textBox1.Text = Papa.info;
                    textBox1.Text = String.Join(" ", outArr);
                    listBox1.SelectedItems.Clear();
                    labeInfo.Content = Papa.MainKind;
                }
                catch (Exception ex) { textBox1.Text = ex.Message + "\n"; }
                #endregion
            }

        }

        private void Col1Terminals_Click(object sender, RoutedEventArgs e)
        {
            Papa.MainKind = "terminals";
            listBox1.ItemsSource = DbTermMeth.GetLisTerm();
            labeInfo.Content = Papa.MainKind;
        }

        private void Col1Departments_Click(object sender, RoutedEventArgs e)
        {
            Papa.MainKind = "departments";
            listBox1.ItemsSource = DbDepMeth.GetListDep();
            labeInfo.Content = Papa.MainKind;
        }

        private void Col1Partners_Click(object sender, RoutedEventArgs e)
        {
            Papa.MainKind = "partners";
            listBox1.ItemsSource = DbDepMeth.GetLisPartner();
            labeInfo.Content = Papa.MainKind;
        }

        private void Col1Folders_Click(object sender, RoutedEventArgs e)
        {
            Papa.MainKind = "folders";
            listBox1.ItemsSource = Papa.MkGdriveFolders();
            labeInfo.Content = Papa.MainKind;
        }



        /*

        //Otbor text___________________________________

        private void ButtonOtborTextTerm_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = OtborTextTerm.MainOtborTerm;
            WorkMenu(myDelegatemenu);
            #endregion
        }
        */

        //People___________________________________

        private void Priem_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = Priem.MainPriem;
            WorkMenu(myDelegatemenu);
            #endregion
        }


        //people__________________
        private void Otpusk_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = Otpusk.MainOtpusk;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void Perevod_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = Perevod.MainPerevod;
            WorkMenu(myDelegatemenu);
            #endregion
        }



        //butttons____________________________________

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            #region
            ClearMe();
            Papa.info = "";
            #endregion
        }


        //some______________________________

        private void Term_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = Term.MainTerm;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void HrOtbor_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = HrDep.MainHrDep;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void HrAb_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = HrDepAb.MainHrDepAb;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void Ekv_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = Ekv.MainEkv;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void Natasha_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = Natasha.MainNatasha;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void SiteNew_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = SiteNew.MainSiteNew;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        //Doc_______________________________


        private void Activaciya_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = Activaciya.MainActivaciya;
            WorkMenu(myDelegatemenu);
            #endregion
        }


        private void ActPeredachi_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = ActPeredachi.MainActPeredachi;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        // monitor _____________________________________________

        private void ButtonGetRP_Click(object sender, RoutedEventArgs e)
        {
            #region
            ClearMe();
            try
            {
                GetRp.MainGetRp();
            }
            catch (Exception ex) { Papa.info += ex.Message; }
            textBox1.Text = Papa.info;
            #endregion
        }


        private void ButtonRasklad_Click(object sender, RoutedEventArgs e)
        {
            #region
            ClearMe();
            try
            {
                Rasklad.MainRasklad();
                textBox1.Text = Papa.info;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            #endregion
        }




        //kabinet_______________________________

        private void Rro_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = Rro.MainRro;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void Pereezd_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = Pereezd.MainPereezd;
            WorkMenu(myDelegatemenu);
            #endregion
        }


        private void Otmena_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = Otmena.MainOtmena;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void Prro_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = Prro.MainPrro;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void Knigi_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = Knigi.MainKnigi;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        /*
        private void ActivTerm_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = ActivTerm.MainActivTerm;
            WorkMenu(myDelegatemenu);
            #endregion
        }



        //monitor________________________________
        private void Monitor_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = Monitor.MainMonitor;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        

        private void AccBack_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = AccBack.MainAccBack;
            //myDelegatemenu = AccessBackUp.MainAccessBackUp;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void GnetzCopy_Click(object sender, RoutedEventArgs e)
        {
            #region
            Papa.gnetzKind = "copy";
            delegateMemu myDelegatemenu;
            myDelegatemenu = Gnetz.MainGnetz;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void GnetzMovy_Click(object sender, RoutedEventArgs e)
        {
            #region
            Papa.gnetzKind = "movy";
            delegateMemu myDelegatemenu;
            myDelegatemenu = Gnetz.MainGnetz;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void GdriveBackUp_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = GdriveBackUp.MainGdriveBackUp;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        


        



        //show & windows ______________________________________

        private void ButtonShowDep_Click(object sender, RoutedEventArgs e)
        {
            #region
            WindowDep windowDep = new WindowDep();
            windowDep.Show();
            #endregion
        }

        private void ButtonShowTerm_Click(object sender, RoutedEventArgs e)
        {
            #region
            WindowTerm windowTerm = new WindowTerm();
            windowTerm.Show();
            #endregion
        }

        private void ButtonOtborShow_Click(object sender, RoutedEventArgs e)
        {
            #region
            ClearMe();
            textBox1.Text += "\n" + PgBase.OtborToText();
            #endregion
        }

        //otbor_____________________________

        private void ButtonOtborInputDep_Click(object sender, RoutedEventArgs e)
        {
            #region
            //ClearMe();
            //Papa.selectedItems = new List<string[]>();
            var inputDeps = textBox1.Text;
            OtborDb.MainOtborDb(inputDeps);
            //ClearMe();
            textBox1.Text = Papa.info;
            //listBox1.SelectedItems.Clear();

            #endregion
        }

        private void ButtonOtborInputDepList_Click(object sender, RoutedEventArgs e)
        {
            #region
            var inputDeps = textBox1.Text;
            OtborHardInputDep.MainOtborHardDep(inputDeps);
            //ClearMe();
            textBox1.Text = Papa.info;
            //listBox1.SelectedItems.Clear();

            #endregion
        }

        private void ButtonOtborInputTermHard_Click(object sender, RoutedEventArgs e)
        {
            #region
            //ClearMe();
            //Papa.selectedItems = new List<string[]>();
            var inputDeps = textBox1.Text;
            OtborHardInput.MainOtborHard(inputDeps);
            //ClearMe();
            textBox1.Text = Papa.info;
            //listBox1.SelectedItems.Clear();
            #endregion
        }

        private void ButtonOtborInputSerialHard_Click(object sender, RoutedEventArgs e)
        {
            #region
            var input = textBox1.Text;
            OtborHardInputSerial.MainOtborHardSerial(input);
            //ClearMe();
            textBox1.Text = Papa.info;
            //listBox1.SelectedItems.Clear();

            #endregion
        }

        private void ButtonOtborInputHvostZn_Click(object sender, RoutedEventArgs e)
        {
            var input = textBox1.Text;

            try
            {
                var data = PgBase.SerialPartialSearch(input);
                textBox1.Text = String.Join(" ", data);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            //ClearMe();
            //textBox1.Text = Papa.info;
            //listBox1.SelectedItems.Clear();
        }

        private void ButtonOtborFromFile_Click(object sender, RoutedEventArgs e)
        {
            #region
            ClearMe();
            //OtborFromFile.MainOtborFromFile();
            OtborFromFileTerm.MainOtborFromFileTerm();
            textBox1.Text = Papa.info;
            #endregion
        }

        //actual_______________________________________



        //analis______________________________



        private void AnalisTerm_Click(object sender, RoutedEventArgs e)
        {
            #region
            ClearMe();
            try { AnalisTerm.MainAnalisTerm(); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            textBox1.Text = Papa.info + "\n";
            #endregion
        }


        //doc___________________________________________

        private void DocActivaciya_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = Activaciya.MainActivaciya;
            WorkMenu(myDelegatemenu);
            #endregion
        }



        private void DocDepartmentsToFile_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = DepToFile.MainDepToFile;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void DocTerminalsToFile_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = TermToFile.MainTermToFile;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void DocLogiToFile_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = LogiToFile.MainLogiToFile;
            WorkMenu(myDelegatemenu);
            #endregion
        }



        //refresh__________________________________________________

        private void RefreshListBox1_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Text = "";
            try
            {
                listBox1.ItemsSource = PgBase.GetListTerm();
            }
            catch (Exception ex) { textBox1.Text += ex.Message + "\n"; }
        }
        


        

        private void ButtonFindDepFromText(object sender, RoutedEventArgs e)
        {
            #region
            string choise = textBox1.Text.Trim();
            if (choise.Length < 8)
            {
                DepInTextBoxChoised = true;
                try { textBox1.Text = EdDep.MkTxt(choise); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else
            {
                DepInTextBoxChoised = false;
                try { textBox1.Text = EdTerm.MkTxt(choise); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            #endregion
        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            #region
            if (DepInTextBoxChoised)
            {
                try { textBox1.Text = EdDep.MkTxt(PgBase.NextDep(EdDep.GetDepFromTextBox(textBox1.Text))); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else
            {
                try { textBox1.Text = EdTerm.MkTxt(PgBase.NextTerm(EdTerm.GetTermFromTextBox(textBox1.Text))); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            #endregion
        }

        private void ButtonBackward_Click(object sender, RoutedEventArgs e)
        {
            #region
            if (DepInTextBoxChoised)
            {
                try { textBox1.Text = EdDep.MkTxt(PgBase.PredDep(EdDep.GetDepFromTextBox(textBox1.Text))); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else
            {
                try { textBox1.Text = EdTerm.MkTxt(PgBase.PredTerm(EdTerm.GetTermFromTextBox(textBox1.Text))); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            #endregion
        }

        private void ButtonAddDepFromTextBox_Click(object sender, RoutedEventArgs e)
        {
            #region
            if (DepInTextBoxChoised)
            {
                try
                {
                    var vec = EdDep.GetDepDataFromTextBox(textBox1.Text);
                    PgBase.DepAddOne(vec);
                    labeInfo.Content = $" + {vec[0]}";
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else
            {
                try
                {
                    var vec = EdTerm.GetTermDataFromTextBox(textBox1.Text);
                    PgBase.TermAddOne(vec);
                    labeInfo.Content = $" + {vec[0]}";
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            #endregion
        }

        private void ButtonDelDepFromTextBox_Click(object sender, RoutedEventArgs e)
        {
            #region
            if (DepInTextBoxChoised)
            {
                try
                {
                    string dep = EdDep.GetDepFromTextBox(textBox1.Text);
                    PgBase.DepDelOne(dep);
                    labeInfo.Content = $" - {dep}";
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else
            {
                try
                {
                    string dep = EdTerm.GetTermFromTextBox(textBox1.Text);
                    PgBase.TermDelOne(dep);
                    labeInfo.Content = $" - {dep}";
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            #endregion
        }

        private void RefreshAccess_Click(object sender, RoutedEventArgs e)
        {

            //TermBack.MainTermBack();
            //DepBack.MainDepBack();
            string txt = "";
            try
            {
                Process proc = new Process();

                proc.StartInfo.FileName = @"C:\SharpForPy\SharpForPy.exe";
                //proc.StartInfo.FileName = @"C:/ToAccessRelease/ToAccess1.exe"; 
                proc.Start();
                proc.WaitForExit();
                txt = "SharpForPy finish\n\n";
            }
            catch (Exception ex) { txt += $"\n{ex.Message}\n\n"; }
            textBox1.Text = txt;

            PgBase.OtborFromFile();
            txt += "otbor refreshed\n";

            PgBase.DepClear();
            PgBase.TermClear();
            PgBase.DepAddFromFileFull();
            //txt += "dep refreshed\n";
            txt += Papa.info + "\n";
            Papa.info = "";
            PgBase.TermAddFromFileFull();
            //txt += "term refreshed\n\n\tall refreshed";
            txt += Papa.info + "\n";
            textBox1.Text = txt;

        }

        private void RpAll_Click(object sender, RoutedEventArgs e)
        {
            #region
            Papa.selectedFolder = listBox1.SelectedItem.ToString();
            delegateMemu myDelegatemenu;
            myDelegatemenu = GetRpAll.MainGetRpAll;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void HrPartner_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //textBox1.Text = Papa.partnerChoised;
                Papa.partnerChoised = listBox1.SelectedItem.ToString();
                HrDepPartner.MainHrDepPartner();
                textBox1.Text = Papa.info;
            }
            catch (Exception ex) { textBox1.Text = ex.Message; }
        }
        */
    }
}
