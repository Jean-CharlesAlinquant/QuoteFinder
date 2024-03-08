using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using QuoteFinder;
using QuoteFinder.DataAccess;
using QuoteFinder.Models;
using QuoteFinder.UserInteractor;

var userInteractor = new ConsoleUserInteractor();
try
{
    string word = userInteractor.ReadSingleWord(
        "What word are you looking for?");
    int numberOfPages = userInteractor.ReadInteger(
        "How many pages do you want to read?");
    int quotesPerPage = userInteractor.ReadInteger(
        "How many quotes per page?");
    bool shallProcessInParallel = userInteractor.ReadYesNo(
        "Shall process pages in parallel?");

    using var quotesApiDataReader = new QuotesApiDataReader();
    var quoteDataFetcher = new QuoteDataFetcher(quotesApiDataReader);

    userInteractor.ShowMessage("Fetching data...");
    IEnumerable<string> data = await quoteDataFetcher.FetchDataFromAllPagesAsync(
        numberOfPages,
        quotesPerPage);
    userInteractor.ShowMessage("Data is ready.");

    Stopwatch stopwatch = Stopwatch.StartNew();
    var quoteDataProcessor = new QuoteDataProcessor(userInteractor);
    await quoteDataProcessor.ProcessAsync(data, word, shallProcessInParallel);
    stopwatch.Stop();
    Console.WriteLine($"Took: {stopwatch.ElapsedMilliseconds} ms.");
}
catch (Exception ex)
{
    userInteractor.ShowMessage("Exception thrown: " + ex.Message);
}

Console.WriteLine("Program is finished.");
Console.ReadKey();

