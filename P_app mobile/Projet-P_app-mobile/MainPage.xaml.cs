using System.Collections.ObjectModel;
using System.IO.Compression;
using System.Net;
using System.Windows.Input;
using System.Xml;


namespace Projet_P_app_mobile
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; } = new();

        public ICommand ItemTappedCommand { get; }
        public ICommand NavigateToDetailCommand { get; }

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;

            NavigateToDetailCommand = new Command(OnItemTapped);

            // Exemple de données test
            Items.Add("Item 1");
            Items.Add("Item 2");
            Items.Add("Item 3");
            Items.Add("Item 4");

            ItemTappedCommand = new Command<string>(item =>
            {
                // Action à exécuter quand on clique un bouton de la collection
                Console.WriteLine($"Item cliqué : {item}");
            });
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            bool isToggled = e.Value;
            Console.WriteLine($"Switch XML toggled: {isToggled}");
            // Ajoute ici la logique si nécessaire
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string url = endpoint.Text;
            try
            {
                using HttpClient client = new();
                string data = await client.GetStringAsync(url);

                // Simule une mise à jour de l'interface
                title.Text = "Titre du livre (extrait)";
                cover.Source = "nouvelle_couverture.png";

                Console.WriteLine("Données chargées : " + data);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", ex.Message, "OK");
            }
        }

        private void OnLoadMoreClicked(object sender, EventArgs e)
        {
            // Exemple de chargement supplémentaire
            Items.Add($"Item {Items.Count + 3}");
        }
        private async void OnItemTapped()
        {
            await Navigation.PushAsync(new DetailPage());
        }
    }

}
