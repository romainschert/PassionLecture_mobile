using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Projet_P_app_mobile
{
    public class Book
    {
        public int Livre_id { get; set; }

        public string titre { get; set; }

        public int nbPages { get; set; }

        public string extrait { get; set; }

        public string resume { get; set; }

        public DateTime anneeEdition { get; set; }

        public string _imageCouverture { get; set; }

        public DateTime datePublication { get; set; }

        public int editeur_fk { get; set; }

        public int categorie_fk { get; set; }

        public int auteur_fk { get; set; }

        public BookBuffer? livre { get; set; }

    }

    public class BookBuffer
    {
        public string? type { get; set; }
        public List<byte> data { get; set; }
    }
    //pour un livre
    public class ApiResponse
    {
        public string message { get; set; }
        public Book data { get; set; }
    }
    //pour tout les livrwe
    public class ApiResponseList
    {
        public string message { get; set; }
        public List<Book> data { get; set; }
    }
}
