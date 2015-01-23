using System;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Utilitity class used for helper functions and constances.
/// </summary>
public class Utils
{
    /// <summary>
    /// Invalid value.
    /// </summary>
    public const int Invalid = -1;

    /// <summary>
    /// Prefab extension.
    /// </summary>
    public const string PrefabExtension = ".prefab";

    /// <summary>
    /// Script extension.
    /// </summary>
    public const string ScriptExtension = ".cs";

    /// <summary>
    /// UI resouces base path.
    /// </summary>
    public const string UIBasePath = "Prefabs/UI";

    #region Window Manager Methods

    /// <summary>
    /// Get file or folder name from a path
    /// </summary>
    /// <param name="path">Path</param>
    /// <returns>File or folder name</returns>
    public static string GetNameFromPath(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            Logger.LogError("path should not be null or empty.");
            return string.Empty;
        }

        if (path[path.Length - 1] == '/')
        {
            path.Remove(path.Length - 1);
        }
        return path.Substring(path.LastIndexOf('/') + 1);
    }

	/// <summary>
	/// Window script name to window prefab name.
	/// </summary>
	/// <param name="windowName">Window script name</param>
	/// <returns>Prefab name</returns>
	/// <remarks>
	/// For example, BattleWindow.cs to Battle.prefab.
	/// </remarks>
	public static string WindowNameToPrefab(string windowName)
	{
		return windowName.Replace("Window", string.Empty);
	}

	/// <summary>
	/// Window prefab name to window script name.
	/// </summary>
	/// <param name="prefabName">Prefab name</param>
	/// <returns>Window script name</returns>
	/// <remarks>
	/// For example, Battle.prefab to BattleWindow.cs.
	/// </remarks>
	public static string PrefabNameToWindow(string prefabName)
	{
		return string.Format("{0}Window", prefabName);
	}

    #endregion

    #region Rotate

    /// <summary>
    /// Get rotation in between source and target.
    /// </summary>
    /// <param name="source">Source position</param>
    /// <param name="target">Target position</param>
    /// <returns>Rotation quaternion</returns>
    public static Quaternion GetRotation(Vector3 source, Vector3 target)
    {
        var delta = target - source;
        var angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, angle);
    }

    /// <summary>
    /// Get rotation in between source and target.
    /// </summary>
    /// <param name="source">Source position</param>
    /// <param name="target">Target position</param>
    /// <returns>Rotation quaternion</returns>
    public static Quaternion GetRotation(Vector2 source, Vector2 target)
    {
        return GetRotation(new Vector3(source.x, source.y, 0), new Vector3(target.x, target.y, 0));
    }

    #endregion

    #region MyPoolManager Helper
    public static void AddChild(GameObject sender, GameObject childObject)
    {
        var t = childObject.transform;
        t.parent = sender.transform;
        t.localPosition = Vector3.zero;
        t.localRotation = Quaternion.identity;
        t.localScale = Vector3.one;
        childObject.SetActive(true);
    }

    #endregion

    /// <summary>
    /// Find the child transform with special name. 
    /// </summary>
    /// <param name="parent">The parent tranfrom of the child which will be found.</param>
    /// <param name="objName">The name of the child transfrom.</param>
    /// <returns>The transfrom to be found.</returns>
    public static Transform FindChild(Transform parent, string objName)
    {
        if (parent.name == objName)
        {
            return parent;
        }
        return (from Transform item in parent select FindChild(item, objName)).FirstOrDefault(child => null != child);
    }

    public static void MoveToParent(Transform parent, Transform instance)
    {
        instance.parent = parent.transform;
        instance.localPosition = Vector3.zero;
        instance.localRotation = Quaternion.identity;
        instance.localScale = Vector3.one;
        instance.gameObject.layer = parent.gameObject.layer;
    }

    // Convert from screen-space coordinates to world-space coordinates on the Z = 0 plane
    public static Vector3 GetWorldPos(Camera cam, Vector2 screenPos)
    {
        Ray ray = cam.ScreenPointToRay(screenPos);

        // we solve for intersection with z = 0 plane
        float t = -ray.origin.z / ray.direction.z;

        return ray.GetPoint(t);
    }

    public static bool IsSameDay(DateTime datetime1, DateTime datetime2)
    {
        return datetime1.Year == datetime2.Year
            && datetime1.Month == datetime2.Month
            && datetime1.Day == datetime2.Day;
    }

    public static string ConvertTimeSpanToString(TimeSpan timeRemain)
    {
        return string.Format("{0:D2}", (int)timeRemain.TotalHours) + ":" +
               string.Format("{0:D2}", timeRemain.Minutes) + ":" +
               string.Format("{0:D2}", timeRemain.Seconds);
    }

    public static string SubstringWithinByteLimit(string text, int byteLimit, string cutSymbol)
    {
        var byteCount = 0;
        var buffer = new char[1];
        for (var i = 0; i < text.Length; i++)
        {
            buffer[0] = text[i];
            byteCount += Encoding.Default.GetByteCount(buffer);
            if (byteCount > byteLimit)
            {
                return text.Substring(0, i) + cutSymbol;
            }
        }
        return text;
    }

    public static string ConvertToAssetBundleName(string resName)
    {
        return resName.Replace('/', '.');
    }

    public static string ConvertToResoucesPath(string resName)
    {
        return resName.Replace('.', '/');
    }
}
