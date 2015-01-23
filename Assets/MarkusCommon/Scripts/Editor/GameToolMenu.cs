using UnityEditor;
using UnityEngine;

/// <summary>
/// Tool menu shows on unity menu bar.
/// </summary>
public static class GameToolMenu
{
    #region Open

    [MenuItem("Tool/Window Mapping XML Generator", false, 0)]
    static public void OpenWindowConfigurator()
    {
        EditorWindow.GetWindow<WindowMappingXmlGeneratorWindow>(false, "Window Mapping XML Generator", true);
    }

    [MenuItem("Tool/Window Prefab Generator", false, 0)]
    static public void OpenWindowPrefabGenerator()
    {
        EditorWindow.GetWindow<WindowPrefabGeneratorWindow>(false, "Window Prefab Generator", true);
    }

    [MenuItem("Tool/Game Event Generator", false, 0)]
    static public void OpenGameEventGenerator()
    {
        EditorWindow.GetWindow<GameEventGenerator>(false, "Game Event Generator", true);
    }

    #endregion
}
