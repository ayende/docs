﻿using Raven.Abstractions.Data;
using Raven.Client.Document;

namespace Raven.Documentation.CodeSamples.ClientApi.Session
{
	public class StoringEntities
	{
		private interface IFoo
		{
			#region store_entities_1
			void Store(dynamic entity);
			#endregion

			#region store_entities_2
			void Store(dynamic entity, string id);
			#endregion

			#region store_entities_3
			void Store(object entity, Etag etag);
			#endregion

			#region store_entities_4
			void Store(object entity, Etag etag, string id);
			#endregion
		}

		public StoringEntities()
		{
			using (var store = new DocumentStore())
			{
				using (var session = store.OpenSession())
				{
					#region store_entities_5
					// generate Id automatically
					// with new empty database and default conventions: 'people/1'
					session.Store(new Person
						              {
							              FirstName = "John", 
										  LastName = "Doe"
						              });

					// send all pending operations to server, in this case only `Put` operation
					session.SaveChanges();
					#endregion
				}
			}
		}
	}
}