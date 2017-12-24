using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Imbentaryo
{
    internal class UtilityHelper
    {
        #region Declarations

        private string fileInventory = "";
        private string filePurchase = "";
        private string fileSeller = "";
        private Inventory contacts = new Inventory();
        private XDocument xDoc = null;

        #endregion Declarations

        #region Constructor

        public UtilityHelper()
        {
            this.fileInventory = ConfigurationManager.AppSettings["fileInventory"];
            this.filePurchase = ConfigurationManager.AppSettings["filePurchase"];
            this.fileSeller = ConfigurationManager.AppSettings["fileSeller"];
        }

        #endregion Constructor

        #region Inventory

        public void AddItem(Inventory inventory)
        {
            //Note: Checks if the file exists if not
            //      it will create a new element
            if (File.Exists(fileInventory))
            {
                xDoc = XDocument.Load(fileInventory);
            }
            else
            {
                xDoc = new XDocument(new XElement("Inventory"));
            }

            XElement xAddElem =
                   new XElement("Item", new XAttribute("ItemCode", inventory.ItemCode),
                                new XElement("ItemType", inventory.ItemType),
                                new XElement("ItemName", inventory.ItemName),
                                new XElement("ItemStock", inventory.ItemStock),
                                new XElement("ItemPrice", inventory.ItemPrice));

            xDoc.Root.Add(xAddElem);
            xDoc.Save(fileInventory);
        }

        public void EditItem(Inventory inventory)
        {
            //Note: Checks if the file exists if not
            //      nothing will happen
            if (File.Exists(fileInventory))
            {
                xDoc = XDocument.Load(fileInventory);

                var editItem = (from xle in xDoc.Descendants("Item")
                                where xle.Attribute("ItemCode").Value == inventory.ItemCode.ToString()
                                select xle).SingleOrDefault();

                editItem.Attribute("ItemCode").Value = inventory.ItemCode.ToString();
                editItem.Element("ItemType").Value = inventory.ItemType;
                editItem.Element("ItemName").Value = inventory.ItemName;
                editItem.Element("ItemStock").Value = inventory.ItemStock.ToString();
                editItem.Element("ItemPrice").Value = inventory.ItemPrice.ToString();
                
            }
            xDoc.Save(fileInventory);
        }

        public void DeleteItem(Inventory inventory)
        {
            //Note: Checks if the file exists if not
            //      nothing will happen
            if (File.Exists(fileInventory))
            {
                xDoc = XDocument.Load(fileInventory);

                var deleteContact = (from xle in xDoc.Descendants("Item")
                                     where xle.Attribute("ItemCode").Value == inventory.ItemCode.ToString()
                                     select xle).Take(1).SingleOrDefault();
                deleteContact.Remove();
                xDoc.Save(fileInventory);
            }
        }

        public List<Inventory> LoadItems()
        {
            List<Inventory> inventory = new List<Inventory>();

            //Note: Checks if the file exists if not
            //      nothing will happen
            if (File.Exists(fileInventory))
            {
                xDoc = XDocument.Load(fileInventory);

                var loadItems = (from xle in xDoc.Descendants("Item")
                                 select xle);

                foreach (var loadItem in loadItems)
                {
                    inventory.Add(new Inventory(Convert.ToInt32(loadItem.Attribute("ItemCode").Value),
                                                loadItem.Element("ItemType").Value,
                                                loadItem.Element("ItemName").Value,
                                                Convert.ToUInt32(loadItem.Element("ItemStock").Value),
                                                Convert.ToDecimal(loadItem.Element("ItemPrice").Value)));
                }
            }
            return inventory;
        }

        #endregion Inventory

        #region Purchase

        public void AddPurchase(Inventory inventory)
        {
            //Note: Checks if the file exists if not
            //      it will create a new element
            XDocument xDoc = null;

            if (File.Exists(filePurchase))
            {
                xDoc = XDocument.Load(filePurchase);
            }
            else
            {
                xDoc = new XDocument(new XElement("Purchase"));
            }

            XElement xAddElem =
                new XElement("Item", new XAttribute("InvoiceNumber", inventory.InvoiceNumber),
                    new XElement("ItemCode", inventory.ItemCode),
                    new XElement("ItemType", inventory.ItemType),
                    new XElement("ItemName", inventory.ItemName),                   
                    new XElement("ItemStock", inventory.ItemStock),
                    new XElement("Manufacturer", inventory.Manufacturer),
                    new XElement("Date", inventory.Date),
                    new XElement("TotalAmount", inventory.TotalAmount));

            xDoc.Root.Add(xAddElem);
            xDoc.Save(filePurchase);
        }

        public void EditPurchase(Inventory inventory)
        {
            //Note: Checks if the file exists if not
            //      nothing will happen
            if (File.Exists(filePurchase))
            {
                xDoc = XDocument.Load(filePurchase);

                var editItem = (from xle in xDoc.Descendants("Item")
                                where xle.Attribute("InvoiceNumber").Value == inventory.InvoiceNumber.ToString()
                                select xle).SingleOrDefault();

                editItem.Attribute("InvoiceNumber").Value = inventory.InvoiceNumber.ToString();
                editItem.Element("ItemType").Value = inventory.ItemType;
                editItem.Element("ItemCode").Value = inventory.ItemCode.ToString();
                editItem.Element("ItemName").Value = inventory.ItemName;
                editItem.Element("ItemStock").Value = inventory.ItemStock.ToString();
                editItem.Element("Date").Value = inventory.Date;
                editItem.Element("TotalAmount").Value = inventory.TotalAmount.ToString();
                editItem.Element("Manufacturer").Value = inventory.Manufacturer;
                
                
            }
            xDoc.Save(filePurchase);
        }

        public void DeletePurchase(Inventory inventory)
        {
            //Note: Checks if the file exists if not
            //      nothing will happen
            if (File.Exists(filePurchase))
            {
                xDoc = XDocument.Load(filePurchase);

                var deleteContact = (from xle in xDoc.Descendants("Item")
                                     where xle.Attribute("InvoiceNumber").Value == inventory.InvoiceNumber.ToString()
                                     select xle).Take(1).SingleOrDefault();
                deleteContact.Remove();
                xDoc.Save(filePurchase);
            }
        }

        public List<Inventory> LoadPurchase()
        {
            List<Inventory> inventory = new List<Inventory>();

            //Note: Checks if the file exists if not
            //      nothing will happen
            if (File.Exists(filePurchase))
            {
                xDoc = XDocument.Load(filePurchase);

                var loadItems = (from xle in xDoc.Descendants("Item")
                                 select xle);

                foreach (var loadPurchase in loadItems)
                {
                    inventory.Add(new Inventory(Convert.ToInt32(loadPurchase.Attribute("InvoiceNumber").Value),
                                              Convert.ToInt32(loadPurchase.Element("ItemCode").Value),
                                              loadPurchase.Element("ItemType").Value,
                                              loadPurchase.Element("ItemName").Value,
                                              Convert.ToUInt32(loadPurchase.Element("ItemStock").Value),
                                              loadPurchase.Element("Manufacturer").Value,
                                              Convert.ToDecimal(loadPurchase.Element("TotalAmount").Value),
                                              loadPurchase.Element("Date").Value));
                }
            }
            return inventory;
        }
        public List<Inventory> FindItem(string filterName, string filterValue)
        {
            List<Inventory> inventorys = new List<Inventory>();

            if (File.Exists(filePurchase))
            {
                xDoc = XDocument.Load(filePurchase);

                var results = (from xle in xDoc.Descendants("Item")
                               where xle.Element(filterName).Value == filterValue
                               select xle);

                foreach (var loadPurchase in results)
                {
                    inventorys.Add(new Inventory(Convert.ToInt32(loadPurchase.Attribute("InvoiceNumber").Value),
                                              Convert.ToInt32(loadPurchase.Element("ItemCode").Value),
                                              loadPurchase.Element("ItemType").Value,
                                              loadPurchase.Element("ItemName").Value,
                                              Convert.ToUInt32(loadPurchase.Element("ItemStock").Value),
                                              loadPurchase.Element("Manufacturer").Value,
                                              Convert.ToDecimal(loadPurchase.Element("TotalAmount").Value),
                                              loadPurchase.Element("Date").Value));
                }                
            }
            return inventorys;
        }
        #endregion Purchase

        #region Seller
        public void AddSeller(Inventory itm)
        {

            if (File.Exists(fileSeller))
            {
                xDoc = XDocument.Load(fileSeller);
            }
            else
            {
                xDoc = new XDocument(new XElement("Seller"));
            }

            XElement xElem =
                new XElement("Item", new XAttribute("ReceiptNumber", itm.ReceiptNumber),
                    new XElement("Date", itm.Date),
                    new XElement("ItemCode", itm.ItemCode),
                    new XElement("ItemName", itm.ItemName),
                    new XElement("ItemType", itm.ItemType),
                    new XElement("SellerQuantity", itm.SellerQuantity),
                    new XElement("ItemPrice", itm.ItemPrice),
                    new XElement("TotalAmount", itm.TotalAmount)
                    );

            xDoc.Root.Add(xElem);
            xDoc.Save(fileSeller);
        }

        public void DeleteSeller(Inventory itm)
        {
  

            if (File.Exists(fileSeller))
            {
                xDoc = XDocument.Load(fileSeller);

                var info2 = (from xle in xDoc.Descendants("Item")
                             where xle.Attribute("ReceiptNumber").Value == itm.ReceiptNumber.ToString()
                             select xle).Take(1).SingleOrDefault();

                info2.Remove();

                xDoc.Save(fileSeller);
            }
        }

        public void FindItemSell(Inventory itm)
        {


            if (File.Exists(fileInventory))
            {
                xDoc = XDocument.Load(fileInventory);

                var info1 = (from xle in xDoc.Descendants("Item")
                             where xle.Attribute("ItemCode").Value == itm.ItemCode.ToString()
                             select xle).Take(1).SingleOrDefault();

                itm.ItemName = info1.Element("ItemName").Value;
                itm.ItemType = info1.Element("ItemType").Value;
                itm.ItemPrice = Convert.ToDecimal(info1.Element("ItemPrice").Value);               
                itm.ItemStock = Convert.ToUInt32(info1.Element("ItemStock").Value);

                xDoc.Save(fileInventory);
            }
        }

        public List<Inventory> LoadSellerReport()
        {
         
            List<Inventory> itm = new List<Inventory>();
         
            xDoc = XDocument.Load(fileSeller);

            var results = (from xle in xDoc.Descendants("Item")
                           select xle);

            foreach (var item in results)
            {
                itm.Add(new Inventory(
                     Convert.ToInt32(item.Attribute("ReceiptNumber").Value),
                    item.Element("Date").Value,
                    Convert.ToInt32(item.Element("ItemCode").Value),
                    item.Element("ItemName").Value,
                    item.Element("ItemType").Value,
                    Convert.ToInt32(item.Element("SellerQuantity").Value),
                    Convert.ToDecimal(item.Element("ItemPrice").Value),
                    Convert.ToDecimal(item.Element("TotalAmount").Value)));
            }
            return itm;
        }

        public List<Inventory> SRSortByDate(Inventory invent)
        {
   
            List<Inventory> itm = new List<Inventory>();
         
            xDoc = XDocument.Load(fileSeller);

            var results = (from xle in xDoc.Descendants("Item")
                           where xle.Element("Date").Value == invent.Date
                           select xle);

            foreach (var item in results)
            {
                itm.Add(new Inventory(
                    invent.ReceiptNumber = Convert.ToInt32(item.Attribute("ReceiptNumber").Value),
                    invent.Date = item.Element("Date").Value,
                    invent.ItemCode = Convert.ToInt32(item.Element("ItemCode").Value),
                    invent.ItemName = item.Element("ItemName").Value,
                    invent.ItemType = item.Element("ItemType").Value,
                    invent.SellerQuantity = Convert.ToInt32(item.Element("SellerQuantity").Value),
                    invent.ItemPrice = Convert.ToDecimal(item.Element("ItemPrice").Value),
                    invent.TotalAmount = Convert.ToDecimal(item.Element("TotalAmount").Value)
                    ));
            }
            return itm;
        }

        public List<Inventory> SRSortByTotalAmount(Inventory invent)
        {
            
            List<Inventory> itm = new List<Inventory>();
          

            xDoc = XDocument.Load(fileSeller);

            var results = (from xle in xDoc.Descendants("Item")
                           where xle.Element("TotalAmount").Value == invent.TotalAmount.ToString()
                           select xle);

            foreach (var item in results)
            {
                itm.Add(new Inventory(
                    invent.ReceiptNumber = Convert.ToInt32(item.Attribute("ReceiptNumber").Value),
                    invent.Date = item.Element("Date").Value,
                    invent.ItemCode = Convert.ToInt32(item.Element("ItemCode").Value),
                    invent.ItemName = item.Element("ItemName").Value,
                    invent.ItemType = item.Element("ItemType").Value,
                    invent.SellerQuantity = Convert.ToInt32(item.Element("SellerQuantity").Value),
                    invent.ItemPrice = Convert.ToDecimal(item.Element("ItemPrice").Value),
                    invent.TotalAmount = Convert.ToDecimal(item.Element("TotalAmount").Value)
                    ));
            }
            return itm;
        }

        public List<Inventory> SRSortBySellerQTY(Inventory invent)
        {
           
            List<Inventory> itm = new List<Inventory>();
         
            xDoc = XDocument.Load(fileSeller);

            var results = (from xle in xDoc.Descendants("Item")
                           where xle.Element("SellerQuantity").Value == invent.SellerQuantity.ToString()
                           select xle);

            foreach (var item in results)
            {
                itm.Add(new Inventory(
                    invent.ReceiptNumber = Convert.ToInt32(item.Attribute("ReceiptNumber").Value),
                    invent.Date = item.Element("Date").Value,
                    invent.ItemCode = Convert.ToInt32(item.Element("ItemCode").Value),
                    invent.ItemName = item.Element("ItemName").Value,
                    invent.ItemType = item.Element("ItemType").Value,
                    invent.SellerQuantity = Convert.ToInt32(item.Element("SellerQuantity").Value),
                    invent.ItemPrice = Convert.ToDecimal(item.Element("ItemPrice").Value),
                    invent.TotalAmount = Convert.ToDecimal(item.Element("TotalAmount").Value)
                    ));
            }
            return itm;
        }

        #endregion

        #region Sell Report
        public void GetRecieptNumberSeller(Inventory itm)
        {

            XDocument xDoc = null;

            if (File.Exists(fileSeller))
            {
                xDoc = XDocument.Load(fileSeller);
                var getReceiptNo = (from xle in xDoc.Descendants("Inventory")
                                    where xle.Attribute("ReceiptNumber").Value == itm.ReceiptNumber.ToString()
                                    select xle).Take(1).SingleOrDefault();

                if (getReceiptNo != null)
                {
                    itm.Manufacturer = "RecieptNumberExist";
                }
                if (getReceiptNo == null)
                {
                    itm.Manufacturer = "RecieptNumberDoesNotExist";
                }
            }

        }

        #endregion
    }
}