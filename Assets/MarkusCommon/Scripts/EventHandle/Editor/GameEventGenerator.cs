using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Global game event generator.
/// </summary>
public class GameEventGenerator : EditorWindow
{
    #region Public Fields

    public enum KeyType
    {
        Bool,
        Int,
        Long,
        Float,
        Double,
        String
    }

    #endregion

    #region Private Fields

    private const string TemplateEventName = "TemplateEvent";
    private const string TemplateFilePath = "Assets/Game/Scripts/Template/TemplateEvent.cs";
    private const string GenerateFilePath = "Game/Scripts/Event";
    private TextAsset templateFile;

    private string className;
    private KeyType fieldKey;
    private string fieldValue;

    private readonly List<KeyValuePair<KeyType, string>> fieldPairList = new List<KeyValuePair<KeyType, string>>();

    #endregion

    #region Mono

    private void OnEnable()
    {
        if (templateFile == null)
        {
            templateFile = AssetDatabase.LoadMainAssetAtPath(TemplateFilePath) as TextAsset;
        }
    }

    private void OnDisable()
    {
        templateFile = null;
    }

    private void OnGUI()
    {
        className = EditorGUILayout.TextField("Choose Your Class Name: ", className);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Template: ");

        // Draw template item.
        EditorGUILayout.BeginHorizontal();
        fieldKey = (KeyType)EditorGUILayout.EnumPopup(fieldKey);
        fieldValue = EditorGUILayout.TextField(fieldValue);
        EditorGUILayout.EndHorizontal();

        // Space bar.
        EditorGUILayout.Space();

        if (fieldPairList.Count != 0)
        {
            EditorGUILayout.LabelField("You have added the following fields to your class.");
        }

        // Draw key value pair list.
        fieldPairList.ForEach(pair =>
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(pair.Key.ToString());
            EditorGUILayout.LabelField(pair.Value);
            EditorGUILayout.EndHorizontal();
        });

        // Space bar.
        EditorGUILayout.Space();

        // Draw Add / Remove line button.
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add"))
        {
            if (string.IsNullOrEmpty(fieldValue))
            {
                Logger.Log("Field value should not be null. please double check it out.");
                return;
            }
            fieldPairList.Add(new KeyValuePair<KeyType, string>(fieldKey, fieldValue));

            Reset();
        }

        // Always remove the last item.
        if (GUILayout.Button("Remove"))
        {
            fieldPairList.RemoveAt(fieldPairList.Count - 1);
        }
        GUILayout.EndHorizontal();

        // Draw space bar.
        EditorGUILayout.Space();

        // Draw generate button.
        if (GUILayout.Button("Generate"))
        {
            GenerateFile();
        }
    }

    #endregion

    #region Private Methods

    private void Reset()
    {
        fieldKey = KeyType.Bool;
        fieldValue = string.Empty;
    }

    private void GenerateFile()
    {
        var content = templateFile.text;

        var contentBuilder = new StringBuilder(content);
        // Change class name.
        contentBuilder.Replace(TemplateEventName, className);

        // Append fields.
        var fieldBuilder = new StringBuilder("{\r\n");
        fieldPairList.ForEach(pair =>
        {
            var line = string.Format("    public {0} {1};\r\n", pair.Key.ToString().ToLower(), pair.Value);
            fieldBuilder.Append(line);
        });
        fieldBuilder.Append('}');
        contentBuilder.Replace("{\r\n}", fieldBuilder.ToString());

        // Generate new event file.
        var generateFilePath = string.Format("{0}/{1}/{2}{3}", Application.dataPath, GenerateFilePath, className, Utils.ScriptExtension);
        File.WriteAllText(generateFilePath, contentBuilder.ToString());

        AssetDatabase.Refresh();

        ResetList();
    }

    private void ResetList()
    {
        fieldPairList.Clear();
    }

    #endregion
}
