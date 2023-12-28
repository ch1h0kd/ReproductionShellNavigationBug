using test.Data;
using test.ViewModels;

namespace test.Pages;
public partial class ClientsPage : ContentPage
{
    private readonly ClientsViewModel _viewModel;

    public ClientsPage(ClientsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    public ClientsViewModel get_viewModel() { return _viewModel; }

    protected async override void OnAppearing()
    {
        //base.OnAppearing();
        //Shell.SetTabBarIsVisible(this, true);
        await _viewModel.LoadClientsAsync();
    }

    private async void AddClient_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("NewClientPage");
    }
}