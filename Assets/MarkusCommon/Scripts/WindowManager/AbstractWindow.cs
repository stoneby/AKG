using UnityEngine;

/// <summary>
/// Base class for all windows.
/// </summary>
public abstract class AbstractWindow : MonoBehaviour
{
    #region Properties

    public string Path { get; set; }

    #endregion

    #region Interfaces

    public abstract void OnOpen();
    public abstract void OnClose();

    #endregion

    #region Mono

    // Use this for initialization
    protected virtual void Start()
    {
    }

    #endregion
}
