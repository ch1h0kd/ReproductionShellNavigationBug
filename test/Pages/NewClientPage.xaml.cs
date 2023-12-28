using test.ViewModels;

namespace test.Pages;

public partial class NewClientPage : ContentPage
{
    private ClientsViewModel _viewModel;

    public NewClientPage(ClientsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;

    }

    private async void BackToClient_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("ClientsPage");
    }
}