# Client API : Session : How to check if entity has changed?

To check if specific entity differs from the one downloaded from server `HasChanged` method from `Advanced` session operations has been introduced.

## Syntax

{CODE has_changed_1@ClientApi\Session\HowTo\HasChanged.cs /}

**Parameters**

entity
:   Type: object   
Instance of entity for which changes will be checked.

**Return Value**

Type: bool   
Indicated if given entity has changed.

## Example

{CODE has_changed_2@ClientApi\Session\HowTo\HasChanged.cs /}

#### Related articles
