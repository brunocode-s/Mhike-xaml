using MHikePrototype.Data;
using System.IO;

namespace MHikePrototype.Pages
{
    public partial class SettingsPage : ContentPage
    {
        private readonly DbService dbService;

        public SettingsPage()
        {
            InitializeComponent();
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "mhike.db");
            dbService = new DbService(dbPath);
        }

        private async void OnResetClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Confirm", "Are you sure you want to reset the database?", "Yes", "No");
            if (confirm)
            {
                await dbService.ResetDb();
                await DisplayAlert("Reset", "Database cleared.", "OK");
            }
        }
    }
}
