using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

/// <summary>
/// Automatically map between path and layer.
/// </summary>
public class AutoPathLayerMapping : AbstractPathLayerMapping
{
    #region Public Fields

    public TextAsset WindowMapText;

    #endregion

    #region Private Fields

    private string absolutePath;

    private Dictionary<string, List<string>> windowMapDict;

    private static string windowMapPath;

    #endregion

    #region AbstractPathLayerMapping

    public override bool Parse()
    {
        PathLayerMap.Clear();
        LayerPathMap.Clear();

        foreach (var pair in windowMapDict)
        {
            var group = pair.Key;
            var prefabList = pair.Value;
            foreach (var prefabPath in prefabList)
            {
                var className = Utils.GetNameFromPath(prefabPath);
                var windowType = Type.GetType(Utils.PrefabNameToWindow(className));

                Logger.LogWarning("Window type - " + windowType + " with name " + className);

                if (windowType == null)
                {
                    Logger.LogError("windowType is null. " + "perfabPath is: " + prefabPath);
                }
                PathTypeMap[prefabPath] = windowType;
                TypePathMap[windowType] = prefabPath;

                var windowGroupType = (WindowGroupType)Enum.Parse(typeof(WindowGroupType), group);
                PathLayerMap[prefabPath] = windowGroupType;
                if (!LayerPathMap.ContainsKey(windowGroupType))
                {
                    LayerPathMap[windowGroupType] = new List<string>();
                }
                LayerPathMap[windowGroupType].Add(prefabPath);
            }
        }

        Display();
        return true;
    }

    #endregion

    private void Display()
    {
        foreach (var pair in PathLayerMap)
        {
            Logger.Log("pair: key-" + pair.Key + ", value-" + pair.Value);
        }

        foreach (var pair in LayerPathMap)
        {
            foreach (var value in pair.Value)
            {
                Logger.Log("pair: key-" + pair.Key + ", value-" + value);
            }
        }

        foreach (var pair in PathTypeMap)
        {
            Logger.Log("pair: key-" + pair.Key + ", value-" + pair.Value);
        }

        foreach (var pair in TypePathMap)
        {
            Logger.Log("pair: key-" + pair.Key + ", value-" + pair.Value);
        }
    }

    /// <summary>
    /// Write window map to xml
    /// </summary>
    /// <param name="prefabDict">Data structure from memory</param>
    /// <param name="filePath">File path to save</param>
    public static void WriteWindowMapToXml(Dictionary<string, List<string>> prefabDict, string filePath)
    {
        var doc = new XmlDocument();
        var root = doc.CreateElement("Root");
        doc.AppendChild(root);
        foreach (var pair in prefabDict)
        {
            var element = doc.CreateElement("Group");
            element.SetAttribute("name", pair.Key);
            foreach (var path in pair.Value)
            {
                var subElement = doc.CreateElement("Path");
                subElement.InnerText = path;
                element.AppendChild(subElement);
            }
            root.AppendChild(element);
        }
        doc.Save(filePath);

        Logger.Log("Save window map file to " + filePath);
    }

    /// <summary>
    /// Read window map from xml.
    /// </summary>
    /// <param name="text">Context from xml</param>
    /// <returns>Data structure</returns>
    public static Dictionary<string, List<string>> ReadWindowMapFromXml(string text)
    {
        var dict = new Dictionary<string, List<string>>();
        var doc = new XmlDocument();
        doc.LoadXml(text);
        var root = doc.DocumentElement;
        foreach (XmlElement element in root.ChildNodes)
        {
            var name = element.Attributes[0].Value;
            dict[name] = new List<string>();
            foreach (XmlElement subElement in element.ChildNodes)
            {
                dict[name].Add(subElement.InnerText);
            }
        }
        return dict;
    }

    #region Mono

    void Awake()
    {
        PathLayerMap = new Dictionary<string, WindowGroupType>();
        LayerPathMap = new Dictionary<WindowGroupType, List<string>>();
        TypePathMap = new Dictionary<Type, string>();
        PathTypeMap = new Dictionary<string, Type>();

        windowMapDict = ReadWindowMapFromXml(WindowMapText.text);
        Parse();
    }

    #endregion
}
