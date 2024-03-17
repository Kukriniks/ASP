
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Common.Repositories
{
	public class SQLBaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
	{
		private readonly ApplicationDBContext _applicationDBContext;

		public SQLBaseRepository(ApplicationDBContext applicationDBContext) 
		{
			_applicationDBContext = applicationDBContext;
		}

		public async Task<TEntity> AddAsync(TEntity node, CancellationToken cancellationToken = default)
		{
			var set = _applicationDBContext.Set<TEntity>();
			  set.Add(node);
			await _applicationDBContext.SaveChangesAsync();
			return  node;
		}

		public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
		{
			var set = _applicationDBContext.Set<TEntity>();
			
			return  predicate == null ? await set.CountAsync(cancellationToken) : await set.CountAsync(predicate, cancellationToken);
		}

		public async Task<bool> DeleteAsync(TEntity node, CancellationToken cancellationToken = default)
		{
			var set = _applicationDBContext.Set<TEntity>();
			set.Remove(node);
			return await  _applicationDBContext.SaveChangesAsync() > 0;
		}

		public async Task<TEntity[]> GetAllAsync
			(int? offset = null, 
			int? limit = null,
			Expression<Func<TEntity,
			bool>>? predicate = null,
			Expression<Func<TEntity, object>>? orderBy = null, 
			bool? descending = null, 
			CancellationToken cancellationToken = default)

		{
			var querible = _applicationDBContext.Set<TEntity>().AsQueryable();
			if (predicate != null)
			{
				querible = querible.Where(predicate);
			}

			if (orderBy is not null)
			{
				querible = descending == true ? querible.OrderByDescending(orderBy) : querible.OrderBy(orderBy);
			}

			if (offset.HasValue)
			{
				querible = querible.Skip(offset.Value);
			}

			if (limit.HasValue)
			{
				querible = querible.Take(limit.Value);
			}

			return await querible.ToArrayAsync();
		}

		public TEntity? SingleOrDefault(Expression<Func<TEntity, bool>>? predicate)
		{
			var set = _applicationDBContext.Set<TEntity>();
			return predicate == null ? set.SingleOrDefault() : set.SingleOrDefault(predicate);
		}

		public async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
		{
			var set = _applicationDBContext.Set<TEntity>();
			return  predicate == null ? await  set.SingleOrDefaultAsync() : await  set.SingleOrDefaultAsync(predicate);
		}

		public async Task<TEntity> UpdateAsync(TEntity node, CancellationToken cancellationToken = default)
		{
			var set = _applicationDBContext.Set<TEntity>();			
			set.Update(node);
			await _applicationDBContext.SaveChangesAsync(cancellationToken);
			return node;
		}


	}
}
