﻿using System;

using Raven.Client.Document;
using Raven.Json.Linq;

using Xunit;

namespace Raven.Documentation.CodeSamples.ClientApi.Commands.HowTo
{
	public class DisableCaching
	{
		private interface IFoo
		{
			#region disable_caching_1
			IDisposable DisableAllCaching();
			#endregion
		}

		public DisableCaching()
		{
			using (var store = new DocumentStore())
			{
				#region disable_caching_2
				store.DatabaseCommands.Put("people/1", null, new RavenJObject(), new RavenJObject());
				store.DatabaseCommands.Put("people/2", null, new RavenJObject(), new RavenJObject());

				store.DatabaseCommands.Get("people/1"); // Response: '200 OK'
				Assert.Equal(0, store.JsonRequestFactory.NumberOfCachedRequests); // nothing in cache

				store.DatabaseCommands.Get("people/1"); // Response: '304 Not Modified'
				Assert.Equal(1, store.JsonRequestFactory.NumberOfCachedRequests); // cached 'people/1' for future calls

				using (store.DatabaseCommands.DisableAllCaching())
				{
					store.DatabaseCommands.Get("people/2"); // Response: '200 OK'
					Assert.Equal(1, store.JsonRequestFactory.NumberOfCachedRequests); // cache state not changed
					store.DatabaseCommands.Get("people/2"); // Response: '304 Not Modified'
					Assert.Equal(1, store.JsonRequestFactory.NumberOfCachedRequests); // cache state not changed
				}

				store.DatabaseCommands.Get("people/2"); // Response: '304 Not Modified'
				Assert.Equal(2, store.JsonRequestFactory.NumberOfCachedRequests); // cached 'people/2' for future calls
				#endregion
			}
		}
	}
}