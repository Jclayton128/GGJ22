using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class CSVUtility
{

    /// <summary>
    /// Converts a CSV TextAsset into a 2D list of strings.
    /// </summary>
    public static List<List<string>> ParseCSV(TextAsset csv)
    {
        if (csv == null) return new List<List<string>>();
        var sourceLines = new List<string>(csv.text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
        return ParseLines(sourceLines);
    }

    private static List<List<string>> ParseLines(List<string> sourceLines)
    { 
        var content = new List<List<string>>();
        while (sourceLines.Count > 0)
        {
            var values = GetValues(sourceLines[0]);
            sourceLines.RemoveAt(0);
            if (values == null || values.Length == 0 || string.IsNullOrEmpty(values[0])) continue;
            var row = new List<string>();
            content.Add(row);
            for (int i = 0; i < values.Length; i++)
            {
                row.Add(values[i]);
            }
        }
        return content;
    }

    /// <summary>
    /// Returns the individual comma-separated values in a line.
    /// </summary>
    private static string[] GetValues(string line)
    {
        Regex csvSplit = new Regex("(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)");
        List<string> values = new List<string>();
        foreach (Match match in csvSplit.Matches(line))
        {
            values.Add(UnwrapValue(match.Value.TrimStart(',')));
        }
        return values.ToArray();
    }

    /// <summary>
    /// Returns a "fixed" version of a comma-separated value where escaped newlines
    /// have been converted back into real newlines, and optional surrounding quotes 
    /// have been removed.
    /// </summary>
    private static string UnwrapValue(string value)
    {
        string s = value.Replace("\\n", "\n").Replace("\\r", "\r");
        if (s.StartsWith("\"") && s.EndsWith("\""))
        {
            s = s.Substring(1, s.Length - 2).Replace("\"\"", "\"");
        }
        return s;
    }

}
