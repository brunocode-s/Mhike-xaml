using MHikePrototype.Data;
using MHikePrototype.Pages;
using MHikePrototype.Models;
using System;
using System.IO;
using System.Linq;
using System.Diagnostics;

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
            try
            {
                Debug.WriteLine("========================================");
                Debug.WriteLine("Button clicked!");
                Debug.WriteLine($"Navigating to: {nameof(AddHikePage)}");
                Debug.WriteLine($"Shell.Current is null: {Shell.Current == null}");

                await Shell.Current.GoToAsync(nameof(AddHikePage));

                Debug.WriteLine("Navigation completed successfully!");
                Debug.WriteLine("========================================");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("========================================");
                Debug.WriteLine($"ERROR during navigation: {ex.Message}");
                Debug.WriteLine($"Exception type: {ex.GetType().Name}");
                Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Debug.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                Debug.WriteLine("========================================");

                await DisplayAlert("Navigation Error", $"{ex.Message}", "OK");
            }
        }

        private async void OnViewHikesClicked(object sender, EventArgs e)
        {
            try
            {
                Debug.WriteLine($"Navigating to: {nameof(HikeListPage)}");
                await Shell.Current.GoToAsync(nameof(HikeListPage));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR navigating to HikeListPage: {ex.Message}");
                await DisplayAlert("Navigation Error", ex.Message, "OK");
            }
        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            try
            {
                Debug.WriteLine($"Navigating to: {nameof(SettingsPage)}");
                await Shell.Current.GoToAsync(nameof(SettingsPage));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR navigating to SettingsPage: {ex.Message}");
                await DisplayAlert("Navigation Error", ex.Message, "OK");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadRecentHikes();
        }
    }
}