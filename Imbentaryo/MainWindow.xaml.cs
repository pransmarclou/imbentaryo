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

namespace Imbentaryo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {       
        #region Methods
        public MainWindow()
        {
            InitializeComponent();
            InitializeAllUserControl();
        }

        public void InitializeAllUserControl()
        {
            this.lstDockLeft.SelectedIndex = 0;
            cntControl.Content = new Home();

            this.btnHome.IsEnabled = true;
            this.btnInventory.IsEnabled = true;
            this.btnPurchase.IsEnabled = true;
            this.btnSell.IsEnabled = true;
            this.btnSellReport.IsEnabled = true;

            this.txbTitle.Text = "Home";

        }
        #endregion

        #region Buttons
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            InitializeAllUserControl();
            this.lstDockLeft.SelectedIndex = 0;
            cntControl.Content = new Home();
            this.btnHome.IsEnabled = false;
            this.txbTitle.Text = "Home";
        }

        private void btnInventory_Click(object sender, RoutedEventArgs e)
        {
            InitializeAllUserControl();
            cntControl.Content = new Inventorys();
            this.lstDockLeft.SelectedIndex = 1;       
            this.btnInventory.IsEnabled = false;
            this.txbTitle.Text = "Inventory";
        }

        private void btnPurchase_Click(object sender, RoutedEventArgs e)
        {
            InitializeAllUserControl();
            cntControl.Content = new Purchase();
            this.lstDockLeft.SelectedIndex = 2;            
            this.btnPurchase.IsEnabled = false;
            this.txbTitle.Text = "Purchase";
        }

        private void btnSell_Click(object sender, RoutedEventArgs e)
        {
            InitializeAllUserControl();
            cntControl.Content = new Sell();
            this.lstDockLeft.SelectedIndex = 3;           
            this.btnSell.IsEnabled = false;
            this.txbTitle.Text = "Sell";
        }
        private void btnSellReport_Click(object sender, RoutedEventArgs e)
        {
            InitializeAllUserControl();
            cntControl.Content = new SellReport();
            this.lstDockLeft.SelectedIndex = 4;         
            this.btnSellReport.IsEnabled = false;
            this.txbTitle.Text = "SellReport";
        }
       
        #endregion
               
    }
}
