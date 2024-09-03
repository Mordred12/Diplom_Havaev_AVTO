using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Diplom_Havaev
{
    public partial class SalesAddWindow : Window
    {
        public Sales currentSale = new Sales();
        public ProduceForSale currentPFS = new ProduceForSale();
        public string AllPFSId;
        public int finalPrise = 0;
        bool cl = true;
        public SalesAddWindow(Sales curSale)
        {
            InitializeComponent();
            currentSale = curSale;
            DGridPFS.ItemsSource = AutoShop_BDEntities.GetContext().ProduceForSale.Where(c => c.saleId == ((Sales)currentSale).id).ToList();
            cmbProducts.ItemsSource = AutoShop_BDEntities.GetContext().Products.Where(c => c.quantity > 0).ToList();
            cmbProducts.DisplayMemberPath = "productName";
            
        }

        private void btnAddPFS_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(txtBoxQuantity.Text) <= ((Products)cmbProducts.SelectedItem).quantity)
            {
                currentPFS = new ProduceForSale();
                currentPFS.article = ((Products)cmbProducts.SelectedItem).article;
                currentPFS.saleId = currentSale.id;
                currentPFS.quantity = int.Parse(txtBoxQuantity.Text);
                try
                {
                    AutoShop_BDEntities.GetContext().ProduceForSale.Add(currentPFS);
                    
                    finalPrise = finalPrise + ((Products)cmbProducts.SelectedItem).price * currentPFS.quantity;
                    FinalPrise.Text = $"Итоговая цена: {finalPrise}";
                    Products currentProduct = (Products)cmbProducts.SelectedItem;
                    currentProduct.quantity = currentProduct.quantity - currentPFS.quantity;
                    AutoShop_BDEntities.GetContext().SaveChanges();
                    AutoShop_BDEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                    DGridPFS.ItemsSource = AutoShop_BDEntities.GetContext().ProduceForSale.Where(c => c.saleId == currentSale.id).ToList();
                    ProduceForSale PFS = AutoShop_BDEntities.GetContext().ProduceForSale.OrderByDescending(z => z.id).First();
                    if (AllPFSId == null)
                        AllPFSId = $"{PFS.id}";
                    else
                        AllPFSId = AllPFSId + $",{PFS.id}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
                MessageBox.Show($"Нет такого количества товара! Максимум {((Products)cmbProducts.SelectedItem).quantity}");
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            cl = false;
            currentSale.produceForSale = AllPFSId;
            currentSale.price = finalPrise;
            AutoShop_BDEntities.GetContext().SaveChanges();
            SalesWindow newSalesWindow = new SalesWindow();
            this.Close();
            newSalesWindow.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (cl)
            {
                List<Sales> saleForRemoving = AutoShop_BDEntities.GetContext().Sales.Where(c => c.id == currentSale.id).ToList();
                AutoShop_BDEntities.GetContext().Sales.RemoveRange(saleForRemoving);
                AutoShop_BDEntities.GetContext().SaveChanges();
                SalesWindow newSalesWindow = new SalesWindow();
                newSalesWindow.Show();
            }
            this.Close();
        }
    }
}
