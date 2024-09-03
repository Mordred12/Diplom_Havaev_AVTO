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
    
    public partial class SalesWindow : Window
    {
        
        public SalesWindow()
        {
            InitializeComponent();
            DGridSales.ItemsSource = AutoShop_BDEntities.GetContext().Sales.ToList();
        }

        private void btnGoToOrders_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow newOrderWindow = new OrderWindow();
            this.Close();
            newOrderWindow.Show();
        }

        private void btnGoToProducts_Click(object sender, RoutedEventArgs e)
        {
            ProductsWindow newProductsWindow = new ProductsWindow();
            this.Close();
            newProductsWindow.Show();
        }

        private void btnGoToClient_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newMainWindow = new MainWindow();
            this.Close();
            newMainWindow.Show();
        }

        private void BtnAddSale_Click(object sender, RoutedEventArgs e)
        {
            customerChoiceWindow newCustomerChoiceWindow = new customerChoiceWindow();
            this.Close();
            newCustomerChoiceWindow.Show();
        }

        private void BtnDelSale_Click(object sender, RoutedEventArgs e)
        {
            if (DGridSales.SelectedIndex != -1)
            {
                var saleForRemoving = DGridSales.SelectedItems.Cast<Sales>().ToList();
                if (MessageBox.Show($"Вы точно хотите удалить этот элемент?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        AutoShop_BDEntities.GetContext().Sales.RemoveRange(saleForRemoving);
                        AutoShop_BDEntities.GetContext().SaveChanges();
                        MessageBox.Show("Данные удалены!");
                        DGridSales.ItemsSource = AutoShop_BDEntities.GetContext().Sales.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }

        private void TxtSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridSales.ItemsSource = AutoShop_BDEntities.GetContext().Sales.Where(c => c.Clients.fio.Contains(TxtSearchBar.Text)).ToList();
        }
    }
}
