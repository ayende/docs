# Client API : Documents : How to delete or update documents using index?

Sometimes we need to update or delete a large amount of documents answering some criteria. With SQL this is a natural operation, and a query doing that will look like this:

`DELETE FROM Users WHERE LastLogin < '2009-01-01'`   
`UPDATE Users SET IsActive = 0 WHERE LastLogin < '2010-01-01'`   

This is usually not the case for NoSQL databases, where batch operations are not supported. RavenDB does support this, and by passing it a query and an operation definition, it will run the query and perform that operation on it's results.

The same queries and indexes that are used for data retrieval are used for the batch operations, therefore the syntax for defining which documents to do work on is exactly the same as you'd specify for those documents to be pulled from store.

## DeleteByIndex

To issue a batch-delete command you need to specify an index, and a query to be sent to it. To minimize the chances of stale results coming back, bulk operations should only be performed on static indexes.

### Syntax

{CODE delete_by_index_1@ClientApi\Commands\Documents\HowTo\DeleteOrUpdateByIndex.cs /}

- indexName - name of an index to perform a query on   
- queryToDelete - query that will be performed    
- allowStale - can operation be performed on a stale index   

### Return Value

{CODE operation@Common.cs /}

- WaitForCompletion - returns state of an operation   

### Example

{CODE delete_by_index_2@ClientApi\Commands\Documents\HowTo\DeleteOrUpdateByIndex.cs /}

## UpdateByIndex

Performing a mass-update is exactly the same as making a mass-delete, but this time it uses the Patching API to make it easy for us to define what to do with the documents matching our query.

### Syntax

{CODE update_by_index_1@ClientApi\Commands\Documents\HowTo\DeleteOrUpdateByIndex.cs /}

- indexName - name of an index to perform a query on   
- queryToUpdate - query that will be performed  
- patchRequests - array of patches that will be executed on query results     
- allowStale - can operation be performed on a stale index   

### Return Value

{CODE operation@Common.cs /}

- WaitForCompletion - returns state of an operation   

### Example

{CODE update_by_index_2@ClientApi\Commands\Documents\HowTo\DeleteOrUpdateByIndex.cs /}

## UpdateByIndex

Mass-update can also be executed with JavaScript patch.

### Syntax

{CODE update_by_index_3@ClientApi\Commands\Documents\HowTo\DeleteOrUpdateByIndex.cs /}

- indexName - name of an index to perform a query on   
- queryToUpdate - query that will be performed  
- patch - JavaScript patch that will be executed on query results        
- allowStale - can operation be performed on a stale index   

### Return Value

{CODE operation@Common.cs /}

- WaitForCompletion - returns state of an operation   

### Example

{CODE update_by_index_4@ClientApi\Commands\Documents\HowTo\DeleteOrUpdateByIndex.cs /}

## Remarks

By default, set-based operations will **not** work on indexes that are stale, and the operation will only succeed if the specified index is NOT stale. This is to make sure you only delete what you intended to delete, and is part of RavenDB's safe-by-default approach.

For indexes that are updated all the time, you can set a `Cutoff` in the `IndexQuery` object you send, and that will make sure the operation is executed and that you know what results to expect.

When you absolutely certain you can perform the operation also when the index is stale, simply set the `allowStale` parameter to true.

#### Related articles

- [Put](../../../../client-api/commands/documents/put)  
- [Delete](../../../../client-api/commands/documents/delete)  