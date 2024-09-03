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
    public class OrdersType
    {
        public string orderType { get; }
        public int orderPrise { get; }
        public OrdersType(string orderType, int orderPrise)
        {
            this.orderType = orderType;
            this.orderPrise = orderPrise;
        }
    }
    public partial class OrderEditingWindow : Window
    { 
        private Orders currentOrders = new Orders();
        public OrderEditingWindow(Orders selectedOrder)
        {
            InitializeComponent();

            if (selectedOrder != null)
                currentOrders = selectedOrder;

            DataContext = currentOrders;
            CmbCustomers.ItemsSource = AutoShop_BDEntities.GetContext().Clients.ToList();
            CmbCustomers.DisplayMemberPath = "fio";
            cmbOrderType.ItemsSource = new List<OrdersType>() 
            {
                new OrdersType("Замена генератора",1500),
                new OrdersType("Замена ремня генератора", 1000),
                new OrdersType("Замена задних стоек", 2000),
                new OrdersType("Замена рулевых наконечников", 1700),
                new OrdersType("Замена бензонасоса", 500),
                new OrdersType("Замена воздушного фильтра", 300),
                new OrdersType("Замена масляного фильтра", 700),
                new OrdersType("Замена тормозной жидкости", 700)
            };
            cmbOrderType.DisplayMemberPath = "orderType";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            currentOrders.orderType = ((OrdersType)cmbOrderType.SelectedItem).orderType;
            currentOrders.idClient = ((Clients)CmbCustomers.SelectedItem).id;
            currentOrders.numCar = ((Cars)CmbCars.SelectedItem).carNum;
            StringBuilder errors = new StringBuilder();

            if (CmbCustomers.SelectedIndex == -1)
                errors.AppendLine("Укажите клиента");
            if (CmbCars.SelectedIndex == -1)
                errors.AppendLine("Машина не указана");
            if (string.IsNullOrEmpty(currentOrders.orderType))
                errors.AppendLine("Тип заказа не указан");
            if (currentOrders.prise < 0)
                errors.AppendLine("Укажите цену");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (currentOrders.id == 0)
                AutoShop_BDEntities.GetContext().Orders.Add(currentOrders);
            try
            {
                AutoShop_BDEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                this.Owner.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        private void CmbCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CmbCars.IsEnabled = true;
            CmbCars.ItemsSource = AutoShop_BDEntities.GetContext().Cars.Where(c => c.idClient == ((Clients)CmbCustomers.SelectedItem).id).ToList();
            CmbCars.DisplayMemberPath = "carNum";
        }

        private void cmbOrderType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtPriseOrder.Text = ((OrdersType)cmbOrderType.SelectedItem).orderPrise.ToString();
        }
    }
}
