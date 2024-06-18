// // https://superheroapi.com/
// // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
using System.Text.Json.Serialization;

//     public class EstadisticasPj
//     {
//         [JsonPropertyName("intelligence")]
//         public string Inteligencia { get; set; }

//         [JsonPropertyName("strength")]
//         public string Fuerza { get; set; }

//         [JsonPropertyName("speed")]
//         public string Velocidad { get; set; }

//         [JsonPropertyName("durability")]
//         public string Durabilidad { get; set; }

//         [JsonPropertyName("power")]
//         public string Poder { get; set; }

//         [JsonPropertyName("combat")]
//         public string Combate { get; set; }
//     }

//     public class InfoPersonaje
//     {
//         [JsonPropertyName("response")]
//         public string response { get; set; }

//         [JsonPropertyName("id")]
//         public string id { get; set; }

//         [JsonPropertyName("name")]
//         public string Nombre { get; set; }

//         [JsonPropertyName("powerstats")]
//         public EstadisticasPj Estadisticas { get; set; }

//     }


// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class Appearance
    {
        [JsonPropertyName("gender")]
        public string gender { get; set; }

        [JsonPropertyName("race")]
        public string race { get; set; }

        [JsonPropertyName("height")]
        public List<string> height { get; set; }

        [JsonPropertyName("weight")]
        public List<string> weight { get; set; }

        [JsonPropertyName("eye-color")]
        public string eyecolor { get; set; }

        [JsonPropertyName("hair-color")]
        public string haircolor { get; set; }
    }

    public class Biography
    {
        [JsonPropertyName("full-name")]
        public string fullname { get; set; }

        [JsonPropertyName("alter-egos")]
        public string alteregos { get; set; }

        [JsonPropertyName("aliases")]
        public List<string> aliases { get; set; }

        [JsonPropertyName("place-of-birth")]
        public string placeofbirth { get; set; }

        [JsonPropertyName("first-appearance")]
        public string firstappearance { get; set; }

        [JsonPropertyName("publisher")]
        public string publisher { get; set; }

        [JsonPropertyName("alignment")]
        public string alignment { get; set; }
    }

    public class Connections
    {
        [JsonPropertyName("group-affiliation")]
        public string groupaffiliation { get; set; }

        [JsonPropertyName("relatives")]
        public string relatives { get; set; }
    }

    public class Image
    {
        [JsonPropertyName("url")]
        public string url { get; set; }
    }

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

        [JsonPropertyName("biography")]
        public Biography biography { get; set; }

        [JsonPropertyName("appearance")]
        public Appearance appearance { get; set; }

        [JsonPropertyName("work")]
        public Work work { get; set; }

        [JsonPropertyName("connections")]
        public Connections connections { get; set; }

        [JsonPropertyName("image")]
        public Image image { get; set; }
    }

    public class Work
    {
        [JsonPropertyName("occupation")]
        public string occupation { get; set; }

        [JsonPropertyName("base")]
        public string @base { get; set; }
    }


