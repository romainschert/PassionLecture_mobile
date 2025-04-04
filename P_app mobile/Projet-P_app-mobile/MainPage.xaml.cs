using System.Collections.ObjectModel;

namespace Projet_P_app_mobile
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; } = new ObservableCollection<string>();
        public Command<string> ItemTappedCommand { get; }

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;

            ItemTappedCommand = new Command<string>(OnItemTapped);
            // Chargement initial de 6 éléments
            LoadItems(6);
        }

        private void OnLoadMoreClicked(object sender, EventArgs e)
        {
            LoadItems(4); // Ajouter 4 nouveaux éléments
        }

        private void LoadItems(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Items.Add($"Item {Items.Count + 1}");
            }
        }
        private void OnItemTapped(string item)
        {
            DisplayAlert("Action", $"Vous avez cliqué sur {item}", "OK");
        }

    }

}
