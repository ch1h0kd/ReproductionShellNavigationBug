using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using test.Data;
using test.Models;
using System.Collections.ObjectModel;
namespace test.ViewModels
{
    public partial class ClientsViewModel : ObservableObject
    {
        private readonly DatabaseContext _context;//instance from DatabaseContext
        public ClientsViewModel(DatabaseContext context)
        {
            _context = context;
        }

        [ObservableProperty]
        private ObservableCollection<Client> _clients = new();

        [ObservableProperty]
        private Client _operatingClient = new();//client that is currently eddited

        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private string _busyText;

        public async Task LoadClientsAsync()
        {
            await ExecuteAsync(async () =>
            {
                var clients = await _context.GetAllAsync<Client>();
                Console.WriteLine("length = " + clients.LongCount());

                if (clients is not null && clients.Any())//if we have at least one client
                {
                    Clients ??= new ObservableCollection<Client>(); //if null, initialize

                    foreach (var client in clients)//insert each client into a obervable collection Clients 
                    {
                        Clients.Add(client);
                    }
                }
            }, "Fetching clients...");
        }

        [RelayCommand]
        private void SetOperatingClient(Client? client) => OperatingClient = client ?? new();

        [RelayCommand]
        private async Task SaveClientAsync()//loading
        {
            if (OperatingClient is null)//do nothing
                return;
            var busyText = OperatingClient.Id == 0 ? "Creating client..." : "Updating client";
            await ExecuteAsync(async () =>
            {
                Console.WriteLine("OperatingClient.Id = " + OperatingClient.Id);
                await _context.AddItemAsync<Client>(OperatingClient);//create
                Clients.Add(OperatingClient);//add this client to the collection
                
                SetOperatingClientCommand.Execute(new());//reset the value
            }, busyText);

        }

        [RelayCommand]
        private async Task DeleteClientAsync(int id)
        {
            await ExecuteAsync(async () =>
            {
                if (await _context.DeteleItemByKeyAsync<Client>(id))
                {
                    var client = Clients.FirstOrDefault(p => p.Id == id);//get the item
                    Clients.Remove(client);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Delete Error", "Client was not deleted", "Ok");
                }
            }, "Deleting client...");
        }

        //if not string provided, display default message which is before and after the operation given
        private async Task ExecuteAsync(Func<Task> operation, string? busyText = null)
        {
            IsBusy = true;
            BusyText = busyText ?? "Processing...";
            try
            {
                Console.WriteLine("here");
                await operation?.Invoke();
            }
            finally
            {
                IsBusy = false;//after the operations, not busy anymore
                BusyText = "Processing...";
            }
        }


    }
}
