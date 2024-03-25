using AutoMapper;
using Users.BL.UserDTO;


namespace User.BL.Mapping
{
	public class UserAutoMapperProfile : Profile
	{
		public UserAutoMapperProfile() 
		{
			CreateMap<CreateUserDTO, Common.Domain.User>();
			CreateMap<GetUserDTO, Common.Domain.User>();		
		}
	}
}
