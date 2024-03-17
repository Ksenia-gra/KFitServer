using KFitServer.BusinessLogic.Services;
using KFitServer.DBContext;
using KFitServer.DBContext.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KFitServer.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class KFitAuthorizationApiController : ControllerBase
	{
		private readonly ILogger<KFitApiNutritionController> logger;
		private readonly AuthentificationService authservice;

		public KFitAuthorizationApiController(ILogger<KFitApiNutritionController> logger, AuthentificationService authservice)
        {
			this.logger = logger;
			this.authservice = authservice;
		}

        [HttpPost("AuthorizeUserByToken")]
		public async Task<IActionResult> AuthorizeUserByToken([FromHeader] string token)
		{
			if (string.IsNullOrEmpty(token))
			{
				return BadRequest("Пустой токен");
			}
			try
			{
				User u = await authservice.AuthorizeByTokenAsync(token);
				logger.LogInformation($"User {u} has been authorize", null, null);
				return StatusCode(StatusCodes.Status200OK, "Пользователь авторизован");
			}
			catch (Exception ex)
			{
				logger.LogError(ex, null, null);
				return StatusCode(StatusCodes.Status502BadGateway, ex.Message);
			}
		}

		[HttpPost("AuthorizeUser")]
		public async Task<IActionResult> AuthorizeUser([FromQuery] string email,string password)
		{
			if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
			{
				return BadRequest("Пустое тело запроса");
			}
			try
			{
				User u = await authservice.AuthorizeAsync(email,password);
				logger.LogInformation($"User {u} has been authorize", null, null);
				return StatusCode(StatusCodes.Status200OK, "Пользователь авторизован");
			}
			catch (Exception ex)
			{
				logger.LogError(ex, null, null);
				return StatusCode(StatusCodes.Status502BadGateway, ex.Message);
			}
		}

		//TODO: Доделать токен
		[HttpPost("RegisterUser")]
		public async Task<IActionResult> RegisterUser([FromQuery] string login,string email, string password)
		{
			if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
			{
				return BadRequest("Пустое тело запроса");
			}
			try
			{
				User u = await authservice.RegisterAsync(login,email, password);
				logger.LogInformation($"User {u} has been registered", null, null);
				return StatusCode(StatusCodes.Status201Created, "Пользователь зарегистрирован");

			}
			catch (Exception ex)
			{
				logger.LogError(ex, null, null);
				return StatusCode(StatusCodes.Status502BadGateway, ex.Message);
			}
		}

	}
}
