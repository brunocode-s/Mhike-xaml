using MHikePrototype.Data;
using MHikePrototype.Models;
using System;
using System.IO;
using System.Linq;

namespace MHikePrototype
{
    public partial class MainPage : ContentPage
    {
        private readonly DbService dbService;

        public MainPage()
        {
            InitializeComponent();

            // Initialize database
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "mhike.db");
            dbService = new DbService(dbPath);

            // Load recent hikes
            LoadRecentHikes();
        }

        private async void LoadRecentHikes()
        {
            try
            {
                var hikes = await dbService.GetAllHikes();
                var recentHikes = hikes.OrderByDescending(h => h.Id).Take(5).ToList();
                RecentHikesCollection.ItemsSource = recentHikes;

                TotalHikesLabel.Text = hikes.Count.ToString();
                double totalDistance = hikes.Sum(h => h.Length);
                TotalDistanceLabel.Text = $"{totalDistance} km";
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load hikes: {ex.Message}", "OK");
            }
        }

        private async void OnAddHikeClicked(object sender, EventArgs e)
        {
            // Navigate to AddHikePage
            await Navigation.PushAsync(new Pages.AddHikePage(dbService));
        }

        private async void OnViewHikesClicked(object sender, EventArgs e)
        {
            // Navigate to HikeListPage
            await Navigation.PushAsync(new Pages.HikeListPage(dbService));
        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            // Navigate to SettingsPage
            await Navigation.PushAsync(new Pages.SettingsPage());
        }

        // Optional: Refresh recent hikes when the page appears
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadRecentHikes();
        }
    }
}
