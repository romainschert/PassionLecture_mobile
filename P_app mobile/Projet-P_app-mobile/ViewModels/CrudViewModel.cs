
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VersOne.Epub;

namespace Projet_P_app_mobile.ViewModels
{
    public partial class CrudViewModel : ObservableObject
    {

        public static string baseAddress = "http://10.0.2.2:3000";
        public static string allBooksUrl = $"{baseAddress}/api/books";
        public static string oneBookUrl = $"{baseAddress}/api/books/1";
        HttpClient _httpClient;

        [ObservableProperty]
        private ObservableCollection<Book>? books;

        public CrudViewModel()
        {
            _httpClient = new HttpClient();
            GetBooks();
        }

        [RelayCommand]
        public async void GetBooks()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(allBooksUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    ApiResponseList? apiResponse = JsonSerializer.Deserialize<ApiResponseList>(json, options);

                    if (apiResponse != null && apiResponse.data != null)
                    {
                        Books = new ObservableCollection<Book>(apiResponse.data);
                        Debug.WriteLine($"Livres chargés : {Books.Count}");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tERROR: {ex}");
            }
        }

        [RelayCommand]
        public async Task GetOneBookAsync()
        {
            Debug.WriteLine("CRUDVIEWMODEL - Récupération d'un livre");
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(oneBookUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    ApiResponse? apiResponse = JsonSerializer.Deserialize<ApiResponse>(json, options);

                    if (apiResponse?.data != null)
                    {
                        var livre = apiResponse.data;

                        // Facultatif : lecture de l'epub
                        if (livre.livre?.data != null)
                        {
                            byte[] epubBytes = livre.livre.data.ToArray();
                            using MemoryStream epubStream = new MemoryStream(epubBytes);
                            EpubBook book = await EpubReader.ReadBookAsync(epubStream);

                            string title = book.Title;
                            string author = string.Join(", ", book.AuthorList);
                            string content = string.Join("\n\n", book.ReadingOrder.Select(c => c.Content));

                            Debug.WriteLine("Titre: " + title);
                            Debug.WriteLine("Auteur: " + author);
                            Debug.WriteLine("Contenu:\n" + content);
                        }

                        // ?? Met à jour la collection liée à l'affichage
                        Books = new ObservableCollection<Book> { livre };
                        Debug.WriteLine("Livre affiché dans la collection");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tERROR: {ex}");
            }
        }
    }
}