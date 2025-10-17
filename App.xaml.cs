namespace MHikePrototype;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
	}

	// Override CreateWindow instead of setting MainPage
	protected override Window CreateWindow(IActivationState activationState)
	{
		return new Window(new AppShell());
	}
}
