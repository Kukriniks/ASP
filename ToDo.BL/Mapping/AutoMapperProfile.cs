using AutoMapper;
using Common.Domain;


namespace ToDo.BL.Mapping
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile() 
		{
			CreateMap<CreateToDoDTO, ToDoNode>();
			CreateMap<UpdateToDoDTO, ToDoNode>();
			CreateMap<UpdateToDoLabelDTO, ToDoNode>();
		}
	}
}
