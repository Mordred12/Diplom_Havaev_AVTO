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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Diplom_Havaev
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DGridClienes.ItemsSource = AutoShop_BDEntities.GetContext().Clients.ToList();
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

        private void btnGoToSales_Click(object sender, RoutedEventArgs e)
        {
            SalesWindow newSalesWindow = new SalesWindow();
            this.Close();
            newSalesWindow.Show();
        }

        private void BtnAddClient_Click(object sender, RoutedEventArgs e)
        {
            ClientEditingWindow newClientEditingWindow = new ClientEditingWindow(null);
            newClientEditingWindow.Owner = this;
            this.Hide();
            newClientEditingWindow.Show();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            AutoShop_BDEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            DGridClienes.ItemsSource = AutoShop_BDEntities.GetContext().Clients.ToList();
        }

        private void BtnDelClient_Click(object sender, RoutedEventArgs e)
        {
            if(DGridClienes.SelectedIndex != -1)
            {
                var clientesForRemoving = DGridClienes.SelectedItems.Cast<Clients>().ToList();
                if (MessageBox.Show($"Вы точно хотите удалить этот элемент?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        AutoShop_BDEntities.GetContext().Clients.RemoveRange(clientesForRemoving);
                        AutoShop_BDEntities.GetContext().SaveChanges();
                        MessageBox.Show("Данные удалены!");
                        DGridClienes.ItemsSource = AutoShop_BDEntities.GetContext().Clients.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }

        private void BtnEditingClient_Click(object sender, RoutedEventArgs e)
        {
            Clients selectedClient = (Clients)DGridClienes.SelectedItem;
            if (selectedClient == null)
            {
                MessageBox.Show("Выделите в таблице клиента для внесения изменений", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                ClientEditingWindow newClientEditingWindow = new ClientEditingWindow(selectedClient);
                newClientEditingWindow.Title = string.Format($"Редактирование клиента {selectedClient.fio}");
                newClientEditingWindow.HeadText.Text = string.Format($"Редактирование клиента {selectedClient.fio}");
                newClientEditingWindow.Owner = this;
                this.Hide();
                newClientEditingWindow.Show();
            }
        }

        private void btnAddCar_Click(object sender, RoutedEventArgs e)
        {
            CarEditingWindow newCarEditingWindow = new CarEditingWindow((sender as Button).DataContext as Clients);
            newCarEditingWindow.Owner = this;
            this.Hide();
            newCarEditingWindow.Show();
        }

        private void TxtSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridClienes.ItemsSource = AutoShop_BDEntities.GetContext().Clients.Where(c => c.fio.Contains(TxtSearchBar.Text)).ToList();
        }
    }
}
