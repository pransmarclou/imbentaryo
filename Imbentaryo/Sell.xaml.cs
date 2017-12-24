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
    /// Interaction logic for Sell.xaml
    /// </summary>
    public partial class Sell
    {

        #region Declarations
        Inventory itm = new Inventory();
        UtilityHelper help = new UtilityHelper();
        decimal totalamtHandler = 0;
        decimal paymentHandler = 0;
        #endregion

        #region Methods
        public Sell()
        {
            InitializeComponent();
            LoadDate();
            LoadRecieptNumberSeller();
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
        #endregion

        private void LoadDate()
        {
           this.lblDate.Content = DateTime.Now.ToLongDateString();
           this.lblTotalAmount.Content = "0.00";
        }

        //LoadRecieptNumberSeller() returns the value of the itm.RecieptNumber that does not exist in the xml file. place it on the display.
        //          it uses  the itm.Manufacturer as a parameter to determine if the xml contain the recieptnumber.
        private void LoadRecieptNumberSeller()
        {
            int recieptHolder = 0;

                       

            itm.Manufacturer = "";
            do
            {
                itm.ReceiptNumber= recieptHolder;
                // itm.RecieptNumber must contain a value. Use a do while to determine if the Reciept Number is existing. 
                // starting from the value of 0 onwards.
                help.GetRecieptNumberSeller(itm);
                // This method search for the reciept number located in the XML file.

                // Cases:
                // if it exist, return itm.Manufacturer = "RecieptNumberExist";
                if (itm.Manufacturer == "RecieptNumberExist")
                {
                    recieptHolder++;
                }
                // if it does not exist, return itm.Manufacturer = "RecieptNumberDoesNotExist";
            } while (itm.Manufacturer == "RecieptNumberExist");

            itm.ReceiptNumber = recieptHolder;
            lblReceiptNumber.Content = Convert.ToString(itm.ReceiptNumber);
        }
        

        private void SAddItemBtn_Click(object sender, RoutedEventArgs e)
        {
            bool error = false;
           

            itm.ReceiptNumber = Convert.ToInt32(lblReceiptNumber.Content);

            try
            {
                itm.ItemCode = Convert.ToInt32(txtItemCode.Text);
            }
            catch
            {
                MessageBox.Show("Please Enter a Number in the box provided.");
                txtItemCode.Text = " ";
                error = true;
            }

            try
            {
                itm.SellerQuantity = Convert.ToInt32(this.txtQuantity.Text);
            }
            catch
            {
                MessageBox.Show("Please Enter a Number in the box provided.");
                txtQuantity.Text = " ";
                error = true;
            }

            itm.Date = DateTime.Now.ToLongDateString();


            if (error == false)
            {
                help.FindItemSell(itm);
             
                //Note: This Method Subtracts the SellerQTY to Quantity wherein the bought items are 
                //       subtracted in the inventory.
                itm.ItemStock = itm.ItemStock - Convert.ToUInt32(itm.SellerQuantity);

                help.EditItem(itm);

                itm.TotalAmount = (itm.SellerQuantity * itm.ItemPrice);
                help.AddSeller(itm);
               

                txtItemCode.Text = "";
                txtQuantity.Text = "";

               this.lstSeller.Items.Add("    " + itm.SellerQuantity + "\t" + itm.ItemName + "\t\t" + itm.ItemPrice + "\t" + itm.TotalAmount);
                totalamtHandler = totalamtHandler + itm.TotalAmount;
                this.lblTotalAmount.Content = "P  " + totalamtHandler;
            }
            error = false;

        }

        private void SCheckOutBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                paymentHandler = Convert.ToDecimal(txtPayment.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
           

            if (paymentHandler > totalamtHandler || paymentHandler == totalamtHandler)
            {
               lstSeller.Items.Add("\n\tSubTotal:\t" + "P " + totalamtHandler);
               lstSeller.Items.Add("\tPayment:\t" + "P " + paymentHandler);
               lstSeller.Items.Add("\tChange:\t" + "P " + (paymentHandler - totalamtHandler));

                MessageBox.Show("Transaction Successful!");
               lstSeller.Items.Clear();

                this.txtPayment.Text = "";
               lblTotalAmount.Content = "0.00";
                LoadRecieptNumberSeller();
                
            }

            if (paymentHandler < totalamtHandler)
            {
                MessageBox.Show("Insufficient Balance!");
                txtPayment.Text = " ";

            }

        }




    }
}