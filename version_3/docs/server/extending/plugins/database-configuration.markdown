# Plugins : Database configuration

To alter database configuration you can edit the configuration document (more about how it can be done and what configuration options are available can be found [here](../../administration/configuration)), but sometimes it might be better to change configuration programatically e.g. imagine a situation, where you have 100 databases and you want to change one setting in every one of them. This is why the `IAlterConfiguration` interfaces was created.

{CODE plugins_5_0@Server\Extending\Plugins.cs /}

## Example - Disable compression

{CODE plugins_5_1@Server\Extending\Plugins.cs /}

#### Related articles

TODO