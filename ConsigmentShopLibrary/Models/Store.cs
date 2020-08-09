using System.Collections.Generic;

namespace ConsigmentShopLibrary
{
    public class Store
    {
        public string StoreName { get; private set; }
        public List<Vendor> Vendors { get; private set; }
        public List<Item> Items { get; private set; }

        public Store(string storeName)
        {
            StoreName = storeName;
            Vendors = new List<Vendor>();
            Items = new List<Item>();
        }
    }
}
