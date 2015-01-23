using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Windows mananger including the following types:
/// - Screen (fullscreen)
/// - Popup (simple pop up window)
/// - TabPanel (tab based pop up window)
/// - Face (toppest window)
/// </summary>
/// <remarks>
/// More please refers to WindowGroupType.
/// Keep in mind that the definition of layer, DO NOT ABUSE IT.
/// The general rule is that window covers the same rect of areas should be in one group.
/// </remarks>
public class WindowManager : Singleton<WindowManager>
{
    #region Public Fields

    public AbstractPathLayerMapping Mapping;

    public WindowRootObjectManager WindowRootManager;

    /// <summary>
    /// UI resouces base path.
    /// </summary>
    public const string UIBasePath = "Prefabs/UI";

    #endregion

    #region Private Fields

    /// <summary>
    /// Window map between window group type and list of windows.
    /// </summary>
    private readonly Dictionary<WindowGroupType, List<Window>> windowMap = new Dictionary<WindowGroupType, List<Window>>();

    /// <summary>
    /// Current window map between window group type and current window.
    /// </summary>
    private readonly Dictionary<WindowGroupType, Window> currentWindowMap = new Dictionary<WindowGroupType, Window>();

    #endregion

    #region Public Properties

    public Dictionary<WindowGroupType, Window> CurrentWindowMap
    {
        get { return currentWindowMap; }
    }

    #endregion

    #region Puiblic Methods

    /// <summary>
    /// Get window with specific type.
    /// </summary>
    /// <returns>The window with specific type</returns>
    public T GetWindow<T>() where T : Window
    {
        var window = GetWindow(typeof(T));
        return window as T;
    }

    /// <summary>
    /// Get window by name
    /// </summary>
    /// <param name="windowName">Window type name</param>
    /// <returns>The window with specific type name</returns>
    public Window GetWindow(string windowName)
    {
        return GetWindow(Type.GetType(windowName));
    }

    /// <summary>
    /// Get window by type
    /// </summary>
    /// <param name="type">Window type</param>
    /// <returns>The window with specific type</returns>
    public Window GetWindow(Type type)
    {
        var path = Mapping.TypePathMap[type];
        var windowGroupType = Mapping.PathLayerMap[path];

        if (!windowMap.ContainsKey(windowGroupType))
        {
            windowMap[windowGroupType] = new List<Window>();
        }

        var window = windowMap[windowGroupType].Find(win => win.Path == path);
        if (window == null)
        {
            window = CreateWindow(windowGroupType, path);
            windowMap[windowGroupType].Add(window);
            Logger.Log("Create window with type - " + type + ", groupType - " + windowGroupType + ", path - " + path);
        }
        else
        {
            Logger.Log("Find window with type - " + type + ", groupType - " + windowGroupType + ", path - " + path);
        }
        return window;
    }

    /// <summary>
    /// Check whether contain specific window of type T 
    /// </summary>
    /// <typeparam name="T">Window type</typeparam>
    /// <returns>True if contain window of type T</returns>
    public bool ContainWindow<T>()
    {
        var type = typeof(T);
        return ContainWindow(type);
    }

    /// <summary>
    /// Check whether contain specific window of type T 
    /// </summary>
    /// <param name="type">Window type</param>
    /// <returns></returns>
    public bool ContainWindow(Type type)
    {
        var path = Mapping.TypePathMap[type];
        var windowGroupType = Mapping.PathLayerMap[path];
        return windowMap.ContainsKey(windowGroupType);
    }

    /// <summary>
    /// Show by generic type.
    /// </summary>
    /// <typeparam name="T">Generic window type</typeparam>
    /// <param name="show">Flag indicates if window to show or hide</param>
    /// <returns>Window to show</returns>
    public T Show<T>(bool show) where T : Window
    {
        var type = typeof(T);
        return Show(type, show) as T;
    }

    /// <summary>
    /// Show by window type name.
    /// </summary>
    /// <param name="windowName">Window name</param>
    /// <param name="show">Flag indicates if window to show or hide</param>
    /// <returns>Window to show</returns>
    public Window Show(string windowName, bool show)
    {
        var type = Type.GetType(windowName);
        return Show(type, show);
    }

