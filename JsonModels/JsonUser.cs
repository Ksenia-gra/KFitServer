using KFitServer.DBContext.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace KFitServer.JsonModels
{
    public class JsonUser
    {
        [JsonProperty(PropertyName = "gender")]
        public string? UserGender { get; set; }

        [JsonProperty(PropertyName = "dateOfBirth")]
        public DateOnly? UserDateOfBirth { get; set; }

        [JsonProperty(PropertyName = "height")]
        public decimal? UserHeight { get; set; }

        [JsonProperty(PropertyName = "target")]
        public string? UserTarget { get; set; }

    }
}
