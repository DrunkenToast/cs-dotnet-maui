using System.Diagnostics;

namespace cs_dotnet_maui.Views;

public class ShopViewModel : ViewModelBase
{
    private IDataStore _dataStore => DependencyService.Get<IDataStore>();
    private int _keys = 0;
	public int Keys { 
		get { return _keys; }
		set { _keys = value; OnPropertyChanged();  }
	}

	public Command BuyCommand { get; }

	public ShopViewModel() {
		BuyCommand = new Command<int>(
			execute: BuyAction
		);
	}

	public async void BuyAction(int amount)
	{
		Debug.WriteLine(amount);
		Keys += 1;
		await _dataStore.PurchaseKey(amount);
	}

	public async void RefreshKeysAsync()
	{
		Keys = await _dataStore.GetKeysAmount();
	}
}

public partial class ShopPage : ContentPage
{
	ShopViewModel _vm;

	public ShopPage()
	{
		InitializeComponent();
		BindingContext = _vm = new ShopViewModel();
	}
	
	private void ShopPageAppearing(object sender, EventArgs e)
	{
		Console.WriteLine("Enter shop page");
		_vm.RefreshKeysAsync();
	}
}