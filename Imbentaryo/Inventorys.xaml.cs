using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignColors;
using MaterialDesignThemes;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using MahApps.Metro.Controls;

namespace Imbentaryo
{
    /// <summary>
    /// Interaction logic for Invetorys.xaml
    /// </summary>
    public partial class Inventorys
    {
        #region Declarations

        private Inventory inventory = new Inventory();
        private UtilityHelper utilityHelper = new UtilityHelper();
        private List<Inventory> inventoryList = null;

        #endregion Declarations

        #region Methods

        public Inventorys()
        {
            InitializeComponent();

            try
            {
                LoadItemsToDgd();
                InitializeMaterialDesign();
            }
            catch (System.Exception ex)
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
            inventoryList = utilityHelper.LoadItems();
            dgdInventory.ItemsSource = inventoryList;
        }

        private void PassValueToClass()
        {
            inventory.ItemCode = Convert.ToInt32(this.txtItemCode.Text);
            inventory.ItemType = this.txtItemType.Text;
            inventory.ItemName = this.txtItemName.Text;
            inventory.ItemStock = Convert.ToUInt32(this.txtItemStock.Text);
            inventory.ItemPrice = Convert.ToDecimal(this.txtItemPrice.Text);
           
        }

        private void ClearTextBoxes()
        {
            this.txtItemCode.Text = "";
            this.txtItemType.Text = "";
            this.txtItemName.Text = "";
            this.txtItemStock.Text = "";
            this.txtItemPrice.Text = "";
            
        }

        private bool CheckTextBoxIsEmpty()
        {
            if (this.txtItemCode.Text != "" &&
                this.txtItemType.Text !="" &&
                this.txtItemName.Text != "" &&
                this.txtItemStock.Text != "" &&
                this.txtItemPrice.Text != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

      
        #endregion Methods

        #region DataGrid

        private void dgdInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Inventory)dgdInventory.SelectedItem;

            if (selectedItem != null)
            {
                this.txtItemCode.Text = selectedItem.ItemCode.ToString();
                this.txtItemType.Text = selectedItem.ItemType;
                this.txtItemName.Text = selectedItem.ItemName;
                this.txtItemStock.Text = selectedItem.ItemStock.ToString();
                this.txtItemPrice.Text = selectedItem.ItemPrice.ToString();
                
            }
        }

        #endregion DataGrid

        #region Buttons

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            int itemCode;
            uint itemStock;
            decimal itemPrice;

            if (CheckTextBoxIsEmpty())
            {
                if (int.TryParse(this.txtItemCode.Text, out itemCode) &&
                    uint.TryParse(this.txtItemStock.Text, out itemStock) &&
                    decimal.TryParse(this.txtItemPrice.Text, out itemPrice))
                {
                    PassValueToClass();
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

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (CheckTextBoxIsEmpty())
            {
                PassValueToClass();
                utilityHelper.EditItem(inventory);
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
            inventory.ItemCode = Convert.ToInt32(this.txtItemCode.Text);
            utilityHelper.DeleteItem(inventory);

            MessageBox.Show("You've succesfully deleted the item!");

            ClearTextBoxes();
            LoadItemsToDgd();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes();
        }


        #endregion Buttons

    }
}