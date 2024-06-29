namespace StellarBillingSystem.Models
{
    public class ProductSelectModel
    {
        public ProductSelectModel() { }

        private string productID;
        private string barcodeID;
        private string quantity;

        public string ProductID { get => productID; set => productID = value; }
        public string BarcodeID { get => barcodeID; set => barcodeID = value; }
        public string Quantity { get => quantity; set => quantity = value; }
    }
}
