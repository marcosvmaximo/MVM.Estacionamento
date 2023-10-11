using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MVM.Estacionamento.Business.Interfaces;
using MVM.Estacionamento.Core;
using MVM.Estacionamento.Data.Context;

namespace MVM.Estacionamento.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
{
    protected readonly DataContext _context;
    protected DbSet<TEntity> _dbSet;

    public Repository(DataContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity> Adicionar(TEntity entity)
    {
        var result = await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task Atualizar(TEntity entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    public async void Dispose()
    {
        await _context.DisposeAsync();
    }

    public async Task<TEntity> ObterPorId(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> ObterTodos()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task Remover(Guid id)
    {
        _dbSet.Remove(new TEntity { Id = id });
    }

    public async Task<bool> SaveChanges()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}

