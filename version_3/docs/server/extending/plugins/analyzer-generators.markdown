# Plugins : Analyzer Generators

To add your custom analyzer, one must implement the `AbstractAnalyzerGenerator` class and provide logic when your custom analyzer should be used.

{CODE plugins_7_0@Server\Extending\Plugins.cs /}

where:   
* **GenerateAnalyzerForIndexing** returns an analyzer that will be used while performing indexing operation.   
* **GenerateAnalyzerForQuerying** returns an analyzer that will be used while performing querying.    

## Example - Using different analyzer for specific index

{CODE plugins_7_1@Server\Extending\Plugins.cs /}

#### Related articles

TODO