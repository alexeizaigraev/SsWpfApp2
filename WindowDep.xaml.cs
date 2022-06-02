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
    /// Логика взаимодействия для WindowDep.xaml
    /// </summary>
    public partial class WindowDep : Window
    {

        public WindowDep()
        {
            InitializeComponent();
            #region
            var items = new List<string>() { "item1", "item2" };
            try
            {
                listBox1.ItemsSource = DbDepMeth.GetListDep();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                listBox1.ItemsSource = items;
            }
            cmbCityType.ItemsSource = DbDepMeth.GetListCityType();
            cmbStreetType.ItemsSource = DbDepMeth.GetLisStreetType();
            cmbPartner.ItemsSource = DbDepMeth.GetLisPartner();
            #endregion

        }

        private void ComboBoxCityType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            #region
            textBoxCityType.Text = cmbCityType.SelectedItem.ToString();
            #endregion
        }

        private void ComboBoxStreetType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            #region
            textBoxStreetType.Text = cmbStreetType.SelectedItem.ToString();
            #endregion
        }

        private void ComboBoxPartner_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            #region
            textBoxPartner.Text = cmbPartner.SelectedItem.ToString();
            #endregion
        }



        private List<string> GetCurrentData()
        {
            #region
            List<string> vec = new List<string>()
                {
                    textBoxDepartment.Text,
                    textBoxRegion.Text,
                    textBoxDistrictRegion.Text,
                    textBoxDistrCity.Text,
                    textBoxCityType.Text,
                    textBoxCity.Text,
                    textBoxStreet.Text,
                    textBoxStreetType.Text,
                    textBoxHous.Text,
                    textBoxPostIndex.Text,
                    textBoxPartner.Text,
                    textBoxStatus.Text,
                    textBoxRegister.Text,
                    textBoxEdrpou.Text,
                    textBoxAddress.Text,
                    textBoxPartnerName.Text,
                    textBoxIdTerminal.Text,
                    textBoxKoatu.Text,
                    textBoxTaxid.Text,
                    textBoxKoatu2.Text
                };
            vec = Papa.GoodVec(vec);
            return vec;
            #endregion
        }

        private void ClearMe()
        {
            #region
            textBoxDepartment.Text = "";
            textBoxRegion.Text = "";
            textBoxDistrictRegion.Text = "";
            textBoxDistrCity.Text = "";
            textBoxCityType.Text = "";
            textBoxCity.Text = "";
            textBoxStreet.Text = "";
            textBoxStreetType.Text = "";
            textBoxHous.Text = "";
            textBoxPostIndex.Text = "";
            textBoxPartner.Text = "";
            textBoxStatus.Text = "";
            textBoxRegister.Text = "";
            textBoxEdrpou.Text = "";
            textBoxAddress.Text = "";
            textBoxPartnerName.Text = "";
            textBoxIdTerminal.Text = "";
            textBoxKoatu.Text = "";
            textBoxTaxid.Text = "";
            textBoxKoatu2.Text = "";

            textBoxFind.Text = "";
            labeInfo.Content = "";

            cmbCityType.SelectedIndex = 0;
            cmbStreetType.SelectedIndex = 0;
            cmbPartner.SelectedIndex = 0;
            #endregion
        }

        private void WinDepMkAddress_Click(object sender, RoutedEventArgs e)
        {
            #region
            string rez = "";
            string post_index = textBoxPostIndex.Text;
            if (post_index != "")
                rez += post_index;
            string region = textBoxRegion.Text;
            if (region != "")
                rez += $" {region} обл.";
            string district_region = textBoxDistrictRegion.Text;
            if (district_region != "")
                rez += $" {district_region} p-н.";
            string city_type = textBoxCityType.Text;
            if (city_type != "")
                rez += $" {city_type}";
            string city = textBoxCity.Text;
            if (city != "")
                rez += $" {city}";
            string district_city = textBoxDistrCity.Text;
            if (district_city != "")
                rez += $" {district_city} p-н.";
            string street_type = textBoxStreetType.Text;
            if (street_type != "")
                rez += $" {street_type}";
            string street = textBoxStreet.Text;
            if (street != "")
                rez += $" {street}";
            string hous = textBoxHous.Text;
            if (hous != "")
                rez += $" {hous}";
            textBoxAddress.Text = rez;

            #endregion
        }

        private void WinDepMkKoatu2_Click(object sender, RoutedEventArgs e)
        {
            #region
            textBoxKoatu2.Text = Koatu2.MkKoatuNew(textBoxCity.Text, textBoxDistrCity.Text, textBoxKoatu.Text);
            #endregion
        }

        private void WinDepShowList_Click(object sender, RoutedEventArgs e)
        {
            #region
            string dep = "";

            try { dep = listBox1.SelectedItem.ToString(); }
            catch { MessageBox.Show("Выбери отделение из списка"); }

            if (dep != "")
            {
                try { Show(dep); }
                catch { MessageBox.Show("выбери одно отделение, балда"); }
            }
            //else { MessageBox.Show("Выбери отделение из списка"); }
            #endregion
        }

        private void WinDepFind_Click(object sender, RoutedEventArgs e)
        {
            #region
            string dep = "";

            try { dep = textBoxFind.Text; }
            catch { MessageBox.Show("Напиши отделение в окошко"); }

            if (dep != "")
            {
                try { Show(dep); }
                catch { MessageBox.Show("Напиши правильно отделение"); }
            }
            //else { MessageBox.Show("Напиши отделение в окошко"); }
            #endregion
        }

        private void Show(string dep)
        {
            #region
            var data = DbDepMeth.GetOneDep(dep);
            ClearMe();
            textBoxDepartment.Text = data[0];
            textBoxRegion.Text = data[1];
            textBoxDistrictRegion.Text = data[2];
            textBoxDistrCity.Text = data[3];
            textBoxCityType.Text = data[4];
            textBoxCity.Text = data[5];
            textBoxStreet.Text = data[6];
            textBoxStreetType.Text = data[7];
            textBoxHous.Text = data[8];
            textBoxPostIndex.Text = data[9];
            textBoxPartner.Text = data[10];
            textBoxStatus.Text = data[11];
            textBoxRegister.Text = data[12];
            textBoxEdrpou.Text = data[13];
            textBoxAddress.Text = data[14];
            textBoxPartnerName.Text = data[15];
            textBoxIdTerminal.Text = data[16];
            textBoxKoatu.Text = data[17];
            textBoxTaxid.Text = data[18];
            textBoxKoatu2.Text = data[19];
            #endregion
        }

        private void WinDepClear_Click(object sender, RoutedEventArgs e)
        {
            ClearMe();
        }




        private void WinDepAdd_Click(object sender, RoutedEventArgs e)
        {
            #region
            List<string> vec = GetCurrentData();
            try
            {
                DbDepMeth.AddOneDepartment(vec);
                labeInfo.Content = vec[0];
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            var items = new List<string>() { "item1", "item2" };
            try
            {
                listBox1.ItemsSource = DbDepMeth.GetListDep();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                listBox1.ItemsSource = items;
            }

            #endregion
        }

        private void WinDepDel_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            string dep = "";
            if (textBoxDepartment.Text != "" && textBoxFind.Text == "")
            {
                dep = textBoxDepartment.Text;
                flag = true;
            }
            if (textBoxDepartment.Text == "" && textBoxFind.Text != "")
            {
                dep = textBoxFind.Text;
                flag = true;
            }
            if (flag)
            {
                try { DbDepMeth.DeleteOneDepartment(dep); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

                var items = new List<string>() { "item1", "item2" };
                try
                {
                    listBox1.ItemsSource = DbDepMeth.GetListDep();
                    labeInfo.Content = Papa.info;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    listBox1.ItemsSource = items;
                }
            }
            else
            {
                MessageBox.Show("Выбери отделение, олух");
            }

        }

        private void WinDepUpdate_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            if (textBoxDepartment.Text != "") { flag = true; }
            if (flag)
            {
                List<string> vec = GetCurrentData();
                try { DbDepMeth.AddOneDepartment(vec); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                labeInfo.Content = Papa.info;
            }
        }

        private void WinDepForward_Click(object sender, RoutedEventArgs e)
        {
            #region
            string dep = textBoxDepartment.Text;
            string nextDep = DbDepMeth.NextDep(dep);
            Show(nextDep);
            #endregion
        }

        private void WinDepBack_Click(object sender, RoutedEventArgs e)
        {
            #region
            try
            {
                string dep = textBoxDepartment.Text;
                string nextDep = DbDepMeth.PredDep(dep);
                Show(nextDep);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            #endregion
        }


    }
}
