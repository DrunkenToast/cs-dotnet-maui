using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace cs_dotnet_maui.Views;

public class InventoryViewModel : ViewModelBase 
{
	private IDataStore _dataStore => DependencyService.Get<IDataStore>();
	private INavigation _nav;

	public ObservableCollection<Item> ItemList { get; set; } = new ObservableCollection<Item>();
	private Item _selectedItem { get; set; }
	public Item SelectedItem { 
		get { return _selectedItem; }
		set {  _selectedItem = value; OnPropertyChanged();  }
	}

	public Command SelectionChangedCommand { get; }
	public Command RefreshCommand { get; }
	Boolean _isRefreshing = false;
	public Boolean IsRefreshing { 
		get { return _isRefreshing; }
		set { _isRefreshing = value; OnPropertyChanged(); }
	}
	public InventoryViewModel(INavigation nav) {
		_nav = nav;
		SelectionChangedCommand = new Command(_openSelectedItem);
		RefreshCommand = new Command(RefreshAsync);
        _ = _updateAllItemsAsync(); // Just refresh
	}

	private async void _openSelectedItem()
	{
		if (SelectedItem != null)
		{
			await _nav.PushAsync(new ItemDetailsPage(SelectedItem));
			Console.WriteLine("test!");
			SelectedItem = null; //	unselect from UI
        }
    }

    public async void RefreshAsync()
	{
		Console.WriteLine("yo?");
		IsRefreshing = true;
        await _updateAllItemsAsync();
        IsRefreshing = false;
	}

	private async Task _updateAllItemsAsync()
	{
		// todo block refreshview
		try
		{
            UpdateItemList(await _dataStore.GetAllItemsAsync());
			Console.WriteLine(JsonSerializer.Serialize(ItemList));
        }
        catch (Exception ex) {
			// TODO: handle error
		}
	}

	public void UpdateItemList(IEnumerable<Item> items)
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

		BindingContext = vm = new InventoryViewModel(Navigation);
	}

    private void InventoryPage_Appearing(object sender, EventArgs e)
    {
		Console.WriteLine("Enter inventory page");
		vm.RefreshAsync();
    }
}