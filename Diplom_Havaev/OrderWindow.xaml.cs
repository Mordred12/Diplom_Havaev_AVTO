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
    public partial class OrderWindow : Window
    {
        public OrderWindow()
        {
            InitializeComponent();
            DGridOrders.ItemsSource = AutoShop_BDEntities.GetContext().Orders.ToList();
        }

        private void btnGoToClientes_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newMainWindow = new MainWindow();
            this.Close();
            newMainWindow.Show();
        }

        private void btnGoToProducts_Click(object sender, RoutedEventArgs e)
        {
            ProductsWindow newProductsWindow = new ProductsWindow();
            this.Close();
            newProductsWindow.Show();
        }

        private void btnGoToSales_Click(object sender, RoutedEventArgs e)
        {
            SalesWindow newSalesWindow = new SalesWindow();
            this.Close();
            newSalesWindow.Show();
        }

        private void BtnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderEditingWindow newOrderEditingWindow = new OrderEditingWindow(null);
            newOrderEditingWindow.Owner = this;
            this.Hide();
            newOrderEditingWindow.Show();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            AutoShop_BDEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            DGridOrders.ItemsSource = AutoShop_BDEntities.GetContext().Orders.ToList();
        }

        private void BtnDelOrder_Click(object sender, RoutedEventArgs e)
        {
            if(DGridOrders.SelectedIndex != -1)
            {
                var orderForRemoving = DGridOrders.SelectedItems.Cast<Orders>().ToList();
                if (MessageBox.Show($"Вы точно хотите удалить этот элемент?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        AutoShop_BDEntities.GetContext().Orders.RemoveRange(orderForRemoving);
                        AutoShop_BDEntities.GetContext().SaveChanges();
                        MessageBox.Show("Данные удалены!");
                        DGridOrders.ItemsSource = AutoShop_BDEntities.GetContext().Clients.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }

        private void BtnEditingOrser_Click(object sender, RoutedEventArgs e)
        {
            Orders selectedOrder = (Orders)DGridOrders.SelectedItem;
            if (selectedOrder == null)
            {
                MessageBox.Show("Выделите в таблице клиента для внесения изменений", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                OrderEditingWindow newOrderEditingWindow = new OrderEditingWindow(selectedOrder);
                newOrderEditingWindow.Title = string.Format($"Редактирование заказа");
                newOrderEditingWindow.HeadText.Text = string.Format($"Редактирование заказа");
                newOrderEditingWindow.Owner = this;
                this.Hide();
                newOrderEditingWindow.Show();
            }
        }

        private void TxtSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridOrders.ItemsSource = AutoShop_BDEntities.GetContext().Orders.Where(c => c.Clients.fio.Contains(TxtSearchBar.Text)).ToList();
        }
    }
}
