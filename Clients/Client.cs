
using BooksAPI.Models.Domain;
namespace BooksAPI.Clients;
using Newtonsoft.Json;

public class Client
{
    private HttpClient _client;
    private static string _addressBook;
    private static string _key;

    public Client(IConfiguration configuration)
    {
        var googleBooksSettings = configuration.GetSection("GoogleBooksSettings");
        _addressBook = googleBooksSettings["AddressBook"]!;
        _key = googleBooksSettings["Key"]!;

        _client = new HttpClient();
        _client.BaseAddress = new Uri(_addressBook);
    }
    public async Task<List<GoogleBook>> GetBooksAsync(string name)
    {
        var response = await _client.GetAsync($"volumes?q={name}&key={_key}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var books = JsonConvert.DeserializeObject<GoogleBooks>(content); 
        return books.Items.ToList();
    }
    
}