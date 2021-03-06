﻿namespace Raven.Documentation.CodeSamples.ClientApi.Commands.Attachments.HowTo
{
	using System.Collections.Generic;

	using Raven.Abstractions.Data;
	using Raven.Client;
	using Raven.Client.Document;
	using Raven.Database.Data;
	using Raven.Json.Linq;

	public class Head
	{
		private interface IFoo
		{
			#region head_1_0
			Attachment HeadAttachment(string key);
			#endregion

			#region head_2_0
			IEnumerable<Attachment> GetAttachmentHeadersStartingWith(
				string idPrefix,
				int start,
				int pageSize);
			#endregion
		}

		public Head()
		{
			using (var store = new DocumentStore())
			{
				#region head_1_1
				var attachment = store
					.DatabaseCommands
					.HeadAttachment("albums/holidays/sea.jpg"); // null if does not exist
				#endregion
			}

			using (var store = new DocumentStore())
			{
				#region head_2_1
				var attachments = store
					.DatabaseCommands
					.GetAttachmentHeadersStartingWith("albums/holidays/", 0, 10);
				#endregion
			}
		}
	}
}