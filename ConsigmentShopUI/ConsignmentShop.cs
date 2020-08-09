using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using ConsigmentShopLibrary;

namespace ConsigmentShopUI
{
    public partial class ConsignmentShop : Form
    {
        private Store store = new Store("Reading is awesome");
        private List<Item> shoppingCartItems = new List<Item>();

        private BindingSource storeItemsBindingSource = new BindingSource();
        private BindingSource storeCartItemsBindingSource = new BindingSource();
        private BindingSource vendorsBindingSource = new BindingSource();

        private decimal storeProfit = 0;

        public ConsignmentShop()
        {
            InitializeComponent();

            SetupData();

            InitStoreItemListBox();
            InitShopCartItemListBox();
            InitVendorBindingListBox();
        }

        private void InitStoreItemListBox()
        {
            storeItemsBindingSource.DataSource = store.Items.Where(x => x.Sold == false).ToList();
            ItemsListBox.DataSource = storeItemsBindingSource;
            ItemsListBox.DisplayMember = "Display";
            ItemsListBox.ValueMember = "Display";
        }

        private void InitShopCartItemListBox()
        {
            storeCartItemsBindingSource.DataSource = shoppingCartItems;
            ShoppingCartListBox.DataSource = storeCartItemsBindingSource;
            ShoppingCartListBox.DisplayMember = "Display";
            ShoppingCartListBox.ValueMember = "Display";
        }

        private void InitVendorBindingListBox()
        {
            vendorsBindingSource.DataSource = store.Vendors;
            VendorListBox.DataSource = vendorsBindingSource;
            VendorListBox.DisplayMember = "Display";
            VendorListBox.ValueMember = "Display";
        }

        private void SetupData()
        {
            store.Vendors.Add(new Vendor("Bill", "Smith"));
            store.Vendors.Add(new Vendor("Sue", "Jones"));

            store.Items.Add(new Item("Moby Dick", "A book about a whale", 10.4M, false, false, store.Vendors[0]));
            store.Items.Add(new Item("A Tale of Two Cities", "A book about a revolution", 15.4M, false, false, store.Vendors[1]));
            store.Items.Add(new Item("Harry Potter", "A book about a boy", 5.8M, false, false, store.Vendors[1]));
            store.Items.Add(new Item("Jane Eyre", "A book about a girl", 2.4M, false, false, store.Vendors[0]));

            // DebugStore();
        }

        private void DebugStore()
        {
            Debug.WriteLine("Vendors: " + store.Vendors.Count);
            Debug.WriteLine("Items: " + store.Items.Count);

            MessageBox.Show("Vendors: " + store.Vendors.Count);
            MessageBox.Show("Items: " + store.Items.Count);

            Trace.Write("Vendors: " + store.Vendors.Count);
            Trace.Write("Items: " + store.Items.Count);
        }

        private void ResetBindingSource(BindingSource bindingSource, bool isMetaDataChanged)
        {
            bindingSource.ResetBindings(isMetaDataChanged);
        }

        #region BUTTONS

        private void AddToCartButton_Click(object sender, EventArgs e)
        {
            Item selectedItem = (Item)ItemsListBox.SelectedItem;

            shoppingCartItems.Add(selectedItem);

            ResetBindingSource(storeCartItemsBindingSource, false);
        }

        private void MakePurchaseButton_Click(object sender, EventArgs e)
        {
            foreach (var item in shoppingCartItems)
            {
                if(item != null)
                {
                    item.SetSoldState(true);
                    item.Owner.ModifyPaymentDue(item.Price);
                    storeProfit += (1 - (decimal)item.Owner.Commission) * item.Price;

                    StoreProfitValueLabel.Text = string.Format("€{0}", storeProfit);
                }
                else
                {
                    MessageBox.Show("Foo!");
                }
            }

            shoppingCartItems.Clear();

            storeItemsBindingSource.DataSource = store.Items.Where(x => x.Sold == false).ToList();

            ResetBindingSource(storeCartItemsBindingSource, false);
            ResetBindingSource(storeItemsBindingSource, false);
            ResetBindingSource(vendorsBindingSource, false);
        }

        #endregion BUTTONS     
    }
}
