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
    public partial class ProductEditingWindow : Window
    {
        public bool prov = false;
        Products currentProduct = new Products();
        public ProductEditingWindow(Products selectedProduct)
        {
            InitializeComponent();
            if(selectedProduct != null)
                currentProduct = selectedProduct;
            else
                prov = true;
            DataContext = currentProduct;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(currentProduct.article))
                errors.Append("Укажите артикул");
            if (string.IsNullOrEmpty(currentProduct.productName))
                errors.Append("Укажите наименование");
            if (currentProduct.price < 0)
                errors.Append("Укажите цену");
            if (string.IsNullOrEmpty(currentProduct.manufacturer))
                errors.Append("Укажите производителя");
            if (string.IsNullOrEmpty(currentProduct.typeAutoParts))
                errors.Append("Укажите тип автозапчасти");
            if (currentProduct.quantity <= 0)
                errors.Append("Укажите количество");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (prov)
                AutoShop_BDEntities.GetContext().Products.Add(currentProduct);
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
    }
}
