using System;
using System.Runtime.CompilerServices;
using Munq.LifetimeManagers;

#if !PORTABLE
using System.Web;
#endif

namespace Munq
{
	public static class LifetimeExtensions
	{		
		readonly static ContainerLifetime          containerLifetime   = new ContainerLifetime();
#if !PORTABLE
		readonly static CachedLifetime             cachedLifetime      = new CachedLifetime();
		readonly static RequestLifetime            requestLifetime     = new RequestLifetime();
		readonly static SessionLifetime            sessionLifetime     = new SessionLifetime();
		readonly static ThreadLocalStorageLifetime threadLocalLifetime = new ThreadLocalStorageLifetime();
#endif

		public static IRegistration AsAlwaysNew(this IRegistration reg)
		{
			return reg.WithLifetimeManager(null);
		}

		public static IRegistration AsContainerSingleton(this IRegistration reg)
		{
			return reg.WithLifetimeManager(containerLifetime);
		}

#if !PORTABLE
		public static IRegistration AsCached(this IRegistration reg)
		{
			return reg.WithLifetimeManager(cachedLifetime);
		}

		public static IRegistration AsRequestSingleton(this IRegistration reg)
		{
			return reg.WithLifetimeManager(requestLifetime);
		}

		public static IRegistration AsSessionSingleton(this IRegistration reg)
		{
			return reg.WithLifetimeManager(sessionLifetime);
		}

		public static IRegistration AsThreadSingleton(this IRegistration reg)
		{
			return reg.WithLifetimeManager(threadLocalLifetime);
		}

		// for testing only
		public static HttpContextBase HttpContext
		{
			set
			{
				RequestLifetime.SetContext(value);
				sessionLifetime.SetContext(value);
			} 
		}
#endif
	}
}
