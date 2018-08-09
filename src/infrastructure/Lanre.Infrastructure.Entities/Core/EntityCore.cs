namespace Lanre.Infrastructure.Entities.Core
{
    using System;

    public abstract class EntityCore<T>
    {
        public T Id { get; set; }
    }

    public abstract class EntityCore: EntityCore<Guid>
    {
    }
}
