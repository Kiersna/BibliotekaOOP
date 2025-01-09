using static Program;
//logowanie chodzi juz poprawnie i teraz analiza
//ogaranc statici i nie statici czy sa w dobrych miejscach itp i jak jest lepiej
public class Program
{

    public abstract class ElementBiblioteki
    {
        public string? Tytul { get; set; }
        public string? Autor { get; set; }
    }
    public class Ksiazka : ElementBiblioteki
    {
        private string? isbn;
        private bool status;
        public string ISBN
        {
            get { return isbn; }
            set
            {
                if (value.Length == 3)
                {
                    isbn = value;
                }
                else
                {
                    throw new Exception("ISBN musi mieć 3 znaki");
                }
            }
        }
        public bool Status
        {
            get { return status; }
            set { status = value; }
        }
        public void ZmienStatus(bool nowyStatus)
        {
            Status = nowyStatus;
        }
    }
    public class Uzytkownik
    {
        public string login { get; set; }
        public string imie { get; set; }
        public static List<Uzytkownik> uzytkownicy = new List<Uzytkownik>();
        public Uzytkownik(string login, string imie)
        {
            this.login = login;
            this.imie = imie;
        }
        public static void Zarejestruj()
        {

            Console.WriteLine("Login: ");
            string login = Console.ReadLine();
            if (uzytkownicy.Exists(u => u.login == login))
            {
                Console.WriteLine("Uzytownik z tym loginem juz istnieje");
                Zarejestruj();
            }
            Console.WriteLine("Imie: ");
            string imie = Console.ReadLine();
            Uzytkownik nowy = new Uzytkownik(login, imie);
            uzytkownicy.Add(nowy);
            Console.WriteLine($"Zarejestrowano uzytkownika o imieniu {nowy.imie} z loginem {nowy.login}");
        }
        public static void Zaloguj()
        {
            Console.WriteLine("Podaj login:");
            string login = Console.ReadLine();
            if (uzytkownicy.Exists(u => u.login == login))
            {
                menuPoZalogowaniu();
            }
            else { Console.WriteLine("nie pozdro nie ma takiego uzytkownika"); }
        }
    }
    public interface IOperacjeBiblioteczne
    {
        public void Wypozycz(string tytul);
        public void Zwroc(string tytul);
    }
    public class Biblioteka : IOperacjeBiblioteczne
    {
        public static List<Ksiazka> ksiazki = new List<Ksiazka>();
        public Biblioteka()
        {
            // Dodajemy przykładowe książki do biblioteki
            ksiazki.Add(new Ksiazka
            {
                Tytul = "Władca Pierścieni",
                Autor = "J.R.R. Tolkien",
                ISBN = "001",
                Status = true
            });

            ksiazki.Add(new Ksiazka
            {
                Tytul = "Harry Potter i Kamień Filozoficzny",
                Autor = "J.K. Rowling",
                ISBN = "002",
                Status = true
            });

            ksiazki.Add(new Ksiazka
            {
                Tytul = "Zbrodnia i Kara",
                Autor = "Fiodor Dostojewski",
                ISBN = "003",
                Status = true
            });

            ksiazki.Add(new Ksiazka
            {
                Tytul = "1984",
                Autor = "George Orwell",
                ISBN = "004",
                Status = true
            });
        }

        public static void WyswietlKsiazki()
        {
            foreach (Ksiazka k in ksiazki)
            {
                Console.WriteLine(k.Tytul);
            }
        }
        public void Wypozycz(string tytul)
        {
            var ksiazka = ksiazki.FirstOrDefault(k => k.Tytul.Equals(tytul, StringComparison.OrdinalIgnoreCase));
            if (ksiazka == null)
            {
                Console.WriteLine("Nie odnaleziono książki o podanym tytule.");
                return;
            }
            if (ksiazka.Status)
            {
                ksiazka.ZmienStatus(false);
                Console.WriteLine($"Ksiazka {ksiazka.Tytul} zostala wypozyczona");
            }
            else
            {
                Console.WriteLine($"Ksiazka {ksiazka.Tytul} jest juz wypozyczona");
            }
        }
        public void Zwroc(string tytul)
        {
            var ksiazka = ksiazki.FirstOrDefault(k => k.Tytul.Equals(tytul, StringComparison.OrdinalIgnoreCase));
            if (ksiazka == null)
            {
                Console.WriteLine("Nie odnaleziono książki o podanym tytule.");
                return;
            }
            if (!ksiazka.Status)
            {
                ksiazka.ZmienStatus(true);
                Console.WriteLine($"Ksiazka {ksiazka} zostala zwrocona");
            }
            else
            {
                Console.WriteLine($"Ksiazka {ksiazka} nie byla wypozyczona");
            }
            //czyli akutalnie program zaklada ze biblioteka bedzie miala tylko 1 egzemplarz ksiazki
            //i nie ma sprawdzania czy oddaje ja ta sama osoba co wypozyczyla
        }
    }

    public static void Main()
    {
        int wybor = -1;
        while (wybor != 0) { 
        Console.WriteLine("1. Zarejestuj");
        Console.WriteLine("2. Zaloguj");
            Console.WriteLine("0. Zakoncz program");
        try
        {
            wybor = Int32.Parse(Console.ReadLine());
        }
        catch
        {
            Console.WriteLine("Nieprawidłowa wartość");
        }
            switch (wybor)
            {
                case 1:
                    Uzytkownik.Zarejestruj();
                    break;
                case 2:
                    Uzytkownik.Zaloguj();
                    break;
                case 0:
                    return;
            }
        }
    }
    public static void menuPoZalogowaniu()
    {
        var biblioteka = new Biblioteka();
        while (true)
        {
            Console.WriteLine("1. Wyswietl ksiazki");
            Console.WriteLine("2. Wypozycz ksiazki");
            Console.WriteLine("3. Zwroc ksiazki");
            Console.WriteLine("4. Wyloguj");
            Console.WriteLine("Wybierz opcje: ");
            int wybor = -1;
            try
            {
                wybor = Int32.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Nieprawidłowa wartość");
            }
            switch (wybor)
            {
                case 1:
                    Biblioteka.WyswietlKsiazki();
                    break;
                case 2:
                    Console.WriteLine("Podaj tytuł książki do wypożyczenia: ");
                    string tytulWypozycz = Console.ReadLine();
                    biblioteka.Wypozycz(tytulWypozycz);
                    break;
                case 3:
                    Console.WriteLine("Podaj tytuł książki do zwrotu: ");
                    string tytulZwrot = Console.ReadLine();
                    biblioteka.Zwroc(tytulZwrot);
                    break;
                case 4:
                    Console.WriteLine("Zakonczono program");
                    return;
                default:
                    Console.WriteLine("Niepoprawny wybór");
                    break;
            }
        }
    }
}