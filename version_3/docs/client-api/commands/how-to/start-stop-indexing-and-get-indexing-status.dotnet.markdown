# Client API : Commands : How to start or stop indexing and get indexing status?

Following commands have been created to enable user to toggle indexing and retrieve indexing status:   
- [StartIndexing]()   
- [StopIndexing]()   
- [GetIndexingStatus]()

## StartIndexing

This methods starts indexing, if it was previously stopped.

### Syntax

{CODE start_indexing_1@ClientApi\Commands\HowTo\StartStopIndexingAndGetIndexingStatus.cs /}

### Example

{CODE start_indexing_2@ClientApi\Commands\HowTo\StartStopIndexingAndGetIndexingStatus.cs /}

## StopIndexing

This methods stops indexing, if it was running.

### Syntax

{CODE stop_indexing_1@ClientApi\Commands\HowTo\StartStopIndexingAndGetIndexingStatus.cs /}

### Example

{CODE stop_indexing_2@ClientApi\Commands\HowTo\StartStopIndexingAndGetIndexingStatus.cs /}

## GetIndexingStatus

This methods retrieves current status of the indexing.

### Syntax

{CODE get_indexing_status_1@ClientApi\Commands\HowTo\StartStopIndexingAndGetIndexingStatus.cs /}

**Return Value**

Type: string   
`"Indexing"` if the indexing is running, `"Paused"` otherwise.

### Example

{CODE get_indexing_status_2@ClientApi\Commands\HowTo\StartStopIndexingAndGetIndexingStatus.cs /}

#### Related articles

- [How to **create** or **delete database**?](../../../client-api/commands/how-to/create-delete-database)   