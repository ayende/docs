# Client API : Session : How to check if there are any changes on a session?

Single entity can be checked for changes using [HasChanged](../../../client-api/session/how-to/check-if-entity-has-changed) method, but there is also a possibility to check if there are any changes on a session or even what has changed. Both `HasChanges` property and `WhatChanged` method are available in `Advanced` session operations.

## HasChanges

Property indicates if session contains any changes. That is if there are any new, changed or deleted entities.

### Syntax

{CODE what_changed_1@ClientApi\Session\HowTo\WhatChanged.cs /}

### Example

{CODE what_changed_2@ClientApi\Session\HowTo\WhatChanged.cs /}

## WhatChanged

Method returns all changes for each entity stored within session. Including name of the field/property that changed, its old and new value and change type. 

### Syntax

{CODE what_changed_3@ClientApi\Session\HowTo\WhatChanged.cs /}

**Return Value**

Type: IDictionary<string, [DocumentsChanges]()[]>   
Dictionary containing list of changes per document key.

### Example I

{CODE what_changed_4@ClientApi\Session\HowTo\WhatChanged.cs /}

### Example II

{CODE what_changed_5@ClientApi\Session\HowTo\WhatChanged.cs /}

#### Related articles

TODO