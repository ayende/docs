# Session : Querying : How to stream query results?

Query results can be streamed using `Stream` method from `Advanced` session operations.

## Syntax

{CODE stream_1@ClientApi\Session\Querying\HowToStream.cs /}

**Parameters**

query
:   Type: [IQueryable](../../../client-api/session/querying/how-to-query) or [IDocumentQuery](../../../client-api/session/querying/how-to-use-lucene-in-queries)   
Query to stream results for.

queryHeaderInformation
:   Type: [QueryHeaderInformation](../../../glossary/client-api/query-header-information)   
Information about performed query.

**Return value**

Type: IEnumerator<[StreamResult]()>   
Enumerator with entities.

Type: [QueryHeaderInformation](../../../glossary/client-api/query-header-information)   
Information about performed query.

## Example I

{CODE stream_2@ClientApi\Session\Querying\HowToStream.cs /}

## Example II

{CODE stream_3@ClientApi\Session\Querying\HowToStream.cs /}

#### Related articles

TODO