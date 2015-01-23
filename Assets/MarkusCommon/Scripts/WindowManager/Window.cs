using UnityEngine;

/// <summary>
/// Window which will be attached to each window prefab object.
/// - OnOpen will be sent when window is opend.
/// - OnClose will be sent when window is closed.
/// </summary>
public abstract class Window : MonoBehaviour
{
    public enum MemoryManagementType
    {
        Ignore,
        Always,
        UI,
        Battle,
        Login,
    }

    #region Fields

    /// <summary>
    /// Prefab path.
    /// </summary>
    public string Path;

    /// <summary>
    /// Window group that this window belongs to.
    /// </summary>
    public WindowGroupType WindowGroup;

    /// <summary>
    /// Memory management type which is used by MemoryStrategy.
    /// </summary>
    public MemoryManagementType MemeoryType;

    #endregion

    #region Public Properties

    /// <summary>
    /// Flag indicates if the window is active (show) or not.
    /// </summary>
    public bool Active { get { return gameObject.activeSelf; } }

    #endregion

    #region Const

    /// <summary>
    /// OnOpen method.
    /// </summary>
    private const string OnOpenMethod = "OnOpen";

    /// <summary>
    /// OnClose method.
    /// </summary>
    private const string OnCloseMethod = "OnClose";

    #endregion

    #region Template Methods

    public abstract void OnEnter();

    public abstract void OnExit();

    #endregion

    #region Object

    public override string ToString()
    {
        return string.Format("Window: name-{0}, path-{1}, active-{2}", name, Path, Active);
    }

    #endregion

    #region Mono

    protected virtual void OnEnable()
    {
        OnEnter();
    }

    protected virtual void OnDisable()
    {
        OnExit();
    }

    #endregion
}
