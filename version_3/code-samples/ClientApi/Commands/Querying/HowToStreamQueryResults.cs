﻿using System.Collections.Generic;

using Raven.Abstractions.Data;
using Raven.Client.Document;
using Raven.Json.Linq;

namespace Raven.Documentation.CodeSamples.ClientApi.Commands.Querying
{
	public class HowToStreamQueryResults
	{
		private interface IFoo
		{
			#region stream_query_1
			IEnumerator<RavenJObject> StreamQuery(
				string index,
				IndexQuery query,
				out QueryHeaderInformation queryHeaderInfo);
			#endregion
		}

		public HowToStreamQueryResults()
		{
			using (var store = new DocumentStore())
			{
				#region stream_query_2
				QueryHeaderInformation queryHeaderInfo;
				var enumerator = store
					.DatabaseCommands
					.StreamQuery(
						"Users/ByName",
						new IndexQuery
						{
							Query = "Name:James"
						},
						out queryHeaderInfo);

				while (enumerator.MoveNext())
				{
					var user = enumerator.Current;
				}
				#endregion
			}
		}
	}
}