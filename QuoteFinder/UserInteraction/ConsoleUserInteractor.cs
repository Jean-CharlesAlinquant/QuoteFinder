namespace QuoteFinder.UserInteractor;

public class ConsoleUserInteractor : IUserInteractor
{
    public int ReadInteger(string prompt)
    {
        int result;
        do
        {
            Console.WriteLine(prompt);
        } while (!int.TryParse(Console.ReadLine(), out result));
        return result;
    }

    public string ReadSingleWord(string message)
    {
        string result;
        do
        {
            Console.WriteLine(message);
            result = Console.ReadLine();
        } while (!IsValidWord(result));
        return result;
    }

    public bool ReadYesNo(string message)
    {
        Console.WriteLine($"{message} ('y' for 'yes', anything else for 'no')");
        var input = Console.ReadLine();
        return input == "y";
    }

    private bool IsValidWord(string input)
    {
        return
            input is not null &&
            input.All(char.IsLetter);
    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }
}