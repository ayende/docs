# Querying : How to use intersect?

To return only documents that match **all** provided sub-queries we have introduced `Intersect` extension that enables us to do server-side intersection queries.

## Syntax

{CODE intersect_1@ClientApi\Session\Querying\HowToUseIntersect.cs /}

**Return Value**

Type: IRavenQueryable   
Instance implementing IRavenQueryable interface containing additional query methods and extensions.

## Example

{CODE intersect_2@ClientApi\Session\Querying\HowToUseIntersect.cs /}

#### Related articles

TODO
