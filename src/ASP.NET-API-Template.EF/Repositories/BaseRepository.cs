using Mapster;
using Microsoft.Extensions.Logging;

namespace ASP.NET_API_Template.EF.Repositories;


public class BaseRepository<T>(AppDbContext _context, ILogger<BaseRepository<T>> _logger) :
    IBaseRepository<T> where T : class
{


    #region GetByIdAsync Methods

    public async Task<T> GetByIdAsync(int id)
    {
        _logger.LogDebug("Fetching entity of type {EntityType} by ID: {Id}", typeof(T).Name, id);
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T> GetByIdAsync(int? id)
    {
        if (id is null)
            return null;
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        try
        {
            return await _context.Set<T>().FindAsync(id);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<T?> GetByIdAsync(Guid? id)
    {
        try
        {
            if (!id.HasValue) return null;
            return await _context.Set<T>().FindAsync(id.Value);
        }
        catch (Exception)
        {
            return null;
        }
    }

    #endregion

    #region Find Methods

    public async Task<T> FindAsync(
        Expression<Func<T, bool>> criteria,
        params Expression<Func<T, object>>[] includes)
    {
        _logger.LogTrace("Finding entity of type {EntityType} with criteria", typeof(T).Name);
        IQueryable<T> query = _context.Set<T>().Where(criteria);

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.SingleOrDefaultAsync();
    }

    public async Task<TResult> FindWithSelectionAsync<TResult>(
        Expression<Func<T, bool>> criteria,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _context.Set<T>();

        query = query.Where(criteria);

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        var projectedQuery = query.ProjectToType<TResult>();
        return await projectedQuery.FirstOrDefaultAsync();
    }

    #endregion

    #region Read Data Methods

    public async Task<List<T>> GetAllDataAsync(
        Expression<Func<T, object>> orderBy,
        Expression<Func<T, bool>>? criteria = null,
        params Expression<Func<T, object>>[] includes)
    {
        var query = BuildQuery(orderBy, criteria, includes);
        return await query.ToListAsync();
    }

    public async Task<List<TResult>> GetAllDataWithSelectionAsync<TResult>(
        Expression<Func<T, object>> orderBy,
        Expression<Func<T, bool>>? criteria = null,
        params Expression<Func<T, object>>[] includes)
    {
        var query = BuildQuery(orderBy, criteria, includes);
        var projectedQuery = query.ProjectToType<TResult>();

        return await projectedQuery.ToListAsync();
    }

    public async Task<PagedResult<T>> GetPagedDataAsync(
        Expression<Func<T, object>> orderBy,
        Expression<Func<T, bool>>? criteria = null,
        int pageNumber = 1,
        int pageSize = 10,
        params Expression<Func<T, object>>[] includes)
    {
        var query = BuildQuery(orderBy, criteria, includes);

        var totalRecords = await query.CountAsync();
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return PagedResult<T>.Create(totalRecords, pageNumber, pageSize, items);


    }

    public async Task<PagedResult<TResult>> GetPagedDataWithSelectionAsync<TResult>(
        Expression<Func<T, object>> orderBy,
        Expression<Func<T, bool>>? criteria = null,
        int pageNumber = 1,
        int pageSize = 10,
        params Expression<Func<T, object>>[] includes)
    {
        var query = BuildQuery(orderBy, criteria, includes);

        var totalRecords = await query.CountAsync();

        var projectedQuery = query.ProjectToType<TResult>();

        var items = await projectedQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return PagedResult<TResult>.Create(pageSize, pageNumber, totalRecords, items);


    }

    #endregion

    #region Add Methods

    public async Task<T> AddAsync(T entity)
    {
        _logger.LogDebug("Adding entity of type {EntityType}", typeof(T).Name);
        await _context.Set<T>().AddAsync(entity);
        return entity;
    }

    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
    {
        await _context.Set<T>().AddRangeAsync(entities);
        return entities;
    }

    #endregion

    #region Update Methods

    public T Update(T entity)
    {
        _logger.LogDebug("Updating entity of type {EntityType}", typeof(T).Name);
        _context.Update(entity);
        return entity;
    }

    #endregion

    #region Delete Methods

    public async Task DeleteAsync(T entity)
    {
        _logger.LogDebug("Deleting entity of type {EntityType}", typeof(T).Name);
        if (entity is ISoftDeletable softDeletableEntity)
        {
            softDeletableEntity.IsDeleted = true;
            softDeletableEntity.DeletedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
        else
        {
            _context.Set<T>().Remove(entity);
        }
    }

    public async Task DeleteRange(IEnumerable<T> entities)
    {
        if (entities == null || !entities.Any())
            return;

        var entityType = typeof(T);

        // Check if the entity implements ISoftDeletable
        if (typeof(ISoftDeletable).IsAssignableFrom(entityType))
        {
            // Soft delete: Update DeletedDate and IsDeleted
            var now = DateTime.UtcNow;
            var entityIds = entities.Select(e => ((IEntity)e).Id).ToList();
            await _context.Set<T>()
                .Where(e => entityIds.Contains(((IEntity)e).Id))
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(e => ((ISoftDeletable)e).DeletedDate, now)
                    .SetProperty(e => ((ISoftDeletable)e).IsDeleted, true));
        }
        else
        {
            // Hard delete: Remove the entities
            _context.Set<T>().RemoveRange(entities);
            await _context.SaveChangesAsync();
        }
    }

    #endregion

    #region Attach Methods

    public void Attach(T entity)
    {
        _context.Set<T>().Attach(entity);
    }

    public void AttachRange(IEnumerable<T> entities)
    {
        _context.Set<T>().AttachRange(entities);
    }

    #endregion

    #region Query Methods

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> criteria)
    {
        return await _context.Set<T>().AnyAsync(criteria);
    }

    public IQueryable<T> Where(Expression<Func<T, bool>> criteria)
    {
        return _context.Set<T>().Where(criteria);
    }

    public IQueryable<T> AsQueryable()
    {
        return _context.Set<T>();
    }

    public IEnumerable<T> ExecuteRawSql(string query)
    {
        return _context.Set<T>().FromSqlRaw(query).AsEnumerable();
    }

    public IQueryable<T> IgnoreQueryFilters()
    {
        return _context.Set<T>().IgnoreQueryFilters();
    }

    #endregion

    #region Helper Methods

    private IQueryable<T> BuildQuery(
        Expression<Func<T, object>> orderBy,
        Expression<Func<T, bool>>? criteria = null,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _context.Set<T>();

        if (criteria != null)
            query = query.Where(criteria);

        if (includes != null)
        {
            foreach (var include in includes)
                query = query.Include(include);
        }

        return query.OrderBy(orderBy);
    }

    #endregion

}
