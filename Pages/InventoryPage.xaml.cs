using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace cs_dotnet_maui;

public class InventoryViewModel : ViewModelBase 
{
	private IDataStore _dataStore => DependencyService.Get<IDataStore>();

	public ObservableCollection<Item> ItemList { get; set; } = new ObservableCollection<Item>();

	public Command RefreshCommand { get; }
	Boolean _isRefreshing = false;
	public Boolean IsRefreshing { 
		get { return _isRefreshing; }
		set { _isRefreshing = value; OnPropertyChanged(); }
	}
	public InventoryViewModel() {
		RefreshCommand = new Command(RefreshAsync);
        _ = _updateAllItemsAsync(); // Just refresh
	}

    public async void RefreshAsync()
	{
		IsRefreshing = true;
        await _updateAllItemsAsync();
        IsRefreshing = false;
	}

	private async Task _updateAllItemsAsync()
	{
		// todo block refreshview
		try
		{
            _updateItemList(await _dataStore.GetAllItemsAsync());
			Console.WriteLine(JsonSerializer.Serialize(ItemList));
        }
        catch (Exception ex) {
			// TODO: handle error
		}
	}

	private void _updateItemList(IEnumerable<Item> items)
	{
		ItemList.Clear();

		foreach (Item item in items)
		{
			ItemList.Add(item);
		}
	}
}

public partial class InventoryPage : ContentPage
{
	InventoryViewModel vm;
	public InventoryPage()
	{
		InitializeComponent();
        Appearing += InventoryPage_Appearing;

		BindingContext = vm = new InventoryViewModel();
	}

    private void InventoryPage_Appearing(object sender, EventArgs e)
    {
		Console.WriteLine("Enter inventory page");
    }
}