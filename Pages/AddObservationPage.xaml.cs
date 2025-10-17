using MHikePrototype.Data;
using MHikePrototype.Models;
using System;

namespace MHikePrototype.Pages
{
    public partial class AddObservationPage : ContentPage
    {
        private readonly DbService dbService;
        private readonly Hike hike;

        public AddObservationPage(DbService service, Hike hike)
        {
            InitializeComponent();
            dbService = service;
            this.hike = hike;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var obs = new Observation
            {
                HikeId = hike.Id,
                ObservationText = ObsEditor.Text?.Trim(),
                Comments = CommentsEditor.Text?.Trim(),
                ObservedAtIso = DateTime.UtcNow.ToString("s")
            };

            await dbService.InsertObs(obs);
            await DisplayAlert("Saved!", "Observation added.", "OK");
            await Navigation.PopAsync();
        }
    }
}
