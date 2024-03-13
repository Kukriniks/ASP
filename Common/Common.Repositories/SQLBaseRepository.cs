
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

		TEntity IBaseRepository<TEntity>.Add(TEntity node)
		{
			var set = _applicationDBContext.Set<TEntity>();
			set.Add(node);
			_applicationDBContext.SaveChanges();
			return node;

		}

		int IBaseRepository<TEntity>.Count(Expression<Func<TEntity, bool>>? predicate)
		{
			var set = _applicationDBContext.Set<TEntity>();
			return predicate == null ? set.Count() : set.Count(predicate);
		}

		bool IBaseRepository<TEntity>.Delete(TEntity node)
		{
			var set = _applicationDBContext.Set<TEntity>();
			set.Remove(node);
			return _applicationDBContext.SaveChanges() > 0;
			
		}

		TEntity[] IBaseRepository<TEntity>.GetList(int? offset, int? limit, Expression<Func<TEntity, bool>>? predicate, Expression<Func<TEntity, object>>? orderBy, bool? descending)
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

			return querible.ToArray();
		}

		TEntity? IBaseRepository<TEntity>.SingleOrDefault(Expression<Func<TEntity, bool>>? predicate)
		{
			var set = _applicationDBContext.Set<TEntity>();
			return predicate == null ? set.SingleOrDefault() : set.SingleOrDefault(predicate);
		}

		TEntity IBaseRepository<TEntity>.Update(TEntity node)
		{
			var set = _applicationDBContext.Set<TEntity>();
			set.Update(node);
			_applicationDBContext.SaveChanges();
			return node;
		}
	}
}
