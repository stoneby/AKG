using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Window prefab generator tool.
/// </summary>
public class WindowPrefabGeneratorWindow : EditorWindow
{
    #region Private Fields

    private TextAsset template;

    private string prefabName;

    private WindowGroupType windowGroup = WindowGroupType.Screen;
    
    private GameObject prefabGameObject;

    private string userManual =
        "User manual: Please fill in prefab name and choose a window group, then click generate button.";

    private string log = string.Empty;

    private static readonly string UIBasePathByAsset = string.Format("Assets/Game/Resources/{0}", WindowManager.UIBasePath);
    private static readonly string UIBasePathBySystem = string.Format("{0}/Game/Resources/{1}", Application.dataPath, WindowManager.UIBasePath);
    private static readonly string UIScriptBasePath = string.Format("{0}/Game/Scripts/UI", Application.dataPath);

    private const string TemplateBasePath = "Assets/MarkusCommon/WindowManager/Template";
    private const string TemplateWindow = "TemplateWindow";

    private string templateName;
    private bool buttonPressed;
    private GameObject generatedPrefab;

    private WindnowMappingXmlGenerator xmlGenerator = new WindnowMappingXmlGenerator();

    #endregion

    #region Private Methods

    private void GeneratePrefab()
    {
        if (NullCheck())
        {
            return;
        }

        // create empty prefab need a unique file name.
        if (PrefabExistedCheck())
        {
            return;
        }

        GenerateWindowScript();

        CreatePrefab();
    }

    private bool PrefabExistedCheck()
    {
        var prefabPathEnum =
            Directory.GetFiles(UIBasePathBySystem, "*.prefab", SearchOption.AllDirectories)
                .Where(file => file.Contains(prefabName));
        var prefabPaths = prefabPathEnum as string[] ?? prefabPathEnum.ToArray();
        var prefabExisted = prefabPaths.Any();
        if (prefabExisted)
        {
            var prefabPath = prefabPaths.ElementAt(0);
            log = string.Format("Prefab with name - {0} already existed in path - {1}, please try another name.",
                prefabName, prefabPath);
            return true;
        }
        return false;
    }

    private bool NullCheck()
    {
        if (string.IsNullOrEmpty(prefabName))
        {
            log = string.Format("Prefab name should not be null.");
            return true;
        }

        if (prefabGameObject == null)
        {
            log =
                string.Format(
                    "Game object should not be null, please select the game object you want to attach to prefab.");
            return true;
        }
        return false;
    }

    private void GenerateWindowScript()
    {
        var templateWindowPath = string.Format("{0}/{1}/{2}{3}", Application.dataPath,
            TemplateBasePath.Replace("Assets/", ""), template.name, Utils.ScriptExtension);
        var specificWindowName = Utils.PrefabNameToWindow(prefabName);
        var generatedFilePath = string.Format("{0}/{1}", UIScriptBasePath, windowGroup);
        var generatedFile = string.Format("{0}/{1}{2}", generatedFilePath, specificWindowName, Utils.ScriptExtension);

        Logger.Log("template path: " + templateWindowPath);
        Logger.Log("generated file path: " + generatedFilePath);

        var fullText = File.ReadAllText(templateWindowPath).Replace(template.name, specificWindowName);
        if (!Directory.Exists(generatedFilePath))
        {
            Directory.CreateDirectory(generatedFilePath);
        }
        File.WriteAllText(generatedFile, fullText);

        AssetDatabase.Refresh();
    }

    private void CreatePrefab()
    {
        var prefabPath = string.Format("{0}/{1}", UIBasePathByAsset, windowGroup);
        if (!Directory.Exists(prefabPath))
        {
            Directory.CreateDirectory(prefabPath);
        }

        var prefabPathFile = string.Format("{0}/{1}{2}", prefabPath, prefabName, Utils.PrefabExtension);
        // create prefab.
        generatedPrefab = PrefabUtility.CreatePrefab(prefabPathFile, prefabGameObject, ReplacePrefabOptions.ReplaceNameBased);

        log =
        string.Format("Generate prefab - {0}, to path - {1}, with game object in scene - {2}", prefabName,
            string.Format("{0}/{1}", WindowManager.UIBasePath, prefabName), prefabGameObject.name);
    }

    #endregion

    #region Mono

    void OnGUI()
    {
        userManual = GUILayout.TextArea(userManual, "Label");
        
        EditorGUILayout.Space();

        GUI.enabled = false;
        template = EditorGUILayout.ObjectField("Template: ", template, typeof(TextAsset), false) as TextAsset;
        GUI.enabled = true;

        prefabName = EditorGUILayout.TextField("Name: ", prefabName);
        windowGroup = (WindowGroupType)EditorGUILayout.EnumPopup("Window Group: ", windowGroup);
        var templatePath = string.Format("{0}/{1}{2}", TemplateBasePath, TemplateWindow, Utils.ScriptExtension);

        template = AssetDatabase.LoadMainAssetAtPath(templatePath) as TextAsset;

        const bool allowSceneObjects = true;
        prefabGameObject = EditorGUILayout.ObjectField("Game Object: ", prefabGameObject, typeof(GameObject), allowSceneObjects) as GameObject;

        if (GUILayout.Button("Generator Prefab"))
        {
            buttonPressed = true;

            GeneratePrefab();

            xmlGenerator.GenerateMappingFile();
        }

        if (buttonPressed && !EditorApplication.isCompiling)
        {
            buttonPressed = false;

            var specificWindowName = string.Format("{0}Window", prefabName);
            generatedPrefab.AddComponent(specificWindowName);
        }

        EditorGUILayout.Space();

        log = GUILayout.TextArea(log, "Label");
    }

    #endregion
}
