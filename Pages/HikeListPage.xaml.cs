using MHikePrototype.Data;
using MHikePrototype.Models;
using System.Collections.ObjectModel;

namespace MHikePrototype.Pages
{
    public partial class HikeListPage : ContentPage
    {
        private readonly DbService dbService;
        public ObservableCollection<Hike> Hikes { get; set; } = new ObservableCollection<Hike>();

        public HikeListPage(DbService service)
        {
            InitializeComponent();
            dbService = service;
            HikesCollection.ItemsSource = Hikes;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var hikes = await dbService.GetAllHikes();
            Hikes.Clear();
            foreach (var h in hikes)
                Hikes.Add(h);
        }

        private async void OnHikeSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0) return;
            var selectedHike = (Hike)e.CurrentSelection[0];
            await Navigation.PushAsync(new HikeDetailPage(dbService, selectedHike));
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}
