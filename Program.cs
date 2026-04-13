using Newtonsoft.Json; // Biblioteka, skirta JSON konvertavimui.
//Vartotojo aprašas.
class User
{
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public string City { get; set; } = "";
}
//Admin perima visas User savybes ir prideda savo.
class Admin : User
{
    public string Permission { get; set; } = "";
}

class Program
{
    static void Main(string[] args)
    {// Failų pavadinimai, kuriuose bus saugomi duomenis.
        string file1 = "users.json";
        string file2 = "usertypes.json";


        string jsonFromFile = File.ReadAllText(file1); //Programa paima tekstą iš failo.

        //json pavertimas.
        //Naudojamas ??: jei failas tuščias, sukuriamas naujas sąrašas.
        List <User> users = JsonConvert.DeserializeObject<List<User>>(jsonFromFile) ?? new List<User>(); 

        Console.WriteLine("USERS:");
        foreach (var u in users)
        {
            Console.WriteLine($"{u.Name}, {u.Age}, {u.City}");
        }

        Console.WriteLine();

        //Duomenu papildymas.
        users.Add(new User
        {
            Name = "Tom",
            Age = 30,
            City = "Berlin"
        });
        //Sąrašas paverčiamas atgal į tekstą.
        File.WriteAllText(file1, JsonConvert.SerializeObject(users, Formatting.Indented));

        //Sukuriamas administratoriaus sąrašas.
        List<Admin> admins = new List<Admin>
        {
            new Admin
            {
                Name = "Mike",
                Age = 35,
                City = "Rome",
                Permission = "Full Access"
            }
        };

        //išsaugojamas š atskira failą.
        File.WriteAllText(file2, JsonConvert.SerializeObject(admins, Formatting.Indented));
        //nuskaitomas failas.
        List<Admin> loadedAdmins = JsonConvert.DeserializeObject<List<Admin>>(File.ReadAllText(file2)) ?? new List<Admin>();

        Console.WriteLine("ADMINS:");
        foreach (var a in loadedAdmins)
        {
            Console.WriteLine($"{a.Name}, {a.Age}, {a.City}, {a.Permission}");
        }
    }
}