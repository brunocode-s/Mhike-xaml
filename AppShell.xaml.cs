using MHikePrototype.Pages;

namespace MHikePrototype
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();

			Routing.RegisterRoute(nameof(AddHikePage), typeof(AddHikePage));
			Routing.RegisterRoute(nameof(HikeListPage), typeof(HikeListPage));
			Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
		}
	}
}
