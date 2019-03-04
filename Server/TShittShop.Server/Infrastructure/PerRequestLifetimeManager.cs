using Microsoft.AspNetCore.Http;
using System;
using Unity.Lifetime;

namespace TShirtShop.Server.Infrastructure
{
    public class PerRequestLifetimeManager : LifetimeManager,
                                             IInstanceLifetimeManager,
                                             IFactoryLifetimeManager,
                                             ITypeLifetimeManager
    {
        private readonly object lifetimeKey = new object();

        private IHttpContextAccessor contextAccessor;

        public PerRequestLifetimeManager(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        /// <summary>
        /// Retrieves a value from the backing store associated with this lifetime policy.
        /// </summary>
        /// <returns>The desired object, or null if no such object is currently stored.</returns>
        public override object GetValue(ILifetimeContainer container = null)
        {
            return contextAccessor.HttpContext.Items[lifetimeKey];
        }

        /// <summary>
        /// Stores the given value into the backing store for retrieval later.
        /// </summary>
        /// <param name="newValue">The object being stored.</param>
        /// <param name="container"></param>
        public override void SetValue(object newValue, ILifetimeContainer container = null)
        {
            contextAccessor.HttpContext.Items[lifetimeKey] = newValue;
        }

        /// <summary>
        /// Removes the given object from the backing store.
        /// </summary>
        public override void RemoveValue(ILifetimeContainer container = null)
        {
            var disposable = GetValue() as IDisposable;

            disposable?.Dispose();

            contextAccessor.HttpContext.Items.Remove(lifetimeKey);
        }

        /// <summary>
        /// Creates clone
        /// </summary>
        /// <returns></returns>
        protected override LifetimeManager OnCreateLifetimeManager()
        {
            return new PerRequestLifetimeManager(contextAccessor);
        }
    }
}
