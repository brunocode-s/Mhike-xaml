using MHikePrototype.Data;
using MHikePrototype.Models;
using System.Collections.ObjectModel;

namespace MHikePrototype.Pages
{
    public partial class HikeDetailPage : ContentPage
    {
        private readonly DbService dbService;
        private readonly Hike hike;
        public ObservableCollection<Observation> Observations { get; set; } = new ObservableCollection<Observation>();

        public HikeDetailPage(DbService service, Hike hike)
        {
            InitializeComponent();
            dbService = service;
            this.hike = hike;
            ObsCollection.ItemsSource = Observations;

            LblName.Text = hike.Name;
            LblMeta.Text = $"{hike.Location} • {hike.DateIso} • {hike.Difficulty}";
            LblDesc.Text = hike.Description;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var obs = await dbService.GetObsForHike(hike.Id);
            Observations.Clear();
            foreach (var o in obs)
                Observations.Add(o);
        }

        private async void OnAddObsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddObservationPage(dbService, hike));
        }
    }
}
