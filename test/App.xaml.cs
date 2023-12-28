using test.Pages;

namespace test
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            Routing.RegisterRoute("NewClientPage", typeof(NewClientPage)); 
            Routing.RegisterRoute("ClientsPage", typeof(ClientsPage));


        }
    }
}
