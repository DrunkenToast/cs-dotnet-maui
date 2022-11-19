namespace cs_dotnet_maui;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppTabbedPage();
	}
}
