using Newtonsoft.Json;

namespace BooksAPI.Models.Domain;

public class GoogleBooks
{
    [JsonProperty("totalItems")]
    public int TotalItems { get; set; }

    [JsonProperty("items")]
    public List<GoogleBook> Items { get; set; }
}