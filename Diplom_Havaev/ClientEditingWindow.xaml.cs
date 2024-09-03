using MaterialDesignThemes.Wpf;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace Diplom_Havaev
{
    public partial class ClientEditingWindow : Window
    {
        private Clients currentClient = new Clients();
        public ClientEditingWindow(Clients selectedClient)
        {
            InitializeComponent();

            if (selectedClient != null)
                currentClient = selectedClient;

            DataContext = currentClient;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string strPattern = @"(\+7|8|\b)[\(\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[)\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)";
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(currentClient.fio))
                errors.Append("Укажиет имя");
            if (string.IsNullOrEmpty(currentClient.number) || !(Regex.IsMatch(currentClient.number, strPattern)))
                errors.Append("Номер телефона не указан или указан в неверном формате");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (currentClient.id == 0)
                AutoShop_BDEntities.GetContext().Clients.Add(currentClient);
            try
            {
                AutoShop_BDEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
                this.Close();
                this.Owner.Show();
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
    }
}
