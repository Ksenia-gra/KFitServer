using KFitServer.DBContext;
using KFitServer.DBContext.Models;
using KFitServer.JsonModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;

namespace KFitServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KFitApiController : ControllerBase
    {
        private const string productsAPI = "https://api.calorieninjas.com/v1/nutrition?query=";
        private const string youTubeAPI = "";
        private const string productsAPIKeyValue = "TcBIT+XGgZpJJbFxu0iROQ==KOeOlJQtCRNeFlvV";

        private readonly ILogger<KFitApiController> _logger;
        private readonly KfitContext dbContext;

        public KFitApiController(ILogger<KFitApiController> logger, KfitContext kfitContext)
        {
            _logger = logger;
            dbContext = kfitContext;
        }

        /*[HttpGet("GetProductsFromDb")]
        public IEnumerable<Product> GetProductsFromDb()
        {
            return dbContext.Products.ToArray();
        }

        [HttpGet("GetTrainingsFromDb")]
        public IEnumerable<TrainingMetadatum> GetTrainingsFromDb()
        {
            return dbContext.TrainingMetadata.ToArray();
        }*/

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProductsFromApi([FromQuery] string productName)
        {
            if (string.IsNullOrEmpty(productName))
            {
                return BadRequest();
            }

            ResponseProducts productsFromApi = null;

            using (WebClient webClient = new WebClient())
            {
                try
                {
                    webClient.Headers.Add("X-Api-Key", productsAPIKeyValue);
                    string json = await webClient.DownloadStringTaskAsync(productsAPI + productName);
                    productsFromApi = JsonConvert.DeserializeObject<ResponseProducts>(json);
                }
                catch
                {
                }
            }

            if (productsFromApi == null)
            {
                return StatusCode(StatusCodes.Status502BadGateway);
            }

            if (!productsFromApi.Items.Any())
            {
                return NotFound();
            }


            string resultJson = JsonConvert.SerializeObject(productsFromApi.Items);
            return Ok(resultJson);
        }

        [HttpGet("GetUserNutritionStatistic")]
        public async Task<IActionResult> GetUserNutritionStatistic([FromHeader] string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }

            UsersPersonalParameter? user = await dbContext.UsersPersonalParameters.FirstOrDefaultAsync(x => x.UserToken.Equals(token));
            if (user == null)
            {
                return BadRequest("Неверный токен аутентификации");
            }

            IEnumerable<UsersNutritionStatistic>? userNutrStatistic = await dbContext.UsersNutritionStatistics
                .Include(x => x.Product)
                .Where(x => x.UserId.Equals(user.Id))
                .ToListAsync();

            if (userNutrStatistic == null)
            {
                return NoContent();
            }

            List<JsonNutritionStatistic> jsonNutritionStatisticsList = new List<JsonNutritionStatistic>();

            try
            {
                foreach (UsersNutritionStatistic userStatistic in userNutrStatistic)
                {
                    JsonNutritionStatistic jsonNutritionStatistic = new JsonNutritionStatistic
                    {
                        NutritionDate = userStatistic.NutritionDate,
                        ProductName = userStatistic.Product.ProductName,
                        TypeOfMeal = (await dbContext.TypeOfMeals.FirstOrDefaultAsync(x => x.Id.Equals(userStatistic.TypeOfMealId)))?.TypeName
                    };
                    jsonNutritionStatisticsList.Add(jsonNutritionStatistic);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка на стороне сервера");
            }
            string resultJson = JsonConvert.SerializeObject(jsonNutritionStatisticsList);
            return Ok(resultJson);

        }

        [HttpGet("GetTrainingsFromAPI")]
        public async Task<IActionResult> GetTrainingsFromAPI()
        {
            YPlaylist yPlaylist = await GetVideoOnPlaylistAsync();
            if(yPlaylist is null)
            {
                StatusCode(StatusCodes.Status500InternalServerError,"Не удалось подключится");
            }
            return Ok(yPlaylist);
        }

        /*[HttpGet("GetUserTrainingStatistic")]
        public async Task<IActionResult> GetUserTrainingStatistic([FromHeader] string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }

            UsersPersonalParameter jsonUser = await dbContext.UsersPersonalParameters.FirstOrDefaultAsync(x => x.UserToken.Equals(token));
            string resultJson = JsonConvert.SerializeObject(jsonUser);
            return Ok(resultJson);
        }*/

        [HttpGet("GetUserPersonalParameters")]
        public async Task<IActionResult> GetUserPersonalParameters([FromHeader] string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }

            UsersPersonalParameter? user = await dbContext.UsersPersonalParameters.FirstOrDefaultAsync(x => x.UserToken.Equals(token));
            if (user == null)
            {
                return BadRequest("Неверный токен аутентификации");
            }
            string? targetName = (await dbContext.Targets.FirstOrDefaultAsync(x => x.Id.Equals(user.UserTarget)))?.TargetName;
            JsonUser jsonUser = new JsonUser
            {
                UserGender = user.UserGender,
                UserDateOfBirth = user.UserDateOfBirth,
                UserTarget = targetName,
                UserHeight = user.UserHeight
            };
            string resultJson = JsonConvert.SerializeObject(jsonUser);
            return Ok(resultJson);
        }

        [HttpGet("GetUserPersonalStatistic")] 
        public async Task<IActionResult> GetUserPersonalStatistic([FromHeader] string token)
        {
            if (string.IsNullOrEmpty(token)) 
            { 
                return BadRequest("Пустой токен");
            }
            UsersPersonalParameter? user = await dbContext.UsersPersonalParameters.FirstOrDefaultAsync(x => x.UserToken.Equals(token));
            if (user == null)
            {
                return BadRequest("Неверный токен аутентификации");
            }

            IEnumerable<UserPersonalStatistic>? userPersonalStatistic = await dbContext.UserPersonalStatistics
                .Where(x => x.UserId.Equals(user.Id))
                .ToListAsync();


            if (userPersonalStatistic is null || userPersonalStatistic.Count()==0)
            {
                return NoContent();
            }
            string resultJson = JsonConvert.SerializeObject(userPersonalStatistic);
            return Ok(resultJson);
        }

        [HttpPost("AuthentificateUser")]
        public async Task<IActionResult> AuthentificateUser([FromHeader]string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Пустой токен");
            }
            try
            {
                if (await dbContext.UsersPersonalParameters.AnyAsync(u => u.UserToken.Equals(token)))
                {
                    return StatusCode(StatusCodes.Status200OK, "Пользователь авторизован");
                }
                UsersPersonalParameter newUser = new UsersPersonalParameter
                {
                    UserToken = token
                };
                dbContext.UsersPersonalParameters.Add(newUser);
                await dbContext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, "Пользователь зарегистрирован");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, ex.Message);
            }

        }

        [HttpPost("AddUserNutritionStatistic")]
        public async Task<IActionResult> AddUserNutritionStatistic([FromHeader]string token,[FromBody] List<JsonNutritionStatistic> jsonNutritionStatistics)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Пустой токен аутентификации");
            }

            if (jsonNutritionStatistics is null)
            {
                return BadRequest("Пустой запрос");
            }

            IEnumerable<UsersNutritionStatistic>? userNutrStatistics = await dbContext.UsersNutritionStatistics
                .ToListAsync();
            foreach (JsonNutritionStatistic jsonNutrStatistic in jsonNutritionStatistics)
            {
                UsersNutritionStatistic? userNutrStatistic = new UsersNutritionStatistic
                {
                    NutritionDate = jsonNutrStatistic.NutritionDate,
                    UserId = (await dbContext.UsersPersonalParameters.FirstOrDefaultAsync(x => x.UserToken.Equals(token)))?.Id,
                    ProductId = (await dbContext.Products.FirstOrDefaultAsync(x => x.ProductName.Equals(jsonNutrStatistic.ProductName)))?.Id,
                    TypeOfMealId = (await dbContext.TypeOfMeals.FirstOrDefaultAsync(x => x.TypeName.Equals(jsonNutrStatistic.TypeOfMeal)))?.Id
                };
                await dbContext.UsersNutritionStatistics.AddAsync(userNutrStatistic);
            }
            

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return Ok("Данные статистики питания успешно добавлены");
        }

        [HttpPost("AddUserPersonalStatistic")]
        public async Task<IActionResult> AddUserPersonalStatistic([FromHeader] string token, [FromBody] List<JsonUsersPersonalStatistic> statistic)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Пустой токен");
            }
            UsersPersonalParameter? user = await dbContext.UsersPersonalParameters.FirstOrDefaultAsync(x => x.UserToken.Equals(token));
            if (user == null)
            {
                return BadRequest("Неверный токен аутентификации");
            }

            if(statistic is null)
            {
                return BadRequest("Данные о статистике не заполнены");
            }

            IEnumerable<UserPersonalStatistic>? userPersonalStatistics = await dbContext.UserPersonalStatistics
                .ToListAsync();

            foreach (JsonUsersPersonalStatistic juserPersonalStatistic in statistic)
            {
                UserPersonalStatistic userPersonalStatistic = new UserPersonalStatistic
                {
                    UserId = user.Id,
                    RecordDate = juserPersonalStatistic.RecordDate,
                    UserChestGirth = juserPersonalStatistic.UserChestGirth,
                    UserHipGirth = juserPersonalStatistic.UserHipGirth,
                    UserWaistCircumference = juserPersonalStatistic.UserWaistCircumference,
                    UserWeight = juserPersonalStatistic.UserWeight
                };
                await dbContext.UserPersonalStatistics.AddAsync(userPersonalStatistic);
            }
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return Ok("Данные статистики замеров пользователя успешно добавлены");
        }

        [HttpPatch("ChangeUserParameters")]
        public async Task<IActionResult> ChangeUserParameters([FromHeader] string token, [FromBody] JsonUser userFromBody)
        {
            using (WebClient webClient = new WebClient())
            {

                if (string.IsNullOrEmpty(token))
                {
                    return BadRequest("Пустой токен аутентификации");
                }

                UsersPersonalParameter? newUser = await dbContext.UsersPersonalParameters.FirstOrDefaultAsync(x => x.UserToken.Equals(token));
                if (newUser is null)
                {
                    return BadRequest("Неверный токен аутентификации");
                }

                if (userFromBody is null)
                {
                    return BadRequest("Пустые данные пользователя");
                }

                if (!string.IsNullOrEmpty(userFromBody.UserGender) &&
                    await dbContext.Genders.AnyAsync(x => x.GenderName.Equals(userFromBody.UserGender)))
                {
                    newUser.UserGender = (await dbContext.Genders.FirstOrDefaultAsync(x => x.GenderName == userFromBody.UserGender))?.Id;
                }

                if (!string.IsNullOrEmpty(userFromBody.UserTarget) &&
                    await dbContext.Targets.AnyAsync(x => x.TargetName.Equals(userFromBody.UserTarget)))
                {
                    newUser.UserTarget = (await dbContext.Targets.FirstOrDefaultAsync(x => x.TargetName == userFromBody.UserTarget))?.Id;
                }

                if (userFromBody.UserHeight is not null)
                {
                    newUser.UserHeight = userFromBody.UserHeight;
                }

                if (userFromBody.UserDateOfBirth is not null)
                {
                    newUser.UserDateOfBirth = userFromBody.UserDateOfBirth;
                }

                //dbContext.UsersPersonalParameters.Add(newUser);
                await dbContext.SaveChangesAsync();
                return Ok();
            }
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromHeader] string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Пустой токен аутентификации");
            }

            UsersPersonalParameter? userToDelete = await dbContext.UsersPersonalParameters.FirstOrDefaultAsync(x=>x.UserToken.Equals(token));
            if (userToDelete is null)
            {
                return BadRequest("Неверный токен аутентификации");
            }

            try
            {
                dbContext.UsersPersonalParameters.Remove(userToDelete);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
            return Ok("Пользователь удален");
        }

        private static async Task<YPlaylist> GetVideoOnPlaylistAsync()
        {
            var parameter = new Dictionary<string, string>()
            {
                ["key"] = "AIzaSyB6fEqlel3KUkVJHTsi9hYrjAxVycgHUEA",
                ["playlistId"] = "PLAFs3kxY4h1-LHUzBN2XhtJ1IutUxAB43",
                ["part"] = "snippet",
                /*["fields"] = "items/snippet(title,description,thumbnails/standard/url)"*/
                
            };

            string baseUrl = "https://www.googleapis.com/youtube/v3/playlistItems?";
            string fullUrl = MakeUrlFromQuery(baseUrl, parameter);

            var result = await new HttpClient().GetStringAsync(fullUrl);

            if (result != null)
            {
                return JsonConvert.DeserializeObject<YPlaylist>(result);
            }

            return null;
        }

        private static string MakeUrlFromQuery(string baseUrl, Dictionary<string, string> parameter)
        {
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new ArgumentNullException(nameof(baseUrl));
            }

            if (parameter == null || parameter.Count == 0)
            {
                return baseUrl;
            }

            return parameter.Aggregate(baseUrl,
                (accumulated, kvp) => string.Format($"{accumulated}{kvp.Key}={kvp.Value}&"));
        }
    }

}