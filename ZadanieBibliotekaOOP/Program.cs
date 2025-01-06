using static Program;
//teraz robie logowanie i rejestracje i probuje sprawdzic czy uzytrkonwik naprawde istneije w momencie logowania 
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
        }
    }
    public interface IOperacjeBiblioteczne
    {
        public void Wypozycz(Ksiazka ksiazka);
        public void Zwroc(Ksiazka ksiazka);
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
            foreach(Ksiazka k in ksiazki)
            {
                Console.WriteLine(k.Tytul);
            }
        }
            public void Wypozycz(Ksiazka ksiazka)
            {
            if (ksiazka.Status == true)
            {
                ksiazka.ZmienStatus(false);
                Console.WriteLine("Ksiazka zostala wypozyczona");
            }
            else
            {
                Console.WriteLine("Ksiazka jest juz wypozyczona");
            }
        }
        public void Zwroc(Ksiazka ksiazka)
        {
            ksiazka.ZmienStatus(true);
            Console.WriteLine("Ksiazka zostala zwrocona");
        }
    }
    public void menu()
    {
        Console.WriteLine("1. Zarejestuj");
        Console.WriteLine("2. Zaloguj");
        int wybor = -1;
        try
        {
            wybor = Int32.Parse(Console.ReadLine());
        }
        catch
        {
            Console.WriteLine("Nieprawidłowa wartość");
        }
        switch (wybor){
            case 1:
                Uzytkownik.Zarejestruj();
                break;
            case 2:
                Console.WriteLine("Podaj Login: ");
                break;
               

        }
    }
    public static void menuPoZalogowaniu()
    {
        Console.WriteLine("1. Wyswietl ksiazki");
        Console.WriteLine("2. Wypozycz ksiazki");
        Console.WriteLine("3. Zwroc ksiazki");
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
                Biblioteka.Wypozycz(ksiazka);
                break;
            case 3:
                Biblioteka.Zwroc(ksiazka);
                break;


        }
    }
}