﻿namespace Raven.Documentation.CodeSamples
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Threading.Tasks;

	using Raven.Abstractions.Data;
	using Raven.Abstractions.Indexing;
	using Raven.Json.Linq;

	public class Common
	{
		#region multiloadresult
		public class MultiLoadResult
		{
			public List<RavenJObject> Results { get; set; }

			public List<RavenJObject> Includes { get; set; }
		}
		#endregion

		#region putresult
		public class PutResult
		{
			public string Key { get; set; }

			public Etag ETag { get; set; }
		}
		#endregion

		#region getrequest
		public class GetRequest
		{
			public string Url { get; set; }

			public IDictionary<string, string> Headers { get; set; }

			public string Query { get; set; }
		}
		#endregion

		#region getresponse
		public class GetResponse
		{
			public RavenJToken Result { get; set; }

			public IDictionary<string, string> Headers { get; set; }

			public int Status { get; set; }
		}
		#endregion

		#region transformerdefinition
		public class TransformerDefinition
		{
			public string TransformResults { get; set; }

			public int TransfomerId { get; set; }

			public string Name { get; set; }
		}
		#endregion

		#region indexdefinition
		public class IndexDefinition
		{
			/// <summary>
			/// Get or set the id of this index
			/// </summary>
			public int IndexId { get; set; }

			/// <summary>
			/// This is the means by which the outside world refers to this index defiintion
			/// </summary>
			public string Name { get; set; }

			/// <summary>
			/// Get or set the index lock mode
			/// </summary>
			public IndexLockMode LockMode { get; set; }

			/// <summary>
			/// All the map functions for this index
			/// </summary>
			public HashSet<string> Maps { get; set; }

			/// <summary>
			/// Gets or sets the reduce function
			/// </summary>
			/// <value>The reduce.</value>
			public string Reduce { get; set; }

			public bool IsCompiled { get; set; }

			/// <summary>
			/// Gets or sets the stores options
			/// </summary>
			/// <value>The stores.</value>
			public IDictionary<string, FieldStorage> Stores { get; set; }

			/// <summary>
			/// Gets or sets the indexing options
			/// </summary>
			/// <value>The indexes.</value>
			public IDictionary<string, FieldIndexing> Indexes { get; set; }

			/// <summary>
			/// Gets or sets the sort options.
			/// </summary>
			/// <value>The sort options.</value>
			public IDictionary<string, SortOptions> SortOptions { get; set; }

			/// <summary>
			/// Gets or sets the analyzers options
			/// </summary>
			/// <value>The analyzers.</value>
			public IDictionary<string, string> Analyzers { get; set; }

			/// <summary>
			/// The fields that are queryable in the index
			/// </summary>
			public IList<string> Fields { get; set; }

			/// <summary>
			/// Gets or sets the suggest options
			/// </summary>
			/// <value>The suggest options.</value>
			public IDictionary<string, SuggestionOptions> Suggestions { get; set; }

			/// <summary>
			/// Gets or sets the term vectors options
			/// </summary>
			/// <value>The term vectors.</value>
			public IDictionary<string, FieldTermVector> TermVectors { get; set; }

			/// <summary>
			/// Gets or sets the spatial options
			/// </summary>
			/// <value>The spatial options.</value>
			public IDictionary<string, SpatialOptions> SpatialIndexes { get; set; }

			/// <summary>
			/// Internal map of field names to expressions generating them
			/// Only relevant for auto indexes and only used internally
			/// </summary>
			public IDictionary<string, string> InternalFieldsMapping { get; set; }

			/// <summary>
			/// Index specific setting that limits the number of map outputs that an index is allowed to create for a one source document. If a map operation applied to
			/// the one document produces more outputs than this number then an index definition will be considered as a suspicious and the index will be marked as errored.
			/// Default value: null means that the global value from Raven configuration will be taken to detect if number of outputs was exceeded.
			/// </summary>
			public int? MaxIndexOutputsPerDocument { get; set; }

			/// <summary>
			/// Prevent index from being kept in memory. Default: false
			/// </summary>
			public bool DisableInMemoryIndexing { get; set; }
		}
		#endregion

		#region indexmergeresults
		public class IndexMergeResults
		{
			/// <summary>
			/// Dictionary of unmergable indexes with reason why they can't be merged
			/// </summary>
			public Dictionary<string, string> Unmergables = new Dictionary<string, string>();

			/// <summary>
			/// List of all merge suggestions
			/// </summary>
			public List<MergeSuggestions> Suggestions = new List<MergeSuggestions>();
		}
		#endregion

		#region mergesuggestions
		public class MergeSuggestions
		{
			/// <summary>
			/// List of all indexes (names) that can be merged together
			/// </summary>
			public List<string> CanMerge = new List<string>();

			/// <summary>
			/// Proposition of a new index definition for a merged index
			/// </summary>
			public IndexDefinition MergedIndex = new IndexDefinition();
		}
		#endregion

		#region attachment
		public class Attachment
		{
			/// <summary>
			/// Function that returns attachment data
			/// </summary>
			public Func<Stream> Data { get; set; }

			/// <summary>
			/// Size of the attachment.
			/// </summary>
			/// <remarks>The max size of an attachment can be 2GB.</remarks>
			public int Size { get; set; }

			/// <summary>
			/// Attachment metadata
			/// </summary>
			public RavenJObject Metadata { get; set; }

			/// <summary>
			/// Attachment ETag
			/// </summary>
			public Etag Etag { get; set; }

			/// <summary>
			/// Attachment key
			/// </summary>
			public string Key { get; set; }
		}
		#endregion

		#region attachmentinformation
		public class AttachmentInformation
		{
			/// <summary>
			/// Size of the attachment.
			/// </summary>
			public int Size { get; set; }

			/// <summary>
			/// Attachment key
			/// </summary>
			public string Key { get; set; }

			/// <summary>
			/// Attachment metadata
			/// </summary>
			public RavenJObject Metadata { get; set; }

			/// <summary>
			/// Attachment ETag
			/// </summary>
			public Etag Etag { get; set; }
		}
		#endregion

		#region patchrequest
		public class PatchRequest
		{
			/// <summary>
			/// Type of the operation
			/// </summary>
			public PatchCommandType Type { get; set; }

			/// <summary>
			/// Gets or sets the previous value, which is compared against the current value to verify a
			/// change isn't overwriting new values.
			/// If the value is null, the operation is always successful
			/// </summary>
			public RavenJToken PrevVal { get; set; }

			/// <summary>
			/// Value that will be applied
			/// </summary>
			public RavenJToken Value { get; set; }

			/// <summary>
			/// Gets or sets the nested operations to perform. This is only valid when the <see cref="Type"/> is <see cref="PatchCommandType.Modify"/>.
			/// </summary>
			public PatchRequest[] Nested { get; set; }

			/// <summary>
			/// Name of the property that will be patched
			/// </summary>
			public string Name { get; set; }

			/// <summary>
			/// Position in array that will be patched
			/// </summary>
			public int? Position { get; set; }

			/// <summary>
			/// Set this property to true if you want to modify all items in an collection.
			/// </summary>
			public bool? AllPositions { get; set; }
		}
		#endregion

		#region patchcommandtype
		public enum PatchCommandType
		{
			/// <summary>
			/// Set a property
			/// </summary>
			Set,
			/// <summary>
			/// Unset (remove) a property
			/// </summary>
			Unset,
			/// <summary>
			/// Add an item to an array
			/// </summary>
			Add,
			/// <summary>
			/// Append an item to an array
			/// </summary>
			Insert,
			/// <summary>
			/// Remove an item from an array at a specified position
			/// </summary>
			Remove,
			/// <summary>
			/// Modify a property value by providing a nested set of patch operation
			/// </summary>
			Modify,
			/// <summary>
			/// Increment a property by a specified value
			/// </summary>
			Inc,
			/// <summary>
			/// Copy a property value to another property
			/// </summary>
			Copy,
			/// <summary>
			/// Rename a property
			/// </summary>
			Rename
		}
		#endregion

		#region scriptedpatchrequest
		public class ScriptedPatchRequest
		{
			/// <summary>
			/// JavaScript function that will patch a document
			/// </summary>
			/// <value>The type.</value>
			public string Script { get; set; }

			/// <summary>
			/// Additional values that will be passed to function
			/// </summary>
			public Dictionary<string, object> Values { get; set; }
		}
		#endregion

		#region putcommanddata
		public class PutCommandData
		{
			public string Key { get; set; }

			public string Method
			{
				get { return "PUT"; }
			}

			public Etag Etag { get; set; }

			public RavenJObject Document { get; set; }

			public TransactionInformation TransactionInformation { get; set; }

			public RavenJObject Metadata { get; set; }

			public RavenJObject AdditionalData { get; set; }
		}
		#endregion

		#region deletecommanddata
		public class DeleteCommandData
		{
			public string Key { get; set; }

			public string Method
			{
				get { return "DELETE"; }
			}

			public Etag Etag { get; set; }

			public TransactionInformation TransactionInformation { get; set; }

			public RavenJObject Metadata
			{
				get { return null; }
			}

			public RavenJObject AdditionalData { get; set; }
		}
		#endregion

		#region patchcommanddata
		public class PatchCommandData
		{
			public PatchRequest[] Patches { get; set; }

			public PatchRequest[] PatchesIfMissing { get; set; }

			public string Key { get; set; }

			public string Method
			{
				get { return "PATCH"; }
			}

			public Etag Etag { get; set; }

			public TransactionInformation TransactionInformation { get; set; }

			public RavenJObject Metadata { get; set; }

			public RavenJObject AdditionalData { get; set; }
		}
		#endregion

		#region scriptedpatchcommanddata
		public class ScriptedPatchCommandData
		{
			/// <summary>
			/// ScriptedPatchRequest (using JavaScript) that is used to patch the document
			/// </summary>
			public ScriptedPatchRequest Patch { get; set; }

			/// <summary>
			/// ScriptedPatchRequest (using JavaScript) that is used to patch a default document if the document is missing
			/// </summary>
			public ScriptedPatchRequest PatchIfMissing { get; set; }

			public string Key { get; set; }

			public string Method
			{
				get { return "EVAL"; }
			}

			public Etag Etag { get; set; }

			public TransactionInformation TransactionInformation { get; set; }

			public RavenJObject Metadata { get; set; }

			public bool DebugMode { get; set; }

			public RavenJObject AdditionalData { get; set; }
		}
		#endregion

		#region batchresult
		public class BatchResult
		{
			/// <summary>
			/// ETag generated by the etag (if applicable)
			/// </summary>
			public Etag Etag { get; set; }

			/// <summary>
			/// Method used for the operation (PUT,DELETE,PATCH).
			/// </summary>
			public string Method { get; set; }

			/// <summary>
			/// Document key
			/// </summary>
			public string Key { get; set; }

			/// <summary>
			/// Document metadata.
			/// </summary>
			public RavenJObject Metadata { get; set; }

			/// <summary>
			/// Additional data
			/// </summary>
			public RavenJObject AdditionalData { get; set; }

			/// <summary>
			/// Result of a PATCH operation.
			/// </summary>
			public PatchResult? PatchResult { get; set; }

			/// <summary>
			/// Result of a DELETE operation.
			/// </summary>
			/// <value>true if the document was deleted, false if it did not exist.</value>
			public bool? Deleted { get; set; }
		}
		#endregion

		#region databasestatistics
		public class DatabaseStatistics
		{
			public Etag LastDocEtag { get; set; }
			public Etag LastAttachmentEtag { get; set; }
			public int CountOfIndexes { get; set; }
			public int InMemoryIndexingQueueSize { get; set; }
			public long ApproximateTaskCount { get; set; }

			public long CountOfDocuments { get; set; }

			public long CountOfAttachments { get; set; }

			public string[] StaleIndexes { get; set; }

			public int CurrentNumberOfItemsToIndexInSingleBatch { get; set; }

			public int CurrentNumberOfItemsToReduceInSingleBatch { get; set; }

			public decimal DatabaseTransactionVersionSizeInMB { get; set; }

			public IndexStats[] Indexes { get; set; }

			public ServerError[] Errors { get; set; }

			public TriggerInfo[] Triggers { get; set; }

			public IEnumerable<ExtensionsLog> Extensions { get; set; }

			public class TriggerInfo
			{
				public string Type { get; set; }
				public string Name { get; set; }
			}

			public ActualIndexingBatchSize[] ActualIndexingBatchSize { get; set; }
			public FutureBatchStats[] Prefetches { get; set; }

			public Guid DatabaseId { get; set; }

			public bool SupportsDtc { get; set; }
		}
		#endregion

		#region actualindexingbatchsize
		public class ActualIndexingBatchSize
		{
			public int Size { get; set; }
			public DateTime Timestamp { get; set; }
		}
		#endregion

		#region futurebatchstats
		public class FutureBatchStats
		{
			public DateTime Timestamp { get; set; }
			public TimeSpan? Duration { get; set; }
			public int? Size { get; set; }
			public int Retries { get; set; }
		}
		#endregion

		#region extensionslog
		public class ExtensionsLog
		{
			public string Name { get; set; }
			public ExtensionsLogDetail[] Installed { get; set; }
		}
		#endregion

		#region extensionslogdetail
		public class ExtensionsLogDetail
		{
			public string Name { get; set; }
			public string Assembly { get; set; }
		}
		#endregion

		#region buildnumber
		public class BuildNumber
		{
			public string ProductVersion { get; set; }

			public string BuildVersion { get; set; }
		}
		#endregion

		#region queryheaderinformation
		public class QueryHeaderInformation
		{
			public string Index { get; set; }

			public bool IsStale { get; set; }

			public DateTime IndexTimestamp { get; set; }

			public int TotalResults { get; set; }

			public Etag ResultEtag { get; set; }

			public Etag IndexEtag { get; set; }
		}
		#endregion
	}

	public class CodeOmitted : Exception
	{
	}
}
