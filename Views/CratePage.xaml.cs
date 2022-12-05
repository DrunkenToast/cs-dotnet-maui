namespace cs_dotnet_maui.Views;

public class CrateViewModel : ViewModelBase {
	private IDataStore _dataStore => DependencyService.Get<IDataStore>();
	public Command UnboxButtonCommand { get; }
	private INavigation _nav;
	private ContentPage _page;
	public Boolean _isUnboxButtonEnabled = true;
	public Boolean IsUnboxButtonEnabled { 
		get { return _isUnboxButtonEnabled; }
		set {  _isUnboxButtonEnabled = value; OnPropertyChanged();  }
	}
	public CrateViewModel(INavigation nav, ContentPage page) {
		//UnboxButtonCommand = new Command(UnboxCrate);
		UnboxButtonCommand = new Command(UnboxCrate, CanClickUnbox);
        _nav = nav;
		_page = page;
	}

    private bool CanClickUnbox(object arg)
    {
        Console.WriteLine($"CanClick called! IsButtonEnabled: {!IsUnboxButtonEnabled}");
        return !IsUnboxButtonEnabled;
    }

    public async void UnboxCrate(object _arg)
	{
		try
		{
			IsUnboxButtonEnabled = false;
            var it = await _dataStore.UnboxItemAsync();
			IsUnboxButtonEnabled = true;
            await _nav.PushAsync(new ItemDetailsPage(it) { Title = "Unboxed a new item!" });
        }
        catch (Exception)
        {
			await _page.DisplayAlert("Unbox failed :((", "Something went wrong with unboxing the crate :(", "Darn...");
        }
		finally
		{
			IsUnboxButtonEnabled = true;
		}
    }
}

public partial class CratePage : ContentPage
{
	CrateViewModel vm;

	public CratePage()
	{
		InitializeComponent();
		BindingContext = vm = new CrateViewModel(Navigation, this);
    }

    private void _unboxClicked(object sender, EventArgs e)
	{
	}
}

