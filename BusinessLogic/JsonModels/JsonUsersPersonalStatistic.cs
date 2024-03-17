using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace KFitServer.JsonModels
{
    public class JsonUsersPersonalStatistic
    {
        public DateOnly? RecordDate { get; set; }

        public decimal? UserWeight { get; set; }

        public decimal? UserChestGirth { get; set; }

        public decimal? UserHipGirth { get; set; }

        public decimal? UserWaistCircumference { get; set; }

        public int? CaloriesNorm { get; set; }

        public int? Proteins { get; set; }

        public int? Lipids { get; set; }

        public int? Carbohydrates { get; set; }

        public decimal? WaterNorm { get; set; }
    }
}
