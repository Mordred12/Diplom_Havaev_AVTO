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
    public partial class ProductsWindow : Window
    {
        public ProductsWindow()
        {
            InitializeComponent();
            DGridProducts.ItemsSource = AutoShop_BDEntities.GetContext().Clients.ToList();
        }

        private void btnGoToClientes_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newMainWindow = new MainWindow();
            this.Close();
            newMainWindow.Show();
        }

        private void btnGoToOrders_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow newOrderWindow = new OrderWindow();
            this.Close();
            newOrderWindow.Show();
        }

        private void btnGoToSales_Click(object sender, RoutedEventArgs e)
        {
            SalesWindow newSalesWindow = new SalesWindow();
            this.Close();
            newSalesWindow.Show();
        }

        private void BtnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductEditingWindow newProductEditingWindow = new ProductEditingWindow(null);
            newProductEditingWindow.Owner = this;
            this.Hide();
            newProductEditingWindow.Show();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            AutoShop_BDEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            DGridProducts.ItemsSource = AutoShop_BDEntities.GetContext().Products.ToList();
        }

        private void BtnDelProduct_Click(object sender, RoutedEventArgs e)
        {
            if (DGridProducts.SelectedIndex != -1)
            {
                var productsForRemoving = DGridProducts.SelectedItems.Cast<Products>().ToList();
                if (MessageBox.Show($"Вы точно хотите удалить этот элемент?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        AutoShop_BDEntities.GetContext().Products.RemoveRange(productsForRemoving);
                        AutoShop_BDEntities.GetContext().SaveChanges();
                        MessageBox.Show("Данные удалены!");
                        DGridProducts.ItemsSource = AutoShop_BDEntities.GetContext().Products.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }

        private void BtnEditingProduct_Click(object sender, RoutedEventArgs e)
        {
            Products selectedProduct = (Products)DGridProducts.SelectedItem;
            if (selectedProduct == null)
            {
                MessageBox.Show("Выделите в таблице клиента для внесения изменений", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                ProductEditingWindow newProductEditingWindow = new ProductEditingWindow(selectedProduct);
                newProductEditingWindow.Title = string.Format($"Редактирование продукта");
                newProductEditingWindow.HeadText.Text = string.Format($"Редактирование продукта");
                newProductEditingWindow.Owner = this;
                this.Hide();
                newProductEditingWindow.Show();
            }
        }

        private void TxtSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridProducts.ItemsSource = AutoShop_BDEntities.GetContext().Products.Where(c => c.productName.Contains(TxtSearchBar.Text)).ToList();
        }
    }
}
