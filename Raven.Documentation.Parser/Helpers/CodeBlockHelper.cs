﻿using System.Linq;

namespace Raven.Documentation.Parser.Helpers
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Text;
	using System.Text.RegularExpressions;

	using Raven.Documentation.Parser.Data;

	public class CodeBlockHelper
	{
		private static readonly Regex CodeWithoutLanguageFinder = new Regex(@"{CODE\s+(.+)/}", RegexOptions.Compiled);

		private static readonly Regex CodeWithLanguageFinder = new Regex(@"{CODE:(.+?)\s+(.+)/}", RegexOptions.Compiled);

		private static readonly Regex CodeBlockFinder = new Regex(@"{CODE-BLOCK:(.+?)}(.*?){CODE-BLOCK/}", RegexOptions.Compiled | RegexOptions.Singleline);

		private static readonly Regex CodeTabsFinder = new Regex(@"{CODE-TABS}(.*?){CODE-TABS/}", RegexOptions.Compiled | RegexOptions.Singleline);

		private static readonly Regex CodeTabFinder = new Regex(@"{CODE-TAB:(.+?)\s+(.+)/}", RegexOptions.Compiled);

		private static readonly Regex CodeTabBlockFinder = new Regex(@"{CODE-TAB-BLOCK:(.+?)}(.*?){CODE-TAB-BLOCK/}", RegexOptions.Compiled | RegexOptions.Singleline);

		private static readonly Regex FirstLineSpacesFinder = new Regex(@"^(\s|\t)+", RegexOptions.Compiled);

		public static string GenerateCodeBlocks(string content, string documentationVersion, ParserOptions options)
		{
			content = CodeTabsFinder.Replace(content, match => GenerateCodeTabsBlock(match.Groups[1].Value.Trim(), documentationVersion, options));
			content = CodeWithLanguageFinder.Replace(content, match => GenerateCodeBlockFromFile(match.Groups[1].Value.Trim(), match.Groups[2].Value.Trim(), documentationVersion, options));
			content = CodeWithoutLanguageFinder.Replace(content, match => GenerateCodeBlockFromFile("csharp", match.Groups[1].Value.Trim(), documentationVersion, options));
			content = CodeBlockFinder.Replace(content, match => GenerateCodeBlock(match.Groups[1].Value.Trim(), match.Groups[2].Value.Trim()));

			return content;
		}

		private static string GenerateCodeBlock(string languageAsString, string content)
		{
			var language = (CodeBlockLanguage)Enum.Parse(typeof(CodeBlockLanguage), languageAsString, true);

			content = NormalizeContent(content);

			var builder = new StringBuilder();
			builder.AppendLine(string.Format("<pre class='prettyprint {0}'>", ConvertLanguageToCssClass(language)));
			builder.AppendLine(content);
			builder.AppendLine("</pre>");
			return builder.ToString();
		}

		private static string GenerateCodeBlockFromFile(string languageAsString, string value, string documentationVersion, ParserOptions options)
		{
			var language = (Language)Enum.Parse(typeof(Language), languageAsString, true);
			var samplesDirectory = options.GetPathToDocumentationSamplesDirectory(language, documentationVersion);

			var values = value.Split('@');
			var section = values[0];
			var file = values[1];

			var builder = new StringBuilder();
			builder.AppendLine(string.Format("<pre class='prettyprint {0}'>", ConvertLanguageToCssClass(language)));
			switch (language)
			{
				case Language.Csharp:
					builder.AppendLine(ExtractSectionFromCsharpFile(section, Path.Combine(samplesDirectory, file)));
					break;
				case Language.Java:
					builder.AppendLine(ExtractSectionFromJavaFile(section, Path.Combine(samplesDirectory, file)));
					break;
				default:
					throw new NotSupportedException(language.ToString());
			}
			builder.AppendLine("</pre>");

			return builder.ToString();
		}

		private static string GenerateCodeTabsBlock(string content, string documentationVersion, ParserOptions options)
		{
			var tabs = new List<CodeTab>();
			var matches = CodeTabFinder.Matches(content);
			foreach (Match match in matches)
				tabs.Add(GenerateCodeTabFromFile(match.Groups[1].Value.Trim(), match.Groups[2].Value.Trim(), documentationVersion, options));

			matches = CodeTabBlockFinder.Matches(content);
			foreach (Match match in matches)
				tabs.Add(GenerateCodeTab(match.Groups[1].Value.Trim(), match.Groups[2].Value.Trim()));

			var builder = new StringBuilder();
			builder.AppendLine("<div class='code-tabs'>");
			builder.AppendLine("<ul class='nav nav-tabs'>");
			foreach (var tab in tabs)
				builder.AppendLine(string.Format("<li class='code-tab'><a href='#{0}' data-toggle='tab'>{1}</a></li>", tab.Id, ConvertLanguageToDisplayName(tab.Language)));
			builder.AppendLine("</ul>");

			builder.AppendLine("<div class='tab-content'>");
			foreach (var tab in tabs)
			{
				builder.AppendLine(string.Format("<div class='tab-pane code-tab' id='{0}'>", tab.Id));
				builder.AppendLine(string.Format("<pre class='prettyprint {0}'>", ConvertLanguageToCssClass(tab.Language)));
				builder.AppendLine(tab.Content);
				builder.AppendLine("</pre>");
				builder.AppendLine("</div>");
			}
			builder.AppendLine("</div>");
			builder.AppendLine("</div>");

			return builder.ToString();
		}

		private static CodeTab GenerateCodeTab(string languageAsString, string content)
		{
			var language = (Language)Enum.Parse(typeof(Language), languageAsString, true);

			content = content
				.Replace("<", "&lt;")
				.Replace(">", "&gt;");

			return new CodeTab { Content = content, Language = language, Id = Guid.NewGuid().ToString("N") };
		}

		private static CodeTab GenerateCodeTabFromFile(string languageAsString, string value, string documentationVersion, ParserOptions options)
		{
			var language = (Language)Enum.Parse(typeof(Language), languageAsString, true);
			var samplesDirectory = options.GetPathToDocumentationSamplesDirectory(language, documentationVersion);

			var values = value.Split('@');
			var section = values[0];
			var file = values[1];

			string content;
			switch (language)
			{
				case Language.Csharp:
					content = ExtractSectionFromCsharpFile(section, Path.Combine(samplesDirectory, file));
					break;
				case Language.Java:
					content = ExtractSectionFromJavaFile(section, Path.Combine(samplesDirectory, file));
					break;
				default:
					throw new NotSupportedException(language.ToString());
			}

			return new CodeTab { Content = content, Language = language, Id = Guid.NewGuid().ToString("N") };
		}

		private static string ExtractSectionFromJavaFile(string section, string filePath)
		{
			if (File.Exists(filePath) == false)
				throw new FileNotFoundException(string.Format("File '{0}' does not exist.", filePath), filePath);

			var content = File.ReadAllText(filePath);
			var startText = string.Format("//region {0}", section);
			var indexOfStart = content.IndexOf(startText, StringComparison.OrdinalIgnoreCase);
			if (indexOfStart == -1)
				throw new InvalidOperationException(string.Format("Section '{0}' not found in '{1}'.", section, filePath));

			var start = content.IndexOf(startText, StringComparison.OrdinalIgnoreCase) + startText.Length;
			var end = content.IndexOf("//endregion", start, StringComparison.OrdinalIgnoreCase);
			var sectionContent = content.Substring(start, end - start);
			if (sectionContent.EndsWith("//"))
				sectionContent = sectionContent.TrimEnd(new[] { '/' });

			return NormalizeContent(sectionContent);
		}

		private static string ExtractSectionFromCsharpFile(string section, string filePath)
		{
			if (File.Exists(filePath) == false)
				throw new FileNotFoundException(string.Format("File '{0}' does not exist.", filePath), filePath);

			var content = File.ReadAllText(filePath);
			var startText = string.Format("#region {0}", section);
			var indexOfStart = content.IndexOf(startText, StringComparison.OrdinalIgnoreCase);
			if (indexOfStart == -1)
				throw new InvalidOperationException(string.Format("Section '{0}' not found in '{1}'.", section, filePath));

			var start = content.IndexOf(startText, StringComparison.OrdinalIgnoreCase) + startText.Length;
			var end = content.IndexOf("#endregion", start, StringComparison.OrdinalIgnoreCase);
			var sectionContent = content.Substring(start, end - start);
			if (sectionContent.EndsWith("//"))
				sectionContent = sectionContent.TrimEnd(new[] { '/' });

			return NormalizeContent(sectionContent);
		}

		private static string NormalizeContent(string content)
		{
			content = content
				.Replace("<", "&lt;")
				.Replace(">", "&gt;");

			content = content
				.TrimEnd('\t')
				.TrimEnd(Environment.NewLine.ToCharArray())
				.TrimStart(Environment.NewLine.ToCharArray());

			var line = content.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
			var firstLineSpaces = GetFirstLineSpaces(line.FirstOrDefault());
			var firstLineSpacesLength = firstLineSpaces.Length;
			var formattedLines = line
				.Select(l => string.Format("    {0}", l.Substring(l.Length < firstLineSpacesLength ? 0 : firstLineSpacesLength)));

			return string.Join(Environment.NewLine, formattedLines);
		}

		private static string GetFirstLineSpaces(string firstLine)
		{
			if (firstLine == null)
				return string.Empty;

			var match = FirstLineSpacesFinder.Match(firstLine);
			if (match.Success)
			{
				return firstLine.Substring(0, match.Length);
			}

			return string.Empty;
		}

		private static string ConvertLanguageToCssClass(Language language)
		{
			switch (language)
			{
				case Language.Csharp:
					return "lang-cs";
				case Language.Java:
					return "lang-java";
				case Language.Http:
					return "lang-js";
				default:
					throw new NotSupportedException(language.ToString());
			}
		}

		private static object ConvertLanguageToCssClass(CodeBlockLanguage language)
		{
			switch (language)
			{
				case CodeBlockLanguage.Csharp:
					return "lang-cs";
				case CodeBlockLanguage.Java:
					return "lang-java";
				case CodeBlockLanguage.Http:
					return "lang-js";
				case CodeBlockLanguage.Json:
					return "lang-json";
				case CodeBlockLanguage.Plain:
					return string.Empty;
				case CodeBlockLanguage.Xml:
					return "lang-xml";
				default:
					throw new NotSupportedException(language.ToString());
			}
		}

		private static string ConvertLanguageToDisplayName(Language language)
		{
			switch (language)
			{
				case Language.Csharp:
					return "C#";
				case Language.Java:
					return "Java";
				case Language.Http:
					return "HTTP";
				default:
					throw new NotSupportedException(language.ToString());
			}
		}
	}
}