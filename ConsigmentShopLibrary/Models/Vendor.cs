namespace ConsigmentShopLibrary
{
    public class Vendor
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public double Commission { get; private set; }
        public decimal PaymentDue { get; private set; }

        public string Display
        {
            get
            {
                return string.Format("{0} {1} - €{2}", FirstName, LastName, PaymentDue);
            }
        }

        public Vendor(string firstName, string lastName, double commission = 0.5)
        {
            FirstName = firstName;
            LastName = lastName;
            Commission = commission;
        }

        public void ModifyPaymentDue(decimal price)
        {
            PaymentDue += (decimal)Commission * price;
        }
    }
}
