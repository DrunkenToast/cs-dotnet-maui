namespace cs_dotnet_maui;

public partial class ItemDetailsPage : ContentPage
{
	public ItemDetailsPage(Item it)
	{
		InitializeComponent();
		BindingContext = it;
	}
}