namespace Imbentaryo
{
    internal class Inventory
    {
        #region Declarations

        private int itemCode;
        private string itemType;
        private string itemName;
        private uint itemStock;
        private decimal itemPrice;
        public string manufacturer;
        private int invoiceNumber;
        private int receiptNumber;
        private string date;
        private decimal totalAmount;
        private int sellerQuantity;

        #endregion Declarations

        #region Properties

        public int ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }

        public string ItemType
        {
            get { return itemType; }
            set { itemType = value; }
        }

        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }

        public uint ItemStock
        {
            get { return itemStock; }
            set { itemStock = value; }
        }

        public decimal ItemPrice
        {
            get { return itemPrice; }
            set { itemPrice = value; }
        }

        public string Manufacturer
        {
            get { return manufacturer; }
            set { manufacturer = value; }
        }

        public int InvoiceNumber
        {
            get { return invoiceNumber; }
            set { invoiceNumber = value; }
        }

        public int ReceiptNumber
        {
            get { return receiptNumber; }
            set { receiptNumber = value; }
        }

        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        public decimal TotalAmount
        {
            get { return totalAmount; }
            set { totalAmount = value; }
        }

        public int SellerQuantity
        {
            get { return sellerQuantity; }
            set { sellerQuantity = value; }
        }

        //public string ItemName { get; set; }
        //public uint ItemStock { get; set; }
        //public decimal ItemPrice { get; set; }
        //public string Manufacturer { get; set; }
        //public int InvoiceNumber { get; set; }
        //public int ReceiptNumber { get; set; }
        //public string Date { get; set; }
        //public decimal TotalAmount { get; set; }
        //public int SellerQuantity { get; set; }

        #endregion Properties

        #region Constructors

        public Inventory()
        { }

        /// <summary>
        /// Inventorys Constructor
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="itemType"></param>
        /// <param name="itemName"></param>
        /// <param name="itemStock"></param>
        /// <param name="itemPrice"></param>
        public Inventory(int itemCode, string itemType, string itemName,
                         uint itemStock, decimal itemPrice)
        {
            this.itemCode = itemCode;
            this.itemType = itemType;
            this.itemName = itemName;
            ItemCode = itemCode;
            ItemType = itemType;
            ItemName = itemName;
            ItemStock = itemStock;
            ItemPrice = itemPrice;
        }

        /// <summary>
        /// Purchase Constructor
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <param name="itemCode"></param>
        /// <param name="itemType"></param>
        /// <param name="itemName"></param>
        /// <param name="itemStock"></param>
        /// <param name="manufacturer"></param>
        /// <param name="totalAmount"></param>
        /// <param name="date"></param>
        public Inventory(int invoiceNumber, int itemCode, string itemType,
                         string itemName, uint itemStock, string manufacturer,
                         decimal totalAmount, string date)
        {
            InvoiceNumber = invoiceNumber;
            ItemCode = itemCode;
            ItemType = itemType;
            ItemName = itemName;
            ItemStock = itemStock;
            Manufacturer = manufacturer;
            TotalAmount = totalAmount;
            Date = date;
        }

        /// <summary>
        /// Seller Constructor
        /// </summary>
        /// <param name="recieptNumber"></param>
        /// <param name="date"></param>
        /// <param name="itemCode"></param>
        /// <param name="itemName"></param>
        /// <param name="itemType"></param>
        /// <param name="sellerQTY"></param>
        /// <param name="itemPrice"></param>
        /// <param name="totalAmount"></param>
        public Inventory(int recieptNumber, string date, int itemCode, string itemName, string itemType, int sellerQTY, decimal itemPrice, decimal totalAmount)
        {
            ReceiptNumber = recieptNumber;
            Date = date;
            ItemCode = itemCode;
            ItemName = itemName;
            ItemType = itemType;
            SellerQuantity = sellerQTY;
            ItemPrice = itemPrice;
            TotalAmount = totalAmount;
        }

        #endregion Constructors
    }
}