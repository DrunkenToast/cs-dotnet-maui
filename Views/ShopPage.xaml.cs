using System.Diagnostics;

namespace cs_dotnet_maui.Views;

public class ShopViewModel : ViewModelBase
{
	private IDataStore _dataStore => DependencyService.Get<IDataStore>();
	public float PriceKey { get; } = 2.49F;
	private int _keys = 0;
	public int Keys { 
		get { return _keys; }
		set { _keys = value; OnPropertyChanged();  }
	}

	public Command BuyCommand { get; }
	ShopPage _page;

	public ShopViewModel(ShopPage page) {
		_page = page;
		BuyCommand = new Command<string>(
			execute: BuyAction
		);
	}

	public async void BuyAction(string amount)
	{
		Debug.WriteLine(amount);
		if (int.TryParse(amount, out int amt))
		{
			bool answer = await _page.DisplayAlert(
				$"Buy {amount} keys?", $"This will cost ya {amt * PriceKey} euro's!",
				"Yes take my money!!!",
				"On second thought..."
            );
            Debug.WriteLine("Answer: " + answer);
			if (!answer) return;

			try
			{
                await _dataStore.PurchaseKey(amt);
				RefreshKeyAmountAsync();
                await _page.DisplayAlert("THANKS!!!", "Your poor life decision went through. Come back any time!", "Yay!");
            }
			catch
			{
                await _page.DisplayAlert("Failed to spend money", "Your poor life decision didn't go through. PLEASE try again later.", "Darn...");
            }
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

public partial class ShopPage : ContentPage
{
	ShopViewModel _vm;

	public ShopPage()
	{
		InitializeComponent();
		Appearing += ShopPageAppearing;
		BindingContext = _vm = new ShopViewModel(this);
	}
	
	private void ShopPageAppearing(object sender, EventArgs e)
	{
		Console.WriteLine("Enter shop page");
		_vm.RefreshKeyAmountAsync();
	}
}