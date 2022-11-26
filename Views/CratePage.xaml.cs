namespace cs_dotnet_maui.Views;

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
		var it = await _dataStore.UnboxItemAsync();
		await _nav.PushAsync(new ItemDetailsPage(it) { Title = "Unboxed a new item!"});
	}
}

public partial class CratePage : ContentPage
{
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

