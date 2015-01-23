
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

/// <summary>
/// Window configurator
/// </summary>
/// <remarks>
/// Write essential informations to xml, including:
/// - Window path and layer configuration.
/// </remarks>
public class WindnowMappingXmlGenerator
{
    #region Private Fields

    private readonly string absolutePath = string.Format("{0}/Game/Resources/{1}", Application.dataPath, WindowManager.UIBasePath);
    private List<string> folderNameList = new List<string>();

    private readonly Dictionary<string, List<string>> prefabDict = new Dictionary<string, List<string>>();

    private const string WindowMapPath = "Game/Resources/Config/WindowMap.xml";

    #endregion

    #region Private Methods

    private void CheckValidate()
    {
        if (!Directory.Exists(absolutePath))
        {
            Directory.CreateDirectory(absolutePath);
        }

        folderNameList = Enum.GetNames(typeof (WindowGroupType)).ToList();
        foreach (var folder in folderNameList)
        {
            var folderPath = Path.Combine(absolutePath, folder);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }
    }

    #endregion

    #region Public Methods

    public bool GenerateMappingFile()
    {
        CheckValidate();

        prefabDict.Clear();
        foreach (var folder in folderNameList)
        {
            prefabDict[folder] = new List<string>();
            var path = string.Format("{0}/{1}", absolutePath, folder);
            var prefabList =
                Directory.GetFiles(path)
                    .Select(file => new FileInfo(file))
                    .Where(fileInfor => fileInfor.Extension.Equals(Utils.PrefabExtension))
                    .Select(fileInfor => fileInfor.Name.Remove(fileInfor.Name.IndexOf(fileInfor.Extension, StringComparison.Ordinal)))
                    .ToList();
            foreach (var prefabPath in prefabList.Select(prefabName => string.Format("{0}/{1}/{2}", WindowManager.UIBasePath, folder, prefabName)))
            {
                prefabDict[folder].Add(prefabPath);
            }
        }

        var filePath = Path.Combine(Application.dataPath, WindowMapPath);
        var basePath = new FileInfo(filePath).DirectoryName;
        Directory.CreateDirectory(basePath);
        AutoPathLayerMapping.WriteWindowMapToXml(prefabDict, filePath);

        return true;
    }

    #endregion
}
