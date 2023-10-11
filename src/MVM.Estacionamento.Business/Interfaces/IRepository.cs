using System;
using System.Linq.Expressions;
using MVM.Estacionamento.Core;

namespace MVM.Estacionamento.Business.Interfaces;

public interface IRepository<TEntity> : IDisposable
    where TEntity : Entity
{
    Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> ObterTodos();
    Task<TEntity?> ObterPorId(Guid id);
    Task<TEntity> Adicionar(TEntity entity);
    Task Atualizar(TEntity entity);
    Task Remover(Guid id);
}

