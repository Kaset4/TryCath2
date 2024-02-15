public class InvalidInputException : Exception
{
    public InvalidInputException(string message) : base(message)
    {
    }
}

public class NameList
{
    public event EventHandler SortingRequested;

    private List<string> names = new List<string> { "Иванов", "Петров", "Сидоров", "Козлов", "Михайлов" };

    public void SortNames(bool ascending)
    {
        if (ascending)
            names.Sort();
        else
            names.Sort((a, b) => b.CompareTo(a));
    }

    public void OnSortingRequested()
    {
        SortingRequested?.Invoke(this, EventArgs.Empty);
    }

    public void PrintNames()
    {
        Console.WriteLine("Список фамилий:");
        foreach (var name in names)
        {
            Console.WriteLine(name);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        NameList nameList = new NameList();
        nameList.SortingRequested += (sender, e) =>
        {
            Console.WriteLine("Выберите тип сортировки: 1 - А-Я, 2 - Я-А");
            try
            {
                int choice = int.Parse(Console.ReadLine());
                if (choice != 1 && choice != 2)
                {
                    throw new InvalidInputException("Введите 1 или 2.");
                }

                bool ascending = (choice == 1);
                nameList.SortNames(ascending);
                nameList.PrintNames();
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: Введите число.");
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        };

        nameList.OnSortingRequested();
    }
}