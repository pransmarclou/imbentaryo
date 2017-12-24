using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;

namespace Imbentaryo
{
    /// <summary>
    /// Interaction logic for Inventory.xaml
    /// </summary>
    public partial class Purchase
    {
        #region Decrations

        Inventory inventory = new Inventory();
        UtilityHelper utilityHelper = new UtilityHelper();
        private List<Inventory> inventoryList = null;

        #endregion

        #region Methods

        public Purchase()
        {
            InitializeComponent();
            try
            {
                LoadItemsToDgd();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
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
            inventoryList = utilityHelper.LoadPurchase();
            dgdPurchase.ItemsSource = inventoryList;
        }
        private void PassValueToClass()
        {
            inventory.InvoiceNumber = Convert.ToInt32(this.txtInvoiceNumber.Text);
            inventory.Date = this.txtDate.Text;
            inventory.ItemCode = Convert.ToInt32(this.txtItemCode.Text);
            inventory.ItemType = this.txtItemType.Text;
            inventory.ItemName = this.txtItemName.Text;
            inventory.ItemStock = Convert.ToUInt32(this.txtItemStock.Text);
            inventory.TotalAmount = Convert.ToDecimal(this.txtTotalAmount.Text);
            inventory.Manufacturer = this.txtManufacturer.Text;
        }
        private bool CheckTextBoxIsEmpty()
        {
            if (this.txtItemCode.Text != "" &&
                this.txtItemType.Text != "" &&
                this.txtItemName.Text != "" &&
                this.txtItemStock.Text != "" &&
                this.txtDate.Text != "" &&
                this.txtManufacturer.Text != "" &&
                this.txtTotalAmount.Text != "" &&
                this.txtInvoiceNumber.Text !="")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void ClearTextBoxes()
        {
            this.txtItemCode.Text = "";
            this.txtItemType.Text = "";
            this.txtItemName.Text = "";
            this.txtItemStock.Text = "";
            this.txtManufacturer.Text = "";
            this.txtDate.Text = "";
            this.txtInvoiceNumber.Text = "";
            this.txtItemType.Text = "";
            this.txtTotalAmount.Text = "";

        }
        #endregion Methods

        #region DataGrid
        private void dgdPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Inventory)dgdPurchase.SelectedItem;

            if (selectedItem != null)
            {
                this.txtInvoiceNumber.Text = selectedItem.InvoiceNumber.ToString();
                this.txtItemCode.Text = selectedItem.ItemCode.ToString();
                this.txtItemType.Text = selectedItem.ItemType;
                this.txtItemName.Text = selectedItem.ItemName;
                this.txtItemStock.Text = selectedItem.ItemStock.ToString();
                this.txtManufacturer.Text = selectedItem.Manufacturer;
                this.txtTotalAmount.Text = selectedItem.TotalAmount.ToString();
                this.txtDate.Text = selectedItem.Date;

            }
        }
        #endregion

        #region Buttons
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            int invoiceNumber;
            int itemCode;
            uint itemStock;
            decimal totalAmount;

            if (CheckTextBoxIsEmpty())
            {
                if (int.TryParse(this.txtItemCode.Text, out itemCode) &&
                    uint.TryParse(this.txtItemStock.Text, out itemStock) &&
                    decimal.TryParse(this.txtTotalAmount.Text, out totalAmount) &&
                    int.TryParse(this.txtInvoiceNumber.Text, out invoiceNumber))
                {                   
                    PassValueToClass();
                    inventory.ItemPrice = totalAmount / itemStock;
                    utilityHelper.AddPurchase(inventory);
                    utilityHelper.AddItem(inventory);
                    MessageBox.Show("You've Successfully Added the Item!");
                }
                else
                {
                    MessageBox.Show("Invalid Input!");
                }
                
            }
            else
            {
                MessageBox.Show("Please complete the required details!");
            }
            ClearTextBoxes();
            LoadItemsToDgd();

        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (CheckTextBoxIsEmpty())
            {
                PassValueToClass();
                utilityHelper.EditPurchase(inventory);
                utilityHelper.EditItem(inventory);

                MessageBox.Show("You've succesfully edited the item!");
            }
            else
            {
                MessageBox.Show("Please complete the required details!");
            }
            ClearTextBoxes();
            LoadItemsToDgd();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            inventory.InvoiceNumber = Convert.ToInt32(this.txtInvoiceNumber.Text);
            inventory.ItemCode = Convert.ToInt32(this.txtInvoiceNumber.Text);
            utilityHelper.DeletePurchase(inventory);
            utilityHelper.DeleteItem(inventory);

            MessageBox.Show("You've succesfully deleted the item!");

            ClearTextBoxes();
            LoadItemsToDgd();
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {

            if (PRSearchCmbbox.Text == "Show All")
            {
                LoadItemsToDgd();
            }

            if (PRSearchCmbbox.Text == "Total Amount")
            {

                inventory.TotalAmount = Convert.ToDecimal(txtSearch.Text);
                dgdPurchase.ItemsSource = utilityHelper.FindItem("TotalAmount", txtSearch.Text);
            }

            if (PRSearchCmbbox.Text == "Item Stock")
            {

                inventory.ItemStock = Convert.ToUInt32(txtSearch.Text);
                dgdPurchase.ItemsSource = utilityHelper.FindItem("ItemStock", txtSearch.Text);
            }

            if (PRSearchCmbbox.Text == "Date")
            {

                inventory.Date = txtSearch.Text;
                dgdPurchase.ItemsSource = utilityHelper.FindItem("Date", txtSearch.Text);
            }

        }
        #endregion
        
      
    }
}