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

namespace SalonMainProject
{
    /// <summary>
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        public ServicePage()
        {
            InitializeComponent();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddServicePage((sender as Button).DataContext as Service));
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddServicePage(null));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var serviceRemove = dGridService.SelectedItems.Cast<Service>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {serviceRemove.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    salonEntities.GetContext().Service.RemoveRange(serviceRemove);
                    salonEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    dGridService.ItemsSource = salonEntities.GetContext().Service.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility != Visibility.Visible)
                dGridService.ItemsSource = salonEntities.GetContext().Service.ToList();
        }

        private void textBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void comboBoxService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
