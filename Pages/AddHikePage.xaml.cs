using MHikePrototype.Data;
using MHikePrototype.Models;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System;

namespace MHikePrototype.Pages
{
    public partial class AddHikePage : ContentPage
    {
        private readonly DbService dbService;
        private Pin? selectedPin;

        public AddHikePage(DbService dbService)
        {
            InitializeComponent();
            this.dbService = dbService ?? throw new ArgumentNullException(nameof(dbService));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await InitializeMap();
        }

        private async Task InitializeMap()
        {
            try
            {
                // Check and request location permission
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                }

                if (status == PermissionStatus.Granted)
                {
                    // Try to get location
                    var location = await Geolocation.Default.GetLastKnownLocationAsync();

                    if (location == null)
                    {
                        // If no last known location, get current location
                        var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                        location = await Geolocation.Default.GetLocationAsync(request);
                    }

                    if (location != null)
                    {
                        HikeMap.MoveToRegion(MapSpan.FromCenterAndRadius(
                            new Location(location.Latitude, location.Longitude),
                            Distance.FromKilometers(2)));
                    }
                    else
                    {
                        // No location available, use default
                        UseDefaultLocation();
                    }
                }
                else
                {
                    // Permission denied
                    await DisplayAlert("Location Permission",
                        "Location permission is required to show your position on the map. Using default location.",
                        "OK");
                    UseDefaultLocation();
                }
            }
            catch (FeatureNotSupportedException)
            {
                await DisplayAlert("Not Supported",
                    "Location services are not supported on this device. Using default location.",
                    "OK");
                UseDefaultLocation();
            }
            catch (FeatureNotEnabledException)
            {
                await DisplayAlert("Location Disabled",
                    "Please enable location services in your device settings. Using default location.",
                    "OK");
                UseDefaultLocation();
            }
            catch (PermissionException)
            {
                await DisplayAlert("Permission Error",
                    "Location permission is required. Using default location.",
                    "OK");
                UseDefaultLocation();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Map Error",
                    $"Unable to get your location: {ex.Message}\nUsing default location.",
                    "OK");
                UseDefaultLocation();
            }
        }

        private void UseDefaultLocation()
        {
            // Default location: Abuja, Nigeria
            HikeMap.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Location(9.0820, 8.6753),
                Distance.FromKilometers(5)));
        }

        private void OnMapClicked(object sender, MapClickedEventArgs e)
        {
            if (selectedPin != null)
                HikeMap.Pins.Remove(selectedPin);

            selectedPin = new Pin
            {
                Label = "Selected Location",
                Location = e.Location
            };

            HikeMap.Pins.Add(selectedPin);
            LocationEntry!.Text = $"{e.Location.Latitude:F5}, {e.Location.Longitude:F5}";
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var hike = new Hike
            {
                Name = NameEntry?.Text?.Trim() ?? "",
                Location = LocationEntry?.Text?.Trim() ?? "",
                Parking = ParkingEntry?.Text?.Trim() ?? "",
                Length = double.TryParse(LengthEntry?.Text, out var len) ? len : 0,
                Difficulty = DifficultyPicker?.SelectedItem?.ToString() ?? "",
                Description = DescriptionEditor?.Text?.Trim() ?? "",
                DateIso = DatePicker?.Date.ToString("yyyy-MM-dd") ?? DateTime.Today.ToString("yyyy-MM-dd")
            };

            if (string.IsNullOrWhiteSpace(hike.Name) || string.IsNullOrWhiteSpace(hike.Location))
            {
                await DisplayAlert("Validation", "Name and location are required.", "OK");
                return;
            }

            await dbService.InsertHike(hike);
            await DisplayAlert("Saved!", $"Hike '{hike.Name}' saved successfully.", "OK");
            await Shell.Current.GoToAsync("..");
        }
    }
}