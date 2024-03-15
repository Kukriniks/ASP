
using System.Linq.Expressions;

namespace Common.Repositories
{
	public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
	{
		private readonly List<TEntity> _data = [];

		public TEntity[] GetList(int? offset = null, int? limit = null, Expression<Func<TEntity, bool>>? predicate = null,
			Expression<Func<TEntity, object>>? orderBy = null,
			bool? descending = null)
		{
			IEnumerable<TEntity> result = _data;

			if (predicate != null)
			{
				result = result.Where(predicate.Compile());
			}

			if (orderBy != null)
			{
				result = descending.GetValueOrDefault()
					? result.OrderByDescending(orderBy.Compile())
					: result.OrderBy(orderBy.Compile());
			}

			result = result.Skip(offset.GetValueOrDefault());


			if (limit.HasValue)
			{
				result = result.Take(limit.Value);
			}

			return result.ToArray();
		}

		public TEntity? SingleOrDefault(Expression<Func<TEntity, bool>>? predicate = null)
		{
			if (predicate is null)
			{
				return _data.SingleOrDefault();
			}
			return _data.SingleOrDefault(predicate.Compile());
		}

		public int Count(Expression<Func<TEntity, bool>>? predicate = null)
		{
			IEnumerable<TEntity> entities = _data;
			if (predicate == null)
			{
				return entities.Count();
			}

			return entities.Where(predicate.Compile()).Count();
		}

		public TEntity Add(TEntity node)
		{
			_data.Add(node);
			return node;
		}

		public TEntity Update(TEntity node)
		{
			Delete(node);
			_data.Add(node);
			return node;
		}

		public bool Delete(TEntity node)
		{
			return _data.Remove(node);
		}

		public Task<TEntity[]> GetAllAsync(int? offset = null, int? limit = null, Expression<Func<TEntity, bool>>? predicate = null, Expression<Func<TEntity, object>>? orderBy = null, bool? descending = null, CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}

		public Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}

		public Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}

		public Task<TEntity> AddAsync(TEntity node, CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}

		public Task<TEntity> UpdateAsync(TEntity node, CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(TEntity node, CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}
	}
}
