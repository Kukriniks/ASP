using Users.BL.UserDTO;

namespace Users.BL
{
	public interface IAuthService
	{
		Task<string> GetJwtTokenAsync(AuthDTO authDTO, CancellationToken cancellationToken);
		
	}
}