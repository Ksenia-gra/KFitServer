using KFitServer.BusinessLogic.DBContext.Models;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KFitServer.BusinessLogic.Helpers
{
	public class JwtGenerator : IJwtGenerator
	{
		private readonly SymmetricSecurityKey _key;

		public JwtGenerator(IConfiguration config)
		{
			_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("FFF1DA25-F6B0-4754-877F-7B6CC77B20D2"));
		}

		public string CreateToken(User user)
		{
			List<Claim> claims = new List<Claim> { new Claim(JwtRegisteredClaimNames.Email, user.Email) };
			SigningCredentials credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
			SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(7),
				SigningCredentials = credentials
			};

			JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
			SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}
