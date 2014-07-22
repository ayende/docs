﻿using Raven.Client.Document;

namespace Raven.Documentation.CodeSamples.ClientApi.Session.HowTo
{
	public class Clear
	{
		private interface IFoo
		{
			#region clear_1
			void Clear();
			#endregion
		}

		public Clear()
		{
			using (var store = new DocumentStore())
			{
				using (var session = store.OpenSession())
				{
					#region clear_2
					session.Store(new Person
									  {
										  FirstName = "John",
										  LastName = "Doe"
									  });

					session.Advanced.Clear();

					session.SaveChanges(); // nothing will happen
					#endregion
				}
			}
		}
	}
}