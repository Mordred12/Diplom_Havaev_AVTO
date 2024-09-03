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
    public partial class customerChoiceWindow : Window
    {
        public Sales currentSale = new Sales();
        public customerChoiceWindow()
        {
            InitializeComponent();
            cmbClients.ItemsSource = AutoShop_BDEntities.GetContext().Clients.ToList();
            cmbClients.DisplayMemberPath = "fio";
        }

        private void btnChoice_Click(object sender, RoutedEventArgs e)
        {
            currentSale.idClient = ((Clients)cmbClients.SelectedItem).id;
            currentSale.price = 0;
            if (cmbClients.SelectedItem != null) 
            {
                try
                {
                    AutoShop_BDEntities.GetContext().Sales.Add(currentSale);
                    AutoShop_BDEntities.GetContext().SaveChanges();
                    SalesAddWindow newSalesAddWindow = new SalesAddWindow(currentSale);
                    newSalesAddWindow.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (currentSale.idClient != ((Clients)cmbClients.SelectedItem).id)
            {
                SalesWindow newSalesWindow = new SalesWindow();
                this.Close();
                newSalesWindow.Show();
            }
            else
                this.Close();
        }
    }
}
