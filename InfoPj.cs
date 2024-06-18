// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
using System.Text.Json.Serialization;
    public class Powerstats
    {
        [JsonPropertyName("intelligence")]
        public string intelligence { get; set; }

        [JsonPropertyName("strength")]
        public string strength { get; set; }

        [JsonPropertyName("speed")]
        public string speed { get; set; }

        [JsonPropertyName("durability")]
        public string durability { get; set; }

        [JsonPropertyName("power")]
        public string power { get; set; }

        [JsonPropertyName("combat")]
        public string combat { get; set; }
    }

    public class Root
    {
        [JsonPropertyName("response")]
        public string response { get; set; }

        [JsonPropertyName("id")]
        public string id { get; set; }

        [JsonPropertyName("name")]
        public string name { get; set; }

        [JsonPropertyName("powerstats")]
        public Powerstats powerstats { get; set; }

    }