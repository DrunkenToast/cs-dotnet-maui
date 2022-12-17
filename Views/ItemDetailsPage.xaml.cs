namespace cs_dotnet_maui.Views;
public class ItemDetailViewModel : ViewModelBase
{
	private IDataStore _dataStore => DependencyService.Get<IDataStore>();
	public Command DeleteButtonCommand { get; }
	public Command BackButtonCommand{ get; }
	private Item _item;
	private ContentPage _page;
	public int Id { 
		get { return _item.Id; }
		set {  _item.Id = value; OnPropertyChanged();  }
	}
	public string Name { 
		get { return _item.Name; }
		set {  _item.Name= value; OnPropertyChanged();  }
	}
	public QualityType Quality { 
		get { return _item.Quality; }
		set {  _item.Quality = value; OnPropertyChanged();  }
	}

	public Color Color
	{
		get { return _item.Color; }
	}

	public ItemDetailViewModel(Item it, ContentPage page)
	{
		_item = it;
		_page = page;
		DeleteButtonCommand = new Command(_deleteItem);
		BackButtonCommand = new Command(() => _page.Navigation.PopAsync());
	}
	private async void _deleteItem(object arg)
	{
		try
		{
            await _dataStore.DeleteItemAsync(_item);
			await _page.Navigation.PopAsync();
        }
		catch (Exception)
		{
			await _page.DisplayAlert("Delete failed :((", "Something went wrong with scraping the item :(", "Darn...");
		}
    }
}
public partial class ItemDetailsPage : ContentPage
{
	private ItemDetailViewModel _vm;
	public ItemDetailsPage(Item it)
	{
		InitializeComponent();
		BindingContext = _vm = new ItemDetailViewModel(it, this);
	}

}