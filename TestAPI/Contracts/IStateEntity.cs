﻿using System.Data;

namespace TestAPI.Contracts
{
    /// <summary>
    /// Defines an entity where the state is accessable, which enables cleaner code at the data context layer.
    /// </summary>
    public interface IStateEntity
    {
        /// <summary>
        /// Gets the entity state of this entity.
        /// </summary>
        EntityState State { get; }
    }
}
