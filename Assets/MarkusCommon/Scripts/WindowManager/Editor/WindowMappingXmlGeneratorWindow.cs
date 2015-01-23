using UnityEditor;
using UnityEngine;

/// <summary>
/// Window configurator
/// </summary>
/// <remarks>
/// Write essential informations to xml, including:
/// - Window path and layer configuration.
/// </remarks>
public class WindowMappingXmlGeneratorWindow : EditorWindow
{
    #region Private Fields

    private string absolutePath;
    private const string WindowMapPath = "Game/Resources/Config/WindowMap.xml";

    private readonly WindnowMappingXmlGenerator xmlGenerator = new WindnowMappingXmlGenerator();

    #endregion

    #region Mono

    void OnGUI()
    {
        GUILayout.TextArea(
            "UserManual: This tool to generator window path layer mapping to path - " + WindowMapPath,
            "Label");

        if (GUILayout.Button("Generate"))
        {
            xmlGenerator.GenerateMappingFile();
            AssetDatabase.Refresh();
        }
    }

    #endregion
}
