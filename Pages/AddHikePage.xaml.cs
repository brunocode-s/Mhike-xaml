using MHikePrototype.Data;
using MHikePrototype.Models;

namespace MHikePrototype.Pages;

public partial class AddHikePage : ContentPage
{
    private readonly DbService dbService;

    public AddHikePage(DbService service)
    {
        InitializeComponent();
        dbService = service;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var hike = new Hike
        {
            Name = NameEntry.Text?.Trim(),
            Location = LocationEntry.Text?.Trim(),
            Parking = ParkingEntry.Text?.Trim(),
            Length = double.TryParse(LengthEntry.Text, out var len) ? len : 0,
            Difficulty = DifficultyPicker.SelectedItem?.ToString(),
            Description = DescriptionEditor.Text?.Trim(),
            DateIso = DatePicker.Date.ToString("yyyy-MM-dd")
        };

        if (string.IsNullOrWhiteSpace(hike.Name) || string.IsNullOrWhiteSpace(hike.Location))
        {
            await DisplayAlert("Validation", "Name and location are required.", "OK");
            return;
        }

        await dbService.InsertHike(hike);
        await DisplayAlert("Saved!", $"Hike {hike.Name} saved.", "OK");
        await Navigation.PopAsync();
    }
}
