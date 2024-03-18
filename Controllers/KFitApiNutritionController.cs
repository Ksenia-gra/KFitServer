using KFitServer.BusinessLogic.DBContext;
using KFitServer.BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KFitServer.Controllers
{
	[ApiController]
    [Route("api/[controller]")]
	[Authorize]
    public class KFitApiNutritionController : ControllerBase
    {
        private Dictionary<string, string> daysDict = new Dictionary<string, string>
        {
            { "Mon","понедельник" },
            {"Tue","вторник" },
            {"Wed","среда" },
            {"Thu","четверг" },
            {"Fri" ,"пятница"},
            {"Sat" ,"суббота"},
            {"Sun" ,"воскресенье"},
            {"пн","понедельник" },
            {"вт","вторник" },
            {"ср","среда" },
            {"чт","четверг" },
            {"пт" ,"пятница"},
            {"сб" ,"суббота"},
            {"вс" ,"воскресенье"}
        };

        private readonly ILogger<KFitApiNutritionController> _logger;
        private readonly KfitContext dbContext;
		private readonly NutritionService statisticService;

		public KFitApiNutritionController(ILogger<KFitApiNutritionController> logger, KfitContext kfitContext,NutritionService statisticService)
        {
            _logger = logger;
            dbContext = kfitContext;
			this.statisticService = statisticService;
		}

        [HttpGet("GetProductInfoFromApi")]
        public async Task<IActionResult> GetProductInfoFromApi([FromQuery] string productName)
        {/*
            if (string.IsNullOrEmpty(productName))
            {
                return BadRequest();
            }
            productName = await TranslateService.TranslateText(productName,"ru","en");
            ResponseProducts productsFromApi = null;

            using (WebClient webClient = new WebClient())
            {
                try
                {
                    string query = productsAPIProductInfoURL + $"app_id={appIdForProductsAPI}&app_key={productsAPIKeyValue}&ingr={productName}&nutrition-type=cooking";
                    string json = await webClient.DownloadStringTaskAsync(query);
                    productsFromApi = JsonConvert.DeserializeObject<ResponseProducts>(json);
                }
                catch(Exception ex) 
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }

            if (productsFromApi is null)
            {
                return StatusCode(StatusCodes.Status502BadGateway);
            }

            if (productsFromApi.Hints is null)
            {
                return NotFound();
            }
            Food firstProduct = productsFromApi.Hints.FirstOrDefault().Food;
            Nutrients parsedNutrients = new Nutrients((int)firstProduct.Nutrients.ENERC_KCAL, (int)firstProduct.Nutrients.PROCNT,
                (int)firstProduct.Nutrients.FAT, (int)firstProduct.Nutrients.CHOCDF);
            string translatedName = await TranslateService.TranslateText(productName,"en","ru");
            JsonProduct resultProduct = new JsonProduct(firstProduct.FoodId,translatedName,(int)parsedNutrients.ENERC_KCAL,
                (int)parsedNutrients.CHOCDF,(int)parsedNutrients.PROCNT,(int)parsedNutrients.FAT);
            string resultJson = JsonConvert.SerializeObject(resultProduct);
			*/
            return Ok();
        }

        [HttpGet("ProductsSearching")]
        public async Task<IActionResult> SearchingProducts([FromQuery] string keyword)
        {
            /*if (string.IsNullOrEmpty(keyword))
            {
                return BadRequest();
            }

            keyword = await TranslateService.TranslateText(keyword,"ru","en");
            string query = productsAPISearchingURL+$"app_id={appIdForProductsAPI}&app_key={productsAPIKeyValue}&q={keyword}";
            List<string> resultTranslated = new List<string>();

            using (WebClient webClient = new WebClient())
            {
                try
                {
                    string json = await webClient.DownloadStringTaskAsync(query);
                    List<string> result = JsonConvert.DeserializeObject<List<string>>(json);
                    foreach(string item in result)
                    {
                        resultTranslated.Add(await TranslateService.TranslateText(item,"en","ru"));
                    }

                }
                catch(Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }

            if (!resultTranslated.Any())
            {
                return NotFound();
            }*/

            return Ok();
        }

		/*[HttpPost("AddUserNutritionStatistic")]
		public async Task<IActionResult> AddUserNutritionStatistic([FromHeader] string token, [FromBody] List<UsersNutritionStatistic> nutritionStatistics)
		{
			try
			{
				if(!await statisticService.AddNutritionStatisticAsync(token, nutritionStatistics))
				{
					return BadRequest("Не удалось добавить данные статистики");
				}
				return Ok("Данные статистики питания успешно добавлены");
			}
			catch (Exception ex) 
			{
				_logger.LogError(ex, null, null);
				return BadRequest(ex.Message);
			}
			
		}*/

		/*        [HttpGet("GetUserTrainingStatistic")]
				public async Task<IActionResult> GetUserTrainingStatistic([FromHeader] string token)
				{
					if (string.IsNullOrEmpty(token))
					{
						return BadRequest();
					}

					UsersPersonalParameter user = await dbContext.UsersPersonalParameters.FirstOrDefaultAsync(x => x.UserToken.Equals(token));
					if (user == null)
					{
						return BadRequest("Неверный токен аутентификации");
					}
					IEnumerable<UserTrainingStatistic> userTrainStatistic = await dbContext.UserTrainingStatistics
						.Where(x => x.UserId.Equals(user.Id))
						.ToListAsync();

					if (userTrainStatistic == null)
					{
						return NoContent();
					}

					List<JsonTrainingStatistic> result = new List<JsonTrainingStatistic>();

					try
					{
						foreach(UserTrainingStatistic item in userTrainStatistic)
						{
							JsonTrainingStatistic jsonTrainingStatisticItem = new JsonTrainingStatistic(item.TrainingDate,item.TrainingId);
							result.Add(jsonTrainingStatisticItem);
						}
					}
					catch (Exception ex)
					{
						return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
					}
					return Ok(result);
				}

				[HttpGet("GetUserNutritionStatistic")]
				public async Task<IActionResult> GetUserNutritionStatistic([FromHeader] string token, [FromQuery] string date)
				{
					if (string.IsNullOrEmpty(token))
					{
						return BadRequest();
					}

					UsersPersonalParameter user = await dbContext.UsersPersonalParameters.FirstOrDefaultAsync(x => x.UserToken.Equals(token));
					if (user == null)
					{
						return BadRequest("Неверный токен аутентификации");
					}

					if(date == null)
					{
						return BadRequest("Пустая дата записи");
					}

					IEnumerable<UsersNutritionStatistic> userNutrStatistic = await dbContext.UsersNutritionStatistics
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
							DateOnly parcedDate = DateOnly.Parse(date);
							if(userStatistic.NutritionDate  == parcedDate)
							{
								string typeOfMealName = (await dbContext.TypeOfMeals.FirstOrDefaultAsync(x => x.Id.Equals(userStatistic.TypeOfMealId))).TypeName;
								JsonNutritionStatistic jsonNutritionStatistic =
									new JsonNutritionStatistic(userStatistic.NutritionDate, userStatistic.ProductId,
									typeOfMealName, userStatistic.CaloriesCount, userStatistic.Water,userStatistic.ProductName,userStatistic.Proteins,
									userStatistic.Lipids,userStatistic.Carbohydrates);
								jsonNutritionStatisticsList.Add(jsonNutritionStatistic);
							}
						}

					}
					catch (Exception ex)
					{
						return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка на стороне сервера");
					}
					//string resultJson = JsonConvert.SerializeObject(jsonNutritionStatisticsList);
					return Ok(jsonNutritionStatisticsList);

				}

				[HttpGet("GetTrainingsFromAPI")]
				public async Task<IActionResult> GetTrainingsFromAPI([FromQuery] string playListId)
				{
					YPlaylist yPlaylist = await YoutubeServices.GetVideoOnPlaylistAsync(playListId);
					if(yPlaylist is null)
					{
						StatusCode(StatusCodes.Status500InternalServerError,"Не удалось подключится");
					}

					*//*Random random = new Random();

					foreach (TrainingMetadatum youtubeVideo in dbContext.TrainingMetadata.ToList())
					{
						for(int i = 0;i<3; i++)
						{
							int index =  random.Next(1,dbContext.YoutubeVideos.Count());
							youtubeVideo.Videos.Add(dbContext.YoutubeVideos.ToArray()[index]);
						}
					}*/
		/*foreach (Item youtubeVideo in yPlaylist.Items)
		{
			dbContext.YoutubeVideos.Add(new YoutubeVideo
			{
				Id = youtubeVideo.Id,
				ThumbnailUrl = youtubeVideo.Snippet.Thumbnails.Standard.Url,
				Title = youtubeVideo.Snippet.Title,

			});
		}*//*
		//await dbContext.SaveChangesAsync();
		return Ok(yPlaylist);
	}

	[HttpGet("GetTraining")]
	public async Task<IActionResult> GetTrainings([FromQuery] string dayOfWeekCode)
	{

		//TrainChallange trainChallange = await dbContext.TrainChallanges.Include(x=>x.TrainingMetadata).FirstOrDefaultAsync(x=>x.Id.Equals(challengeId));
		if (dbContext.YoutubeVideos.Count()== 0)
		{
			return BadRequest("Неверный идентификатор челленджа");
		}
		if (string.IsNullOrEmpty(dayOfWeekCode))
		{
			return BadRequest("Не заполнен день недели");
		}
		string day = "";
		try
		{
			day = daysDict[dayOfWeekCode];
		}
		catch
		{
			return BadRequest("Неправильный код дня");
		}

		List<JsonVideo> jsonTraining = new List<JsonVideo>();
		foreach(YoutubeVideo training in await dbContext.YoutubeVideos.ToListAsync())
		{
			string weekDayName = (await dbContext.WeekDays.FirstOrDefaultAsync(x => x.Id.Equals(training.WeekDay))).Name;
			if(day == weekDayName)
			{
				jsonTraining.Add(new JsonVideo(training.Id, training.Title, training.ThumbnailUrl, weekDayName));
			}

		}

		return Ok(jsonTraining);
	}*/
		/*
				[HttpGet("GetUserPersonalParameters")]
				public async Task<IActionResult> GetUserPersonalParameters([FromHeader] string token)
				{
					if (string.IsNullOrEmpty(token))
					{
						return BadRequest();
					}

					UsersPersonalParameter user = await dbContext.UsersPersonalParameters.FirstOrDefaultAsync(x => x.UserToken.Equals(token));
					if (user == null)
					{
						return BadRequest("Неверный токен аутентификации");
					}
					string targetName = (await dbContext.Targets.FirstOrDefaultAsync(x => x.Id.Equals(user.UserTarget))).TargetName;
					JsonUser jsonUser = new JsonUser
					{
						UserGender = user.UserGender,
						UserDateOfBirth = user.UserDateOfBirth,
						UserTarget = targetName,
						UserHeight = user.UserHeight,
						UserTargetWeight = user.UserTargetWeight
					};
					//string resultJson = JsonConvert.SerializeObject();
					return Ok(jsonUser);
				}

				[HttpGet("GetUserPersonalStatistic")] 
				public async Task<IActionResult> GetUserPersonalStatistic([FromHeader] string token)
				{
					if (string.IsNullOrEmpty(token)) 
					{ 
						return BadRequest("Пустой токен");
					}
					UsersPersonalParameter user = await dbContext.UsersPersonalParameters.FirstOrDefaultAsync(x => x.UserToken.Equals(token));
					if (user == null)
					{
						return BadRequest("Неверный токен аутентификации");
					}

					List<UserPersonalStatistic> userPersonalStatistic = await dbContext.UserPersonalStatistics
						.Where(x => x.UserId.Equals(user.Id))
						.OrderBy(x=>x.RecordDate).ToListAsync();

					try
					{
						if (userPersonalStatistic is null) return NoContent();
						List<JsonUsersPersonalStatistic> statistic = new List<JsonUsersPersonalStatistic>();
						foreach(UserPersonalStatistic item in userPersonalStatistic)
						{
							JsonUsersPersonalStatistic jsonUsersPersonalStatisticLast = new JsonUsersPersonalStatistic
							{
								RecordDate = item.RecordDate,
								CaloriesNorm = item.CaloriesNorm,
								Proteins = item.Proteins,
								Lipids = item.Lipids,
								Carbohydrates = item.Carbohydrates,
								UserChestGirth = item.UserChestGirth,
								UserHipGirth = item.UserHipGirth,
								UserWaistCircumference = item.UserWaistCircumference,
								UserWeight = item.UserWeight,
								WaterNorm = item.WaterNorm

							};
							statistic.Add(jsonUsersPersonalStatisticLast);
						}


						return Ok(statistic);
					}
					catch(Exception ex)
					{
						return BadRequest(ex.Message);
					}



				}



				[HttpPost("AddUserPersonalStatistic")]
				public async Task<IActionResult> AddUserPersonalStatistic([FromHeader] string token, [FromBody] List<JsonUsersPersonalStatistic> statistic)
				{
					if (string.IsNullOrEmpty(token))
					{
						return BadRequest("Пустой токен");
					}
					UsersParameter user = await dbContext.UsersParameters.FirstOrDefaultAsync(x => x.UserToken.Equals(token));
					if (user == null)
					{
						return BadRequest("Неверный токен аутентификации");
					}

					if(statistic is null)
					{
						return BadRequest("Данные о статистике не заполнены");
					}

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

						NutritionCounter personalCounter = new NutritionCounter(user, userPersonalStatistic);
						userPersonalStatistic.CaloriesNorm = personalCounter.Calories;
						userPersonalStatistic.Carbohydrates = personalCounter.Carbohydrates;
						userPersonalStatistic.Lipids = personalCounter.Carbohydrates;
						userPersonalStatistic.Proteins = personalCounter.Proteins;

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

				[HttpPost("AddUserTrainingStatistic")]
				public async Task<IActionResult> AddUserTrainingStatistic([FromHeader] string token, [FromBody] List<JsonTrainingStatistic> statistic)
				{
					if(token == null)
					{
						return BadRequest("Пустой токен авторизации");
					}
					if (statistic == null)
					{
						return BadRequest("Нет данных для добавления");
					}

					UsersPersonalParameter user = await dbContext.UsersPersonalParameters.FirstOrDefaultAsync(x => x.UserToken.Equals(token));
					if (user == null)
					{
						return BadRequest("Неверный токен аутентификации");
					}


					foreach (JsonTrainingStatistic juserPersonalStatistic in statistic)
					{
						UserTrainingStatistic userTrainingStatistic = new UserTrainingStatistic
						{
							UserId = user.Id,
							TrainingDate = juserPersonalStatistic.TrainingDate,
							TrainingId = juserPersonalStatistic.TrainingId,
						};
						await dbContext.UserTrainingStatistics.AddAsync(userTrainingStatistic);
					}
					try
					{
						await dbContext.SaveChangesAsync();
					}
					catch (Exception ex)
					{
						return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
					}

					return Ok("Данные статистики тренировок пользователя успешно добавлены");
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

						UsersPersonalParameter newUser = await dbContext.UsersPersonalParameters.FirstOrDefaultAsync(x => x.UserToken.Equals(token));
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
							newUser.UserGender = (await dbContext.Genders.FirstOrDefaultAsync(x => x.GenderName == userFromBody.UserGender)).Id;
						}

						if (!string.IsNullOrEmpty(userFromBody.UserTarget) &&
							await dbContext.Targets.AnyAsync(x => x.TargetName.Equals(userFromBody.UserTarget)))
						{
							newUser.UserTarget = (await dbContext.Targets.FirstOrDefaultAsync(x => x.TargetName == userFromBody.UserTarget)).Id;
						}

						if (userFromBody.UserHeight>100)
						{
							newUser.UserHeight = userFromBody.UserHeight;
						}

						if (userFromBody.UserDateOfBirth < DateOnly.FromDateTime(DateTime.Now))
						{
							newUser.UserDateOfBirth = userFromBody.UserDateOfBirth;
						}

						if (userFromBody.UserTargetWeight > 30)
						{
							newUser.UserTargetWeight = userFromBody.UserTargetWeight;
						}

						//dbContext.UsersPersonalParameters.Add(newUser);
						await dbContext.SaveChangesAsync();
						return Ok();
					}
				}

				*/

	}

}