using Common.Domain;
namespace ToDo.BL
{
	public interface IToDoServices 
	{
		Task<IReadOnlyCollection<Common.Domain.ToDo>> GetListAsync(int? offset, string? nameFreeText, int? limit = 10, CancellationToken cancellation = default);
		Task<Common.Domain.ToDo> GetByIDAsync(int id, CancellationToken cancellation = default);
		Task<Common.Domain.ToDo> AddToDoAsync(CreateToDoDTO node, CancellationToken cancellation = default);
		Task<Common.Domain.ToDo> UpdateToDoAsync( UpdateToDoDTO node, CancellationToken cancellation = default);
		Task<Common.Domain.ToDo?> UpdateLabelAsync(UpdateToDoLabelDTO node, CancellationToken cancellation = default);
		Task<bool> DeleteToDoAsync(int id, CancellationToken cancellation = default);
		Task<bool> IsDoneAsync(int id, CancellationToken cancellation = default);
		Task<int> CountAsync(string? nameFreeText, CancellationToken cancellation = default);		
	}
}