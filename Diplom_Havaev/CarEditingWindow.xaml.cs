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
    
    public partial class CarEditingWindow : Window
    {
        public Cars currentCar = new Cars();
        public Clients currentClient = new Clients();
        public List<string> modelsLadaCar = new List<string> { "1111 (Ока)","2101","2102","2104","2105","2106","2107","2108",
                "2109", "2110", "2111", "2112", "21099", "2113 (Самара)" ,"2114 (Самара)","2115 (Самара)","2120 (Надежда)","2129","4х4 2121 (Нива)",
                "4х4 2131 (Нива)","4х4 Бронто","4х4 Урбан","Веста","Веста Кросс","Веста Спорт","Гранта","Гранта Кросс","Гранта Спорт","Калина",
                "Калина Кросс","Калина Спорт","Ларгус","Ларгус Кросс","Приора","Х-Кросс 5","Х-рей","Х-рей Кросс"};
        public CarEditingWindow(Clients selectedClient)
        {
            List<string> brandCar = new List<string> { "Toyota", "Лада(ВАЗ)", "Nissan", "Honda", "Hyundai", "Chevrolet", "Reno" };
            

            InitializeComponent();

            if (selectedClient != null )
                currentClient = selectedClient;

            DataContext = currentCar;

            cmbFuel.ItemsSource = new List<string> { "АИ-92","АИ-95","АИ-98","АИ-100","Дизель"};
            cmbTransmission.ItemsSource = new List<string> {"МКПП","АКПП","Робот","Вариатор"};
            cmbTypeDrive.ItemsSource = new List<string> {"4WD","Передний","Задний"};
            cmbEngineCapacity.ItemsSource = new List<string> {"0.7", "0.8", "1.0", "1.1", "1.2", "1.3", "1.4", "1.5", "1.6", "1.7", "1.8", "1.9",
                "2.0", "2.2", "2.3", "2.4", "2.5", "2.7", "2.8", "3.0", "3.2", "3.3", "3.5", "3.6", "4.0", "4.2", "4.4", "4.5", "4.6", "4.7", "5.0", "5.5", "5.7", "6.0"};
            cmbBrandCar.ItemsSource = brandCar;
            cmbBodyType.ItemsSource = new List<string> { "Седан", "Хэтчбек", "Универсал", "Купе", "Кабриолет", "Минивэн", "Внедорожник", "Кроссовер" };

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            currentCar.idClient = currentClient.id;
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
                
            try
            {
                AutoShop_BDEntities.GetContext().Cars.Add(currentCar);
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

        private void cmbBrandCar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbModelCar.IsEnabled = true;
            if (cmbBrandCar.SelectedIndex == 1)
                cmbModelCar.ItemsSource = modelsLadaCar;
            else
                cmbModelCar.ItemsSource = null;
        }
    }
}
