using AutoMapper;


namespace ToDo.BL.Mapping
{
	public class ToDoAutoMapperProfile : Profile
	{
		public ToDoAutoMapperProfile() 
		{
			CreateMap<CreateToDoDTO, Common.Domain.ToDo>();
			CreateMap<UpdateToDoDTO, Common.Domain.ToDo>();
			CreateMap<UpdateToDoLabelDTO, Common.Domain.ToDo>();
		}
	}
}
