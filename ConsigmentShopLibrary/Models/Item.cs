namespace ConsigmentShopLibrary
{
    public class Item
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public bool Sold { get; private set; }
        public bool PaymentDistributed { get; private set; }
        public Vendor Owner { get; private set; }

        public string Display
        {
            get
            {
                return string.Format("{0} - €{1}", Title, Price);
            }
        }

        public Item(string title, string description, decimal price, bool sold, bool paymentDistributed, Vendor owner)
        {
            Title = title;
            Description = description;
            Price = price;
            Sold = sold;
            PaymentDistributed = paymentDistributed;
            Owner = owner;
        }

        public bool SetSoldState(bool foo)
        {
            return Sold = foo;
        }
    }
}
