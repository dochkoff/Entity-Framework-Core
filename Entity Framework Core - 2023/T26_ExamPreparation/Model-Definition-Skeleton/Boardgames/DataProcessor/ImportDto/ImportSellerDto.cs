using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Boardgames.DataProcessor.ImportDto
{
    public class ImportSellerDto
    {
        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        [JsonProperty("Name")]
        public string Name { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        [JsonProperty("Address")]
        public string Address { get; set; }

        [Required]
        [JsonProperty("Country")]
        public string Country { get; set; }

        [Required]
        [RegularExpression(@"^www\.[a-zA-Z0-9-]+\.com")]
        [JsonProperty("Website")]
        public string Website { get; set; }

        [Required]
        [JsonProperty("Boardgames")]
        public int[] BoardgamesIds { get; set; }
    }
}

//•	Id – integer, Primary Key
//•	Name – text with length [5…20] (required)
//•	Address – text with length [2…30] (required)
//•	Country – text (required)
//•	Website – a string (required). First four characters are "www.", followed by upper and lower letters, digits or '-' and the last three characters are ".com".
//•	BoardgamesSellers – collection of type BoardgameSeller