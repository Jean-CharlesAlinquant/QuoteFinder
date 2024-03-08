namespace QuoteFinder.UserInteractor;

public interface IUserInteractor
{
    string ReadSingleWord(string prompt);
    int ReadInteger(string prompt);
    bool ReadYesNo(string message);

    void ShowMessage(string message);
}