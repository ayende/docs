# Client API : How to subscribe to replication conflicts?

Replication conflicts, for both documents and attachments, can be tracked by using `ForAllReplicationConflicts` method available in changes API.

## Syntax

{CODE replication_conflicts_changes_1@ClientApi\Changes\HowToSubscribeToReplicationConflictsChanges.cs /}

**Return value**

Type: IObservableWithTask<[ReplicationConflictNotification](../../glossary/client-api/changes/replication-conflict-notification)>   
Observable that allows to add subscribtions to notifications for all replicaton conflicts.

## Example

{CODE replication_conflicts_changes_2@ClientApi\Changes\HowToSubscribeToReplicationConflictsChanges.cs /}

## Automatic document conflict resolution

In RavenDB client you have an opportunity to register [conflict listeners](../../client-api/listeners/what-are-conflict-listeners-and-how-to-work-with-them) which are used to resolve conflicted document. However this can happen only if you get the conflicted document. The ability to subscribe to the replication conflicts gives the client more power. Now if you listen to the conflicts and have any conflict listener registered then the client will automatically resolve the conflict right after the arrival of the notification.

## Remarks

{INFO To get more method overloads, especially the ones supporting delegates, please add [Reactive Extensions](http://nuget.org/packages/Rx-Main) package to your project. /}

#### Related articles

TODO