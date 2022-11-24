namespace cs_dotnet_maui;

public class CrateViewModel {
	private IDataStore _dataStore => DependencyService.Get<IDataStore>();
	public Command UnboxButtonCommand { get; }
	private INavigation _nav;
	public CrateViewModel(INavigation nav) {
		UnboxButtonCommand = new Command(UnboxCrate);
		_nav = nav;
	}

	public async void UnboxCrate()
	{
		Console.WriteLine("Unboxing!!");
		await _nav.PushAsync(new ItemDetailsPage(new Item() { Id=1, Name="test", Quality=QualityType.Unique}) { Title = "yoinky sploinky"});
	}
}

public partial class CratePage : ContentPage
{
	int count = 0;
	CrateViewModel vm;

	public CratePage()
	{
		InitializeComponent();
		BindingContext = vm = new CrateViewModel(Navigation);
	}

	private void _unboxClicked(object sender, EventArgs e)
	{
	}
}

