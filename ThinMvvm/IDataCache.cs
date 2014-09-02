﻿// Copyright (c) Solal Pirelli 2014
// See License.txt file for more details

using System;

namespace ThinMvvm
{
    /// <summary>
    /// Caches one ID -> Data map per Type.
    /// </summary>
    public interface IDataCache
    {
        /// <summary>
        /// Attempts to get the value stored by the specified owner type, with the specified ID.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="owner">The owner type.</param>
        /// <param name="id">The ID.</param>
        /// <param name="value">The value, if any.</param>
        /// <returns>A value indicating whether a value was found.</returns>
        bool TryGet<T>( Type owner, long id, out T value );

        /// <summary>
        /// Sets the specified value for the specified owner type, with the specified ID.
        /// </summary>
        /// <param name="owner">The owner type.</param>
        /// <param name="id">The ID.</param>
        /// <param name="expirationDate">The expiration date.</param>
        /// <param name="value">The value.</param>
        void Set( Type owner, long id, DateTimeOffset expirationDate, object value );
    }
}