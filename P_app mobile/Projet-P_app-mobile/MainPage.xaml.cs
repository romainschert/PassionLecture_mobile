using System.Collections.ObjectModel;
using System.IO.Compression;
using System.Net;
using System.Windows.Input;
using System.Xml;
using Projet_P_app_mobile.ViewModels;


namespace Projet_P_app_mobile
{
    public partial class MainPage : ContentPage
    {
        private CrudViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();
       

        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            bool isToggled = e.Value;
            Console.WriteLine($"Switch XML toggled: {isToggled}");
            // Ajoute ici la logique si nécessaire
        }

        
        private async void OnItemTapped()
        {
            await Navigation.PushAsync(new DetailPage());
        }
    }

}
