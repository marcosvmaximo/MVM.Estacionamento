using System;
using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;
using FluentValidation.Results;

namespace MVM.Estacionamento.Core;

public abstract class Entity
{
    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    [NotMapped]
    public ValidationResult ValidationResult { get; protected set; }

    public virtual ValidationResult? Validate<TValidate, TEntity>()
        where TValidate : AbstractValidator<TEntity>, new()
        where TEntity : Entity
    {
        var result = new TValidate().Validate((TEntity)this);

        return result;
    }

    public abstract void Validar();
}

