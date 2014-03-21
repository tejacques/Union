using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functional.Union
{
    /// <summary>
    /// A self referential class union of four types
    /// </summary>
    /// <typeparam name="RUImpl">The implementing RefUnion</typeparam>
    /// <typeparam name="T1">The first type</typeparam>
    /// <typeparam name="T2">The second type</typeparam>
    /// <typeparam name="T3">The third type</typeparam>
    /// <typeparam name="T4">The fourth type</typeparam>
    /// <typeparam name="T5">The fifth type</typeparam>
    public class RefUnion<RUImpl, T1, T2, T3, T4, T5>
        where RUImpl : RefUnion<RUImpl, T1, T2, T3, T4, T5>, new()
    {
        #region Fields

        /// <summary>
        /// The underlying union
        /// </summary>
        protected Union<T1, T2, T3, T4, T5> union;
        #endregion

        #region Properties

        /// <summary>
        /// Gets the tag of the underlying union
        /// </summary>
        protected Union<T1, T2, T3, T4, T5>.UnionTypes Tag
        {
            get { return union._tag; }
        }
        #endregion

        #region Instantiators
        /// <summary>
        /// Creates the implementing RefUnion type from the first type
        /// </summary>
        /// <param name="val">The value</param>
        /// <returns>A new union</returns>
        public static RUImpl Create(T1 val)
        {
            return new RUImpl() { union = val };
        }

        /// <summary>
        /// Creates the implementing RefUnion type from the second type
        /// </summary>
        /// <param name="val">The value</param>
        /// <returns>A new union</returns>
        public static RUImpl Create(T2 val)
        {
            return new RUImpl() { union = val };
        }

        /// <summary>
        /// Creates the implementing RefUnion type from the third type
        /// </summary>
        /// <param name="val">The value</param>
        /// <returns>A new union</returns>
        public static RUImpl Create(T3 val)
        {
            return new RUImpl() { union = val };
        }

        /// <summary>
        /// Creates the implementing RefUnion type from the fourth type
        /// </summary>
        /// <param name="val">The value</param>
        /// <returns>A new union</returns>
        public static RUImpl Create(T4 val)
        {
            return new RUImpl() { union = val };
        }

        /// <summary>
        /// Creates the implementing RefUnion type from the underlying union
        /// </summary>
        /// <param name="union">The value</param>
        /// <returns>A new union</returns>
        public static RUImpl Create(Union<T1, T2, T3, T4, T5> union)
        {
            return new RUImpl() { union = union };
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
            return this.union.ValueOr(val);
        }

        /// <summary>
        /// Gets the value of the first type or the provided value if the union
        /// is not holding a value of the first type
        /// </summary>
        /// <param name="val">The default value</param>
        /// <returns>The value in the union or the default value</returns>
        public T1 ValueOr(Func<T1> val)
        {
            return this.union.ValueOr(val);
        }

        /// <summary>
        /// Gets the value of the second type or the provided value if the
        /// union is not holding a value of the second type
        /// </summary>
        /// <param name="val">The default value</param>
        /// <returns>The value in the union or the default value</returns>
        public T2 ValueOr(T2 val)
        {
            return this.union.ValueOr(val);
        }

        /// <summary>
        /// Gets the value of the second type or the provided value if the
        /// union is not holding a value of the second type
        /// </summary>
        /// <param name="val">The default value</param>
        /// <returns>The value in the union or the default value</returns>
        public T2 ValueOr(Func<T2> val)
        {
            return this.union.ValueOr(val);
        }

        /// <summary>
        /// Gets the value of the third type or the provided value if the
        /// union is not holding a value of the third type
        /// </summary>
        /// <param name="val">The default value</param>
        /// <returns>The value in the union or the default value</returns>
        public T3 ValueOr(T3 val)
        {
            return this.union.ValueOr(val);
        }

        /// <summary>
        /// Gets the value of the third type or the provided value if the
        /// union is not holding a value of the third type
        /// </summary>
        /// <param name="val">The default value</param>
        /// <returns>The value in the union or the default value</returns>
        public T3 ValueOr(Func<T3> val)
        {
            return this.union.ValueOr(val);
        }

        /// <summary>
        /// Gets the value of the fourth type or the provided value if the
        /// union is not holding a value of the fourth type
        /// </summary>
        /// <param name="val">The default value</param>
        /// <returns>The value in the union or the default value</returns>
        public T4 ValueOr(T4 val)
        {
            return this.union.ValueOr(val);
        }

        /// <summary>
        /// Gets the value of the fourth type or the provided value if the
        /// union is not holding a value of the fourth type
        /// </summary>
        /// <param name="val">The default value</param>
        /// <returns>The value in the union or the default value</returns>
        public T4 ValueOr(Func<T4> val)
        {
            return this.union.ValueOr(val);
        }

        /// <summary>
        /// Gets the value of the fifth type or the provided value if the
        /// union is not holding a value of the fifth type
        /// </summary>
        /// <param name="val">The default value</param>
        /// <returns>The value in the union or the default value</returns>
        public T5 ValueOr(T5 val)
        {
            return this.union.ValueOr(val);
        }

        /// <summary>
        /// Gets the value of the fifth type or the provided value if the
        /// union is not holding a value of the fifth type
        /// </summary>
        /// <param name="val">The default value</param>
        /// <returns>The value in the union or the default value</returns>
        public T5 ValueOr(Func<T5> val)
        {
            return this.union.ValueOr(val);
        }
        #endregion

        #region Pattern Matching
        /// <summary>
        /// Pattern matches on the type the union is holding to perform
        /// the specified action
        /// </summary>
        /// <param name="Match1">The action for the first type</param>
        /// <param name="Match2">The action for the second type</param>
        /// <param name="Match3">The action for the third type</param>
        /// <param name="Match4">The action for the fourth type</param>
        /// <param name="Match5">The action for the fifth type</param>
        /// <param name="Else">
        /// The default action if the matching action was not specified
        /// </param>
        public virtual void Match(
            Action<T1> Match1 = null,
            Action<T2> Match2 = null,
            Action<T3> Match3 = null,
            Action<T4> Match4 = null,
            Action<T5> Match5 = null,
            Action Else = null)
        {
            union.Match(
                Match1,
                Match2,
                Match3,
                Match4,
                Match5,
                Else);
        }

        /// <summary>
        /// Pattern matches on the type the union is holding to call
        /// the specified function
        /// </summary>
        /// <param name="Match1">The function for the first type</param>
        /// <param name="Match2">The function for the second type</param>
        /// <param name="Match3">The function for the third type</param>
        /// <param name="Match4">The function for the fourth type</param>
        /// <param name="Match5">The function for the fifth type</param>
        /// <param name="Else">
        /// The default function if the matching function was not specified
        /// </param>
        /// <typeparam name="TResult">The return type</typeparam>
        public virtual TResult Match<TResult>(
            Func<T1, TResult> Match1 = null,
            Func<T2, TResult> Match2 = null,
            Func<T3, TResult> Match3 = null,
            Func<T4, TResult> Match4 = null,
            Func<T5, TResult> Match5 = null,
            Func<TResult> Else = null)
        {
            return union.Match<TResult>(
                Match1,
                Match2,
                Match3,
                Match4,
                Match5,
                Else);
        }
        #endregion
    }
}
