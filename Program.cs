using System;
using System.IO;

delegate bool NameValidator(string name);

class Program
{
    static void Main()
    {
        var validator = new NameValidator(CheckName);
        var userCreator = new UserCreator();
        
        userCreator.OnUserCreated += HandleUserCreated;

        Console.Write("Введите имя: ");
        string name = Console.ReadLine();
        
        if(!validator(name))
        {
            Console.WriteLine("Имя не может быть пустым!");
            return;
        }

        Console.Write("Введите возраст: ");
        int age;
        try
        {
            age = int.Parse(Console.ReadLine());
        }
        catch
        {
            Console.WriteLine("Некорректный возраст!");
            return;
        }
        
        if(age < 18)
        {
            Console.WriteLine("Поздравляю! У вас прекрасный возраст.");
            return;
        }

        userCreator.Create(name, age);
    }
    
    static bool CheckName(string name)
{
    return !string.IsNullOrEmpty(name);
}
    
    private static void HandleUserCreated(string name, int age)
    {
        Console.WriteLine($"Поздравляю! {name}, {age} - зарегистрирован!");
        
        if(age >= 18)
        {
            File.AppendAllText("users.txt", $"{name} , {age}\n");
        }
    }
}

class UserCreator
{
    public event Action<string, int> OnUserCreated;
    
    public void Create(string name, int age)
    {
        OnUserCreated?.Invoke(name, age);
    }
}