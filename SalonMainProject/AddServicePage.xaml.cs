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
    /// Логика взаимодействия для AddServicePage.xaml
    /// </summary>
    public partial class AddServicePage : Page
    {
        private Service service = new Service();
        public AddServicePage(Service selectService)
        {
            InitializeComponent();

            if (selectService != null)
                service = selectService;

            DataContext = service;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(service.Title))
                errors.AppendLine("Название услуги не введено");
            if (service.DurationInSeconds < 0)
                errors.AppendLine("Длительность не может быть отрицательной");
            if (service.DurationInSeconds < 14400)
                errors.AppendLine("Длительность не может быть более 4 часов");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (service.ID == 0)
            {
                salonEntities.GetContext().Service.Add(service);
            }

            try
            {
                salonEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные сохранены");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void btnPhotoPath_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            if (dlg.ShowDialog() == true && !string.IsNullOrWhiteSpace(dlg.FileName))
                textFotoPath.Text = dlg.FileName.ToString();
                textFotoPath.Focus();
        }
    }
}

