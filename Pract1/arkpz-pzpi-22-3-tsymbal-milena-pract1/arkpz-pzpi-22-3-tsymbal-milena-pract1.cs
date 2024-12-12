using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

void ProcessData(List<int> data){
    if (data==null||data.Count==0){
        Console.WriteLine("No data to process.");
        return;
        }

int result=0;

    foreach (var item in data){
        result+=item;}

if (result>1000){
        Console.WriteLine("Result is too large: "+result+". Цей рядок спеціально доданий, щоб перевищити рекомендовану довжину рядка в 80-120 символів, і продемонструвати, як рядок може бути розбитий на декілька частин, зберігаючи логічну структуру та покращуючи читабельність коду.");}
    else{
        Console.WriteLine("Processed result: "+result);}
}

void ProcessDataMethod(List<int> data) 
{
    if (data == null || data.Count == 0)
    {
        Console.WriteLine("No data to process.");
        return;
    }

    int result = 0;

    foreach (var item in data)
    {
        result += item;
    }

    if (result > 1000)
    {
        Console.WriteLine("Result is too large: " + result + ". Цей рядок спеціально доданий, щоб перевищити " +
            "рекомендовану довжину рядка в 80-120 символів, і продемонструвати, як рядок може бути розбитий на " +
            "декілька частин, зберігаючи логічну структуру та покращуючи читабельність коду.");
    }
    else
    {
        Console.WriteLine("Processed result: " + result);
    }
}


//Поганий приклад
public class Emp
{
    public string fn;

    public string ln;

    public void p()
    {
        Console.WriteLine(fn + " " + ln);
    }

}

//Гарний приклад
public class Employee
{
    public string firstName;

    public string lastName;

    public void PrintFullName()
    {
        Console.WriteLine(firstName + " " + lastName);
    }
}

//Поганий приклад
//Це змінна для зберігання імені користувача
string userName = "John";

// Це умова перевірки
if (userName == "John")
{
    // Вивести ім'я
    Console.WriteLine("Hello, John!");
}

// Це цикл
for (int i = 0; i < 10; i++)
{
    Console.WriteLine(i); // Вивести число
}

//Гарний приклад
// Ім'я користувача, яке використовується для привітання в системі
string userName = "John";

// Перевіряємо, чи користувач є адміністратором
if (userName == "Admin")
{
    // Виводимо привітання для адміністратора
    Console.WriteLine("Welcome back, Admin!");
}

// Генеруємо список чисел для відображення в інтерфейсі
for (int i = 0; i < 10; i++)
{
    Console.WriteLine(i); // Показуємо номер елемента на екрані
}

public class Example
{
    private List<int> numbers;

    public Example()
    {
        numbers = new List<int>();
    }

    public void AddNumber(int number)
    {
        numbers.Add(number);
    }

    public int Sum()
    {
        int sum = 0;
        foreach (var number in numbers)
        {
            sum += number;
        }
        return sum;
    }

    public async Task<int> GetDataAndCalculateSum()
    {
        var data = await FetchData();
        foreach (var item in data)
        {
            AddNumber(item);
        }
        return Sum();
    }

    private Task<List<int>> FetchData()
    {
        return Task.FromResult(new List<int> { 1, 2, 3, 4, 5 });
    }
}

public class ExampleClass
{
    public int Sum(IEnumerable<int> numbers)
    {
        return numbers.Aggregate(0, (acc, number) => acc + number);
    }

    public async Task<int> GetDataAndCalculateSum()
    {
        var data = await FetchData();
        return Sum(data);
    }

    private Task<List<int>> FetchData()
    {
        return Task.FromResult(new List<int> { 1, 2, 3, 4, 5 });
    }
}


//Поганий приклад
public class Example
{
    public void ProcessFile(string filePath)
    {
        FileStream file = null;
        file = new FileStream(filePath, FileMode.Open);
        StreamReader reader = new StreamReader(file);
        string content = reader.ReadToEnd();
        Console.WriteLine(content);
    }
}
//Гарний приклад
public class ExampleClass
{
    private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

    public void ProcessFile(string filePath)
    {
        FileStream file = null;
        try
        {
            Logger.Info($"Спроба відкрити файл: {filePath}");
            file = new FileStream(filePath, FileMode.Open);
            StreamReader reader = new StreamReader(file);
            string content = reader.ReadToEnd();
            Console.WriteLine(content);
            Logger.Info($"Файл успішно прочитано: {filePath}");
        }
        catch (FileNotFoundException ex)
        {
            Logger.Error(ex, $"Файл не знайдено: {filePath}");
        }
        catch (UnauthorizedAccessException ex)
        {
            Logger.Error(ex, $"Недостатньо прав доступу до файлу: {filePath}");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, $"Невідома помилка при обробці файлу: {filePath}");
        }
        finally
        {
            if (file != null)
            {
                file.Close();
                Logger.Info($"Файл закрито: {filePath}");
            }
        }
    }
}


