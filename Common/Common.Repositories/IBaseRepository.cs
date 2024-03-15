﻿
using System.Linq.Expressions;

namespace Common.Repositories
{
	public interface IBaseRepository<TEntity> where TEntity : class, new()
	{
		Task<TEntity[]> GetAllAsync(				
				int? offset = null,
				int? limit = null,
				Expression<Func<TEntity, bool>>? predicate = null,
				Expression<Func<TEntity, object>>? orderBy = null,
				bool? descending = null, 
				CancellationToken cancellationToken = default);	
		
		Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);
		Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);
		Task<TEntity> AddAsync(TEntity node, CancellationToken cancellationToken = default);
		Task<TEntity> UpdateAsync(TEntity node, CancellationToken cancellationToken = default);
		Task<bool> DeleteAsync(TEntity node, CancellationToken cancellationToken = default);
	}
}
