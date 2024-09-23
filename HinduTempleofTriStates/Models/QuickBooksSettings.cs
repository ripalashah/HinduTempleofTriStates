public class QuickBooksSettings
{
    public Guid Id { get; set; }
    public required string ClientId { get; set; }
    public required string ClientSecret { get; set; }
    public required string RedirectUrl { get; set; }
    public required string Environment { get; set; }
   
    public required string AuthUrl { get; set; }  // Add this if needed
    public required string AccessTokenUrl { get; set; }  // Add this if needed
    public required string BaseUrl { get; set; }
    public string? RealmId { get; internal set; }
    public QuickBooksSettings()
    {
        RealmId = string.Empty; // Or set to a default value if not nullable
    }
}
