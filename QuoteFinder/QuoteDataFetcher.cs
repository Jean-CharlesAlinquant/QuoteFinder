
using QuoteFinder.DataAccess;

namespace QuoteFinder;

public class QuoteDataFetcher : IQuoteDataFetcher
{
    private readonly IQuotesApiDataReader _quotesApiDataReader;

    public QuoteDataFetcher(IQuotesApiDataReader quotesApiDataReader)
    {
        _quotesApiDataReader = quotesApiDataReader;
    }

    public async Task<IEnumerable<string>> FetchDataFromAllPagesAsync(int numberOfPages, int quotesPerPage)
    {
        var tasks = new List<Task<string>>();

        for (int i = 0; i < numberOfPages; i++)
        {
            var fetchDataTask = _quotesApiDataReader.ReadAsync(i + 1, quotesPerPage);
            tasks.Add(fetchDataTask);
        }

        // Task.WaitAll(tasks.ToArray()); //blocking call
        // return tasks.Select(task => task.Result).ToList();

        return (await Task.WhenAll(tasks)).ToList();
    }

    public async Task<IEnumerable<string>> FetchDataFromAllPagesAsyncSlow(int numberOfPages, int quotesPerPage)
    {
        var result = new List<string>();

        for (int i = 0; i < numberOfPages; i++)
        {
            var responseBody = await _quotesApiDataReader.ReadAsync(i + 1, quotesPerPage);
            result.Add(responseBody);
        }

        return result;
    }
}