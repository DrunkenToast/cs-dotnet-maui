namespace cs_dotnet_maui;

static class Environment
{
    public const string baseUrl = "http://10.0.2.2:5121/";
}

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		DependencyService.Register<ApiDataStore>();

		MainPage = new NavigationPage(new AppTabbedPage());
	}
}
