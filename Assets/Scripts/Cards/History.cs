using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

/// <summary>
/// Tracks the variables that have been filled in so far.
/// </summary>
public class History
{

    // Keys are ID.variableName:
    private Dictionary<string, string> variables = new Dictionary<string, string>();

    private const int MaxReplacements = 20; // Safeguard to prevent infinite loops.

    // Type names allowed in variable tags:
    private const string FirstNameTypeName = "firstname";
    private const string LastNameTypeName = "lastname";
    private const string JobTypeName = "job";
    private const string ObjectTypeName = "object";
    private const string LocationTypeName = "location";
    private const string NationalityTypeName = "nationality";
    private const string FromListTypeName = "from";

    public string ReplaceVariables(string cardID, string text)
    {
        if (string.IsNullOrEmpty(text) || !text.Contains("{")) return text;
        int endPosition = text.Length - 1;
        int numReplacements = 0; // Sanity check to prevent infinite loops in case of bug.
        while ((endPosition >= 0) && (numReplacements < MaxReplacements))
        {
            numReplacements++;
            int tagPosition = text.LastIndexOf("{", endPosition, System.StringComparison.OrdinalIgnoreCase);
            endPosition = tagPosition - 1;
            text = SubstituteTag(cardID, text, tagPosition);
        }
        return text;
    }

    private string SubstituteTag(string cardID, string text, int tagPosition)
    {
        if (tagPosition < 0) return text;
        Regex regex = new Regex("{.+?(?=})}");
        string firstPart = text.Substring(0, tagPosition);
        string secondPart = text.Substring(tagPosition);
        string secondPartVarReplaced = regex.Replace(secondPart, delegate (Match match)
        {
            return ReplaceVariable(cardID, match.Value);
        });
        return firstPart + secondPartVarReplaced;
    }

    public string ReplaceVariable(string cardID, string variableSpec)
    {
        if (string.IsNullOrEmpty(variableSpec)) return variableSpec;
        string inner = variableSpec.Substring(1, variableSpec.Length - 2); // Remove {}
        string[] fields = inner.Split(':');
        string variable = fields[0];
        if (fields.Length == 1)
        {
            return GetVariableValue(cardID, variable);
        }
        else
        {
            return FillVariable(cardID, fields);
        }
    }

    private string GetVariableValue(string cardID, string variable)
    {
        if (string.IsNullOrEmpty(variable)) return variable;
        string key = variable.Contains(".") ? variable : $"{cardID}.{variable}";
        return variables.ContainsKey(key) ? variables[key] : key;
    }

    private void SetVariableValue(string cardID, string variable, string value)
    {
        string key = $"{cardID}.{variable}";
        variables[key] = value;
    }

    private string FillVariable(string cardID, string[] fields)
    {
        if (fields == null || fields.Length == 0) return string.Empty;
        string variable = fields[0];
        if (fields.Length == 1) return GetVariableValue(cardID, variable);
        string type = fields[1];
        string value = FillVariableOfType(cardID, fields);
        SetVariableValue(cardID, variable, value);
        return value;
    }

    private string FillVariableOfType(string cardID, string[] fields)
    {
        string variable = fields[0];
        string type = fields[1];
        switch (type)
        {
            case FirstNameTypeName: return CardData.Instance.FirstNames.GetRandomString();
            case LastNameTypeName: return CardData.Instance.LastNames.GetRandomString();
            case JobTypeName: return CardData.Instance.Jobs.GetRandomString();
            case ObjectTypeName: return CardData.Instance.Objects.GetRandomString();
            case LocationTypeName: return CardData.Instance.Locations.GetRandomString();
            case NationalityTypeName: return CardData.Instance.Nationalities.GetRandomString();
            case FromListTypeName: return ChooseRandomField(fields);
            default: return variable;
        }
    }

    private string ChooseRandomField(string[] fields)
    {
        // Choose random from fields[2]+:
        return fields[Random.Range(2, fields.Length)];
    }

}
