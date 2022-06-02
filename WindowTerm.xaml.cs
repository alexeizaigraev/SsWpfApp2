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
using System.Windows.Shapes;

namespace SsWpfApp2
{
    /// <summary>
    /// Логика взаимодействия для WindowTerm.xaml
    /// </summary>
    public partial class WindowTerm : Window
    {
        public WindowTerm()
        {
            InitializeComponent();

            #region
            var items = new List<string>() { "item1", "item2" };
            try
            {
                listBoxTerm.ItemsSource = DbTermMeth.GetLisTerm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                listBoxTerm.ItemsSource = items;
            }
            textBoxProducer.Text = "ДАТЕКС ООД";
            textBoxToRro.Text = "ТОВ ПОС";

            cmbModel.ItemsSource = DbTermMeth.GetLisModel();
            cmbOwner.ItemsSource = DbTermMeth.GetLisOwner();
            cmbSoft.ItemsSource = DbTermMeth.GetLisSoft();
            cmbSeal.ItemsSource = DbTermMeth.GetLisSeal();
            #endregion
        }

        private void ComboBoxModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            #region
            textBoxModel.Text = cmbModel.SelectedItem.ToString();
            #endregion
        }

        private void ComboBoxOwner_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            #region
            textBoxOwnerRro.Text = cmbOwner.SelectedItem.ToString();
            #endregion
        }

        private void ComboBoxSoft_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            #region
            textBoxSoft.Text = cmbSoft.SelectedItem.ToString();
            #endregion
        }

        private void ComboBoxSeal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            #region
            textBoxSealing.Text = cmbSeal.SelectedItem.ToString();
            #endregion
        }

        private void ClearMe()
        {
            #region
            textBoxDepartment.Text = "";
            textBoxTermial.Text = "";
            textBoxModel.Text = "";
            textBoxSerialNumber.Text = "";
            textBoxDateManufacture.Text = "";
            textBoxSoft.Text = "";
            textBoxProducer.Text = "ДАТЕКС ООД";
            textBoxRneRro.Text = "";
            textBoxSealing.Text = "";
            textBoxFiscalNumber.Text = "";
            textBoxOroSerial.Text = "";
            textBoxOroNumber.Text = "1";
            textBoxTicketSerial.Text = "";
            textBoxTicket1Sheet.Text = "";
            textBoxTicketNumber.Text = "1";
            textBoxSending.Text = "";
            textBoxToRro.Text = "ТОВ ПОС";
            textBoxOwnerRro.Text = "";
            textBoxRegister.Text = "";
            textBoxFinish.Text = "";

            textBoxFind.Text = "";

            textBoxAddress.Text = "";
            labeKoatu.Content = "";
            labeKoatu2.Content = "";
            labeTaxId.Content = "";

            cmbModel.SelectedIndex = 0;
            cmbOwner.SelectedIndex = 0;
            cmbSoft.SelectedIndex = 0;
            cmbSeal.SelectedIndex = 0;
            #endregion
        }

        private void WinTermClear_Click(object sender, RoutedEventArgs e)
        {
            ClearMe();
        }

        private List<string> GetCurrentData()
        {
            #region
            List<string> vec = new List<string>()
                {
                    textBoxDepartment.Text,
                    textBoxTermial.Text,
                    textBoxModel.Text,
                    textBoxSerialNumber.Text,
                    textBoxDateManufacture.Text,
                    textBoxSoft.Text,
                    textBoxProducer.Text,
                    textBoxRneRro.Text,
                    textBoxSealing.Text,
                    textBoxFiscalNumber.Text,
                    textBoxOroSerial.Text,
                    textBoxOroNumber.Text,
                    textBoxTicketSerial.Text,
                    textBoxTicket1Sheet.Text,
                    textBoxTicketNumber.Text,
                    textBoxSending.Text,
                    "", //books_arhiv
                    "", //tickets_arhiv
                    textBoxToRro.Text,
                    textBoxOwnerRro.Text,
                    textBoxRegister.Text,
                    textBoxFinish.Text
                };
            if (vec[6] == "") vec[6] = "ДАТЕКС ООД";
            if (vec[18] == "") vec[18] = "ТОВ ПОС";
            vec = Papa.GoodVec(vec);
            return vec;
            #endregion
        }

        private void WinTermShowList_Click(object sender, RoutedEventArgs e)
        {
            #region
            string term = "";

            try { term = listBoxTerm.SelectedItem.ToString(); }
            catch { MessageBox.Show($"Выбери терминал из списка, term={term}"); }

            if (term != "")
            {
                try { Show(term); }
                //catch {  }
                catch { MessageBox.Show("выбери один терминал, балда"); }
            }
            //else { MessageBox.Show("Выбери отделение из списка"); }
            #endregion
        }

        private void WinTermFind_Click(object sender, RoutedEventArgs e)
        {
            #region
            string term = "";

            try { term = textBoxFind.Text; }
            catch { MessageBox.Show("Напиши терминал в окошко"); }

            if (term != "")
            {
                try { Show(term); }
                catch { }
                //catch { MessageBox.Show("Напиши правильно терминал"); }
            }
            //else { MessageBox.Show("Напиши отделение в окошко"); }
            #endregion
        }

        private void Show(string term)
        {
            #region
            var data = DbTermMeth.GetOneTerm(term);
            if (data[6] == "") data[6] = "ДАТЕКС ООД";
            if (data[18] == "") data[18] = "ТОВ ПОС";
            ClearMe();

            textBoxDepartment.Text = data[0];
            textBoxTermial.Text = data[1];
            textBoxModel.Text = data[2];
            textBoxSerialNumber.Text = data[3];
            textBoxDateManufacture.Text = data[4];
            textBoxSoft.Text = data[5];
            textBoxProducer.Text = data[6];
            textBoxRneRro.Text = data[7];
            textBoxSealing.Text = data[8];
            textBoxFiscalNumber.Text = data[9];
            textBoxOroSerial.Text = data[10];
            textBoxOroNumber.Text = data[11];
            textBoxTicketSerial.Text = data[12];
            textBoxTicket1Sheet.Text = data[13];
            textBoxTicketNumber.Text = data[14];
            textBoxSending.Text = data[15];
            textBoxToRro.Text = data[18];
            textBoxOwnerRro.Text = data[19];
            textBoxRegister.Text = data[20];
            textBoxFinish.Text = data[21];

            textBoxAddress.Text = DbDepMeth.DepGetAddress(data[0]);

            try { labeKoatu.Content = DbDepMeth.DepGetKoatu(data[0]); }
            catch { labeKoatu.Content = ""; }
            try { labeKoatu2.Content = DbDepMeth.GetKoatu2New(data[0]); }
            catch { labeKoatu.Content = ""; }
            try { labeTaxId.Content = DbDepMeth.DepGetTaxId(data[0]); }
            catch { labeKoatu.Content = ""; }
            //catch (Exception ex) { MessageBox.Show(ex.Message); }
            labeKoatu2.Content = DbDepMeth.GetKoatu2New(data[0]);
            #endregion
        }

        private void WinTermAdd_Click(object sender, RoutedEventArgs e)
        {
            #region
            List<string> vec = GetCurrentData();
            try
            {
                //PgBase.TermAddOne(vec);
                DbTermMeth.AddOneTerminal(vec);
                labeInfo.Content = vec[1];
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            var items = new List<string>() { "item1", "item2" };
            try
            {
                listBoxTerm.ItemsSource = DbTermMeth.GetLisTerm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                listBoxTerm.ItemsSource = items;
            }
            #endregion
        }

        private void WinTermDel_Click(object sender, RoutedEventArgs e)
        {
            #region
            bool flag = false;
            string term = "";
            if (textBoxTermial.Text != "" && textBoxFind.Text == "")
            {
                term = textBoxTermial.Text;
                flag = true;
            }
            if (textBoxTermial.Text == "" && textBoxFind.Text != "")
            {
                term = textBoxFind.Text;
                flag = true;
            }
            if (flag)
            {
                try { DbTermMeth.DeleteOneTerminal(term); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

                var items = new List<string>() { "item1", "item2" };
                try
                {
                    listBoxTerm.ItemsSource = DbTermMeth.GetLisTerm();
                    labeInfo.Content = Papa.info;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    listBoxTerm.ItemsSource = items;
                }
            }
            else
            {
                MessageBox.Show("Выбери терминал, олух");
            }
            #endregion
        }

        private void WinTermUpdate_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            if (textBoxTermial.Text != "") { flag = true; }
            if (flag)
            {
                List<string> vec = GetCurrentData();
                try { DbTermMeth.AddOneTerminal(vec); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                labeInfo.Content = Papa.info;
            }
        }

        private void WinTermForward_Click(object sender, RoutedEventArgs e)
        {
            #region
            string term = textBoxTermial.Text;
            string next = DbTermMeth.NextTerm(term);
            Show(next);
            #endregion
        }

        private void WinTermBack_Click(object sender, RoutedEventArgs e)
        {
            #region
            try
            {
                string term = textBoxTermial.Text;
                string pred = DbTermMeth.PredTerm(term);
                Show(pred);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            #endregion
        }


        private void ButtonOtborInputZn_Click(object sender, RoutedEventArgs e)
        {
            var input = textBoxFind.Text;
            try
            {
                string term = DbTermMeth.SerialToTermOne(input);
                Show(term);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void ButtonOtborInputFiscal_Click(object sender, RoutedEventArgs e)
        {
            var input = textBoxFind.Text;
            try
            {
                string term = DbTermMeth.FiscalToTermOne(input);
                Show(term);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }


        private void ButtonFindAny_Click(object sender, RoutedEventArgs e)
        {
            List<string> terms = DbTermMeth.GetLisTerm();
            List<string> serials = DbTermMeth.GetListSerial();
            List<string> fiscals = DbTermMeth.GetListFiscal();

            var input = textBoxFind.Text;

            if (terms.IndexOf(input) > -1)
            {
                try { Show(input); }
                catch { }
            }
            else if (serials.IndexOf(input) > -1)
            {
                try
                {
                    string term = DbTermMeth.SerialToTermOne(input);
                    Show(term);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else if (fiscals.IndexOf(input) > -1)
            {
                try
                {
                    string term = DbTermMeth.FiscalToTermOne(input);
                    Show(term);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }


    }
}
