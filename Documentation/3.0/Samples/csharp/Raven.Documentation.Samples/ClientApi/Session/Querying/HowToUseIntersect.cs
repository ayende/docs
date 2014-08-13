﻿using System.Linq;

using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Linq;
using Raven.Documentation.CodeSamples.Indexes.Querying;

namespace Raven.Documentation.CodeSamples.ClientApi.Session.Querying
{
	public class HowToUseIntersect
	{
		private interface IFoo
		{
			/*
			#region intersect_1
			IRavenQueryable<T> Intersect<T>(this IQueryable<T> self) { ... }
			#endregion
			*/
		}

		public HowToUseIntersect()
		{
			using (var store = new DocumentStore())
			{
				using (var session = store.OpenSession())
				{
					#region intersect_2
					// return all T-shirts that are manufactured by 'Raven'
					// and contain both 'Small Blue' and 'Large Gray' types
					var tshirts = session
							.Query<TShirt>("TShirtIndex")
							.Where(x => x.Manufacturer == "Raven")
							.Intersect()
							.Where(x => x.Types.Any(t => t.Color == "Blue" && t.Size == "Small"))
							.Intersect()
							.Where(x => x.Types.Any(t => t.Color == "Gray" && t.Size == "Large"))
							.ToList();
					#endregion
				}
			}
		}
	}
}