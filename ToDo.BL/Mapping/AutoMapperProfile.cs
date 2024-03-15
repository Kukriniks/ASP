using AutoMapper;

namespace ToDo.BL.Mapping
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile() 
		{
			CreateMap<CreateToDoDTO, Common.Domain.ToDo>();
			CreateMap<UpdateToDoDTO, Common.Domain.ToDo>();
			CreateMap<UpdateToDoLabelDTO, Common.Domain.ToDo>();
		}
	}
}