    /// <summary>
    /// Show by window type.
    /// </summary>
    /// <param name="type">Window type</param>
    /// <param name="show">Flag indicates if window to show or hide</param>
    /// <returns>Window to show</returns>
    public Window Show(Type type, bool show)
    {
        if (!ContainWindow(type) && !show)
        {
            Logger.Log("Window of type: " + type + " is not showing any way, just return null.");
            return null;
        }

        var path = Mapping.TypePathMap[type];
        var windowGroupType = Mapping.PathLayerMap[path];
        var window = GetWindow(type);

        Logger.Log("------ Show Window: " + window.name + ", show: " + show);

        if (show)
        {
            var lastWindow = currentWindowMap.ContainsKey(windowGroupType) ? currentWindowMap[windowGroupType] : null;
            if (lastWindow != null)
            {
                if (lastWindow != window && lastWindow.Active)
                {
                    var groupType = lastWindow.WindowGroup;
                    Logger.Log("Last window hide with type - " + lastWindow.GetType().Name + ", groupType - " + groupType +
                              ", path - " + lastWindow.Path);

                    lastWindow.gameObject.SetActive(false);
                }
                else if (lastWindow == window && lastWindow.Active)
                {
                    Logger.Log("The window is currently showing already." + lastWindow.name);
                    return window;
                }
            }

            currentWindowMap[windowGroupType] = window;
            window.gameObject.SetActive(true);
        }
        else
        {
            window.gameObject.SetActive(false);
        }
        return window;
    }

    /// <summary>
    /// Destroy window
    /// </summary>
    /// <param name="window">Window to destroy</param>
    public void DestroyWindow(Window window)
    {
        var groupType = window.WindowGroup;
        var isCurrentWindow = (currentWindowMap.ContainsKey(groupType) && (currentWindowMap[groupType] == window));
        if (isCurrentWindow)
        {
            Logger.Log("The window you are destroying is the current showing window: " + window.name);
            currentWindowMap.Remove(groupType);
        }

        windowMap[groupType].Remove(window);

        Logger.LogWarning("DestroyWindow: " + window.name);

        Destroy(window.gameObject);
    }

    public void DestroyWindow<T>()
    {
        var type = typeof(T);
        DestroyWindow(type);
    }

    public void DestroyWindow(Type t)
    {
        if (!ContainWindow(t))
        {
            Logger.LogWarning("The window type you are trying to destroy does not exist. window: " + t.Name);
            return;
        }

        var window = GetWindow(t);
        DestroyWindow(window);
    }

    /// <summary>
    /// Show windows by group type.
    /// </summary>
    /// <param name="groupType">Group type</param>
    /// <param name="show">Flag indicates if show or hide</param>
    public void Show(WindowGroupType groupType, bool show)
    {
        if ((int)groupType == Utils.Invalid || !Mapping.LayerPathMap.ContainsKey(groupType))
        {
            Logger.LogError("Could not contain group type - " + groupType +
                           ", please double check with WindowGroupType, that's all we have.");
            return;
        }
        if (!windowMap.ContainsKey(groupType))
        {
            Logger.LogWarning("The window group type - " + groupType +
               ", has already closed, we need not call show window to close them.");
            return;
        }
        windowMap[groupType].ForEach(win =>
        {
            if (win.gameObject.activeSelf != show)
            {
                win.gameObject.SetActive(show);
            }
        });
    }

    /// <summary>
    /// Show all windows.
    /// </summary>
    /// <param name="show">Flag indicates if show or hide</param>
    public void Show(bool show)
    {
        foreach(var map in windowMap)
        {
            map.Value.ForEach(win =>
            {
                if (win.gameObject.activeSelf != show)
                {
                    win.gameObject.SetActive(show);
                }
            });
        }     
    }

    public void DestroyWindows(Window.MemoryManagementType memoryType)
    {
        foreach (var map in windowMap)
        {
            for (var i = map.Value.Count - 1; i >= 0 ; --i)
            {
                var window = map.Value[i];
                if (window.MemeoryType == memoryType)
                {
                    DestroyWindow(window);
                }
            }
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Create window by group type and prefab path
    /// </summary>
    /// <param name="groupType">Window group type</param>
    /// <param name="path">Prefab path</param>
    /// <returns>The window handle</returns>
    private Window CreateWindow(WindowGroupType groupType, string path)
    {
        var root = WindowRootManager.WindowObjectMap[groupType];
        var prefab = Resources.Load<GameObject>(path);
        if (prefab == null)
        {
            Logger.LogError("Could not find window from groupType - " + groupType + ", path - " + path);
            return null;
        }

        // This is importance, Awake / OnEnable should not be called until Show is get called.
        prefab.SetActive(false);
        var child = NGUITools.AddChild(root, prefab);
        var windowName = Utils.PrefabNameToWindow(Utils.GetNameFromPath(path));
        var component = child.GetComponent(windowName) ?? child.AddComponent(windowName);
        var window = component.GetComponent<Window>();
        window.Path = path;
        window.WindowGroup = groupType;
        return window;
    }

    #endregion
}
