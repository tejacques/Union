﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Functional.Union
{
    /// <summary>
    /// A Union of two types
    /// </summary>
    /// <typeparam name="T1">The first type</typeparam>
    /// <typeparam name="T2">The second type</typeparam>
    public struct Union<T1, T2>
    {
        #region Types
        internal enum UnionTypes
        {
            Type1,
            Type2
        }
        #endregion

        #region Fields
        private readonly UnionTypes _tag;
        internal T1 Item1;
        internal T2 Item2;
        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a Union from the first type
        /// </summary>
        /// <param name="item">The value</param>
        public Union(T1 item)
        {
            this.Item2 = default(T2);
            this.Item1 = item;
            this._tag = UnionTypes.Type1;
        }

        /// <summary>
        /// Constructs a Union from the second type
        /// </summary>
        /// <param name="item">The value</param>
        public Union(T2 item)
        {
            this.Item1 = default(T1);
            this.Item2 = item;
            this._tag = UnionTypes.Type2;
        }
        #endregion

        #region Getters

        /// <summary>
        /// Gets the value of the first type or the provided value if the union
        /// is not holding a value of the first type
        /// </summary>
        /// <param name="val">The default value</param>
        /// <returns>The value in the union or the default value</returns>
        public T1 ValueOr(T1 val)
        {
            if (UnionTypes.Type1 == this._tag)
            {
                return this.Item1;
            }

            return val;
        }

        /// <summary>
        /// Gets the value of the first type or the provided value if the union
        /// is not holding a value of the first type
        /// </summary>
        /// <param name="val">The default value</param>
        /// <returns>The value in the union or the default value</returns>
        public T1 ValueOr(Func<T1> val)
        {
            if (UnionTypes.Type1 == this._tag)
            {
                return this.Item1;
            }

            return val();
        }

        /// <summary>
        /// Gets the value of the second type or the provided value if the
        /// union is not holding a value of the second type
        /// </summary>
        /// <param name="val">The default value</param>
        /// <returns>The value in the union or the default value</returns>
        public T2 ValueOr(T2 val)
        {
            if (UnionTypes.Type2 == this._tag)
            {
                return this.Item2;
            }

            return val;
        }

        /// <summary>
        /// Gets the value of the second type or the provided value if the
        /// union is not holding a value of the second type
        /// </summary>
        /// <param name="val">The default value</param>
        /// <returns>The value in the union or the default value</returns>
        public T2 ValueOr(Func<T2> val)
        {
            if (UnionTypes.Type2 == this._tag)
            {
                return this.Item2;
            }

            return val();
        }
        #endregion

        #region Implicit Conversions

        /// <summary>
        /// Creates a new Union from the first type
        /// </summary>
        /// <param name="val">The provided value</param>
        /// <returns>A new Union with the provided value</returns>
        public static implicit operator Union<T1, T2>(T1 val)
        {
            return new Union<T1, T2>(val);
        }

        /// <summary>
        /// Creates a new Union from the second type
        /// </summary>
        /// <param name="val">The provided value</param>
        /// <returns>A new Union with the provided value</returns>
        public static implicit operator Union<T1, T2>(T2 val)
        {
            return new Union<T1, T2>(val);
        }
        #endregion

        #region Pattern Matching

        /// <summary>
        /// Pattern matches on the type the union is holding to perform
        /// the specified action
        /// </summary>
        /// <param name="Match1">The action for the first type</param>
        /// <param name="Match2">The action for the second type</param>
        /// <param name="Else">
        /// The default action if the matching action was not specified
        /// </param>
        public void Match(
            Action<T1> Match1 = null,
            Action<T2> Match2 = null,
            Action Else = null)
        {
            if (null == Else
                && (null == Match1
                    || null == Match2
                    )
                )
            {
                throw new MatchNotExhaustiveException();
            }

            switch (this._tag)
            {
                case UnionTypes.Type1:
                    if (null != Match1) Match1(this.Item1);
                    else Else();
                    break;
                case UnionTypes.Type2:
                    if (null != Match2) Match2(this.Item2);
                    else Else();
                    break;
                default:
                    throw new UnionMatchFailureException(
                        "Unrecognized value: " + this._tag);
            }
        }

        /// <summary>
        /// Pattern matches on the type the union is holding to call
        /// the specified function
        /// </summary>
        /// <param name="Match1">The function for the first type</param>
        /// <param name="Match2">The function for the second type</param>
        /// <param name="Else">
        /// The default function if the matching function was not specified
        /// </param>
        /// <typeparam name="TResult">The return type</typeparam>
        public TResult Match<TResult>(
            Func<T1, TResult> Match1 = null,
            Func<T2, TResult> Match2 = null,
            Func<TResult> Else = null)
        {
            if (null == Else
                && (null == Match1
                    || null == Match2
                    )
                )
            {
                throw new MatchNotExhaustiveException();
            }

            switch (this._tag)
            {
                case UnionTypes.Type1:
                    if (null != Match1) return Match1(this.Item1);
                    else return Else();
                case UnionTypes.Type2:
                    if (null != Match2) return Match2(this.Item2);
                    else return Else();
                default:
                    throw new UnionMatchFailureException(
                        "Unrecognized value: " + this._tag);
            }
        }
        #endregion
    }
}