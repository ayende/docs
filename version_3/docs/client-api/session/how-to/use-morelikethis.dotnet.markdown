# Session : How to use MoreLikeThis?

`MoreLikeThis` is available through extension method in `Advanced` session operations. This method returns articles similar to the provided input.

## Syntax

{CODE more_like_this_1@ClientApi\Session\HowTo\MoreLikeThis.cs /}

**Parameters**

index
:   Type: string   
Nmae of an index on which query will be executed.

documentId
:   Type: string   
Id of a document for which similarities will be searched.

parameters
:   Type: [MoreLikeThisQuery]()   
A more like this query definition that will be executed.

**Return Value**

Type: TResult[]   
Array of similar documents returned as entities.

## Example

{CODE more_like_this_2@ClientApi\Session\HowTo\MoreLikeThis.cs /}

## Remarks

Do not forget to add following **using** statement which contains necessary extensions:

{CODE more_like_this_3@ClientApi\Session\HowTo\MoreLikeThis.cs /}

#### Related articles
