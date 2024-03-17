using KFitServer.DBContext.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace KFitServer.JsonModels
{
    public class JsonUser
    {
        public string UserGender { get; set; }

        public DateOnly? UserDateOfBirth { get; set; }

        public decimal? UserHeight { get; set; }

        public string UserTarget { get; set; }

        public decimal? UserTargetWeight { get; set; }

    }
}
