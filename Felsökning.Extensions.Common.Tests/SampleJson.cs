namespace Felsökning.Extensions.Common.Tests
{
    [ExcludeFromCodeCoverage]
    public class SampleJson
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; } = 0;

        [JsonPropertyName("id")]
        public int Id { get; set; } = 0;

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("completed")]
        public bool Completed { get; set; }
    }
}
