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
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;

namespace Imbentaryo
{
    /// <summary>
    /// Interaction logic for SellReport.xaml
    /// </summary>
    public partial class SellReport 
    {
        #region Declarations

        Inventory inventory = new Inventory();
        UtilityHelper utilityHelper = new UtilityHelper();
        private List<Inventory> inventoryList = null;

        #endregion
        #region Methods

        public SellReport()
        {
            InitializeComponent();
            LoadItemsToDgd();
        }

        private void InitializeMaterialDesign()
        {
            // Create dummy objects to force the MaterialDesign assemblies to be loaded
            // from this assembly, which causes the MaterialDesign assemblies to be searched
            // relative to this assembly's path. Otherwise, the MaterialDesign assemblies
            // are searched relative to Eclipse's path, so they're not found.
            var card = new Card();
            var hue = new Hue("Dummy", Colors.Black, Colors.White);
        }

        private void LoadItemsToDgd()
        {
            inventoryList = utilityHelper.LoadSellerReport();
            dgdSellReport.ItemsSource = inventoryList;
        }

        #endregion
        
        #region Buttons
        private void btnGo_Click(object sender, RoutedEventArgs e)
        {

            if (cmbSearch.Text == "Show All")
            {
                LoadItemsToDgd();
            }

            if (cmbSearch.Text == "Total Amount")
            {

                inventory.TotalAmount = Convert.ToDecimal(txtSearch.Text);
                dgdSellReport.ItemsSource = utilityHelper.FindItem("TotalAmount", txtSearch.Text);
            }

            if (cmbSearch.Text == "Item Stock")
            {

                inventory.ItemStock = Convert.ToUInt32(txtSearch.Text);
                dgdSellReport.ItemsSource = utilityHelper.FindItem("ItemStock", txtSearch.Text);
            }

            if (cmbSearch.Text == "Date")
            {

                inventory.Date = txtSearch.Text;
                dgdSellReport.ItemsSource = utilityHelper.FindItem("Date", txtSearch.Text);
            }

        }
        #endregion


    }
}
