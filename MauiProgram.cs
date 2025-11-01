using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using MHikePrototype.Data;
using MHikePrototype.Pages;

namespace MHikePrototype;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiMaps()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		builder.Services.AddSingleton<DbService>(sp =>
		{
			string dbPath = Path.Combine(FileSystem.AppDataDirectory, "mhike.db");
			return new DbService(dbPath);
		});

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddTransient<AddHikePage>();
		builder.Services.AddTransient<HikeListPage>();
		builder.Services.AddTransient<SettingsPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
