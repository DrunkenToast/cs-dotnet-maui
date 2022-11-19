namespace cs_dotnet_maui;

public partial class InventoryPage : ContentPage
{
	public InventoryPage()
	{
		InitializeComponent();
        Appearing += InventoryPage_Appearing;
	}

    private void InventoryPage_Appearing(object sender, EventArgs e)
    {
		Console.WriteLine("Enter inventory page");
    }
}