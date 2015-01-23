using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Window game object hiarachy manager, which game object layer under NguiCamera object root.
/// - Screen.
/// - Popup.
/// - TabPanel.
/// - Face.
/// </summary>
public class WindowRootObjectManager : MonoBehaviour
{
    #region Public Fields

    /// <summary>
    /// Path layer mapping.
    /// </summary>
    public AbstractPathLayerMapping Mapping;

    /// <summary>
    /// Parent game object of all windows.
    /// </summary>
    public GameObject Root;

    public float FadeInDuration;
    public float FadeOutDuration;

    public delegate void FadeComplete(Window window);

    #endregion

    #region Public Properties

    /// <summary>
    /// Map from window group type to window group game object root.
    /// </summary>
    public Dictionary<WindowGroupType, GameObject> WindowObjectMap = new Dictionary<WindowGroupType, GameObject>();

    #endregion

    #region Mono

    void Start()
    {
        var trans = Root.transform;

        foreach (var groupType in Mapping.LayerPathMap.Keys)
        {
            var windowGroup = groupType.ToString();
            var layerTrans = trans.Find(windowGroup);
            if (layerTrans == null)
            {
                var layerObject = NGUITools.AddChild(trans.gameObject);
                layerTrans = layerObject.transform;

                Logger.Log("Adding game object to root - " + trans.name);
            }

            layerTrans.name = windowGroup;

            Logger.Log("Game object is - " + layerTrans.name + ", with window group - " + windowGroup);

            WindowObjectMap[groupType] = layerTrans.gameObject;
        }
    }

    #endregion
}
