using cs_dotnet_maui.Exceptions;

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

	private int _keys = 0;
	public int Keys { 
		get { return _keys; }
		set { _keys = value; OnPropertyChanged();  }
	}
	public CrateViewModel(INavigation nav, ContentPage page) {
		UnboxButtonCommand = new Command(UnboxCrate, CanClickUnbox);
        _nav = nav;
		_page = page;
	}

    private bool CanClickUnbox(object arg)
    {
        return !IsUnboxButtonEnabled;
    }

    public async void UnboxCrate(object _arg)
	{
		try
		{
			IsUnboxButtonEnabled = false;
            var it = await _dataStore.UnboxItemAsync();
			IsUnboxButtonEnabled = true;
			RefreshKeyAmountAsync(); // Technically not necessary since when we return from the other page it will also return
            await _nav.PushAsync(new ItemDetailsPage(it) { Title = "Unboxed a new item!" });
        }
        catch (NoKeysException)
        {
			await _page.DisplayAlert("Not enough keys...", "This world ain't free, chap. You can buy keys in the shop.", "Darn...");
        }
        catch
        {
			await _page.DisplayAlert("Unbox failed :((", "Something went wrong with unboxing the crate :(", "Darn...");
        }
		finally
		{
			IsUnboxButtonEnabled = true;
		}
    }

	public async void RefreshKeyAmountAsync()
	{
		try
		{
            Keys = await _dataStore.GetKeysAmount();
        }
		catch
		{
			await _page.DisplayAlert("Where did the keys go?", "Request failed to get the key amount. Now where did I put them...", "Darn...");
		}

    }
}

public partial class CratePage : ContentPage
{
	CrateViewModel _vm;

	public CratePage()
	{
		InitializeComponent();
		BindingContext = _vm = new CrateViewModel(Navigation, this);
		Appearing += CratePageAppearing;
    }

	private void CratePageAppearing(object sender, EventArgs e)
	{
		_vm.RefreshKeyAmountAsync();
	}
}

