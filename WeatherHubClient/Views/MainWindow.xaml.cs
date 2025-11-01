using Microsoft.Data.Sqlite;
using System.Collections.ObjectModel;
using System.Net.Http;
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
using WeatherHubClient.Models;
using WeatherHubClient.Services;


namespace WeatherClient.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        private readonly MainViewModel ViewModel;

        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = ViewModel;
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.ResizeMode = ResizeMode.CanResize;
            this.WindowState = WindowState.Normal;
            this.Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var lastCityName = ViewModel.GetLastCity()?.Name;
            if (!string.IsNullOrEmpty(lastCityName))
            {
                await ViewModel.LoadWeatherAsync(lastCityName);
                UpdateGraphLabels();
            }
        }


        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {


            string typedCity = cityTextBox.Text.Trim();

            string? validationError = WeatherHubClient.Validation.CityNameValidator.Validate(typedCity);
            if (validationError != null)
            {
                MessageBox.Show(validationError, "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (typedCity == "Enter city...")
            {
                MessageBox.Show("Please enter a city.");
                return;
            }

            var weatherData = await ViewModel.LoadWeatherAsync(typedCity);
            UpdateGraphLabels();

            if (weatherData == null)
            {
                MessageBox.Show("The entered city was not found. Please enter a valid city.");
                cityTextBox.Text = "Enter city...";
                MainWindow_Loaded(sender, e);
                return;
            }

            ViewModel.AddRecent(weatherData.CityName!);
            cityTextBox.Text = "Enter city...";
        }


        private void cityTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            cityTextBox.Clear();
        }


        private void cityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CityCard_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is City city)
            {
                string typedCity = cityTextBox.Text.Trim();
                cityTextBox.Text = city.Name;
            }


        }
        private void StarButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is City city)
            {
                ViewModel.AddFavorite(city.Name!);

            }
        }


        private void RemoveFavorite_Click(object sender, RoutedEventArgs e)
        {

            if (sender is Button button && button.DataContext is City city)
            {

                ViewModel.RemoveFavorite(city);
            }
        }


        private void FavoriteItem_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is City city)
            {
                string typedCity = cityTextBox.Text.Trim();
                cityTextBox.Text = city.Name;
            }
        }


        private void UpdateGraphLabels()
        {
            LabelsCanvas.Children.Clear();

            var forecasts = ViewModel.CurrentWeatherData?.HourlyForecasts;
            if (forecasts == null || !forecasts.Any()) return;

            double canvasWidth = 650;
            double canvasHeight = 120;
            int count = forecasts.Count;
            double stepX = canvasWidth / (count - 1);

            double maxTemp = forecasts.Max(f => f.Temperature);
            double minTemp = forecasts.Min(f => f.Temperature);

            int step = 5;

            for (int i = 0; i < count; i++)
            {
                if (i % step != 0) continue;

                var f = forecasts[i];
                double x = i * stepX;


                double yTemp = canvasHeight - ((f.Temperature - minTemp) / (maxTemp - minTemp) * canvasHeight);
                var tempLabel = new TextBlock
                {
                    Text = $"{f.Temperature}°",
                    Foreground = Brushes.Black,
                    FontSize = 12
                };
                Canvas.SetLeft(tempLabel, x - 10);
                Canvas.SetTop(tempLabel, yTemp - 20);
                LabelsCanvas.Children.Add(tempLabel);


                var timeLabel = new TextBlock
                {
                    Text = f.ForecastTime.ToString("HH:mm"),
                    Foreground = Brushes.Gray,
                    FontSize = 10
                };
                Canvas.SetLeft(timeLabel, x - 15);
                Canvas.SetTop(timeLabel, canvasHeight + 5);
                LabelsCanvas.Children.Add(timeLabel);
            }
        }


    }
}


