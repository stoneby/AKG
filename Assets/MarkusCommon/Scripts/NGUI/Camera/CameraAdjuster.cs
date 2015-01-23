using UnityEngine;

/// <summary>
/// NGUI default use height to adapt. To meet our requirement, we need use width to adapt when device aspect
/// is less then standard aspect. The texture is always 16/9, we have to adjust to any kinds of monitors.
/// </summary>
[RequireComponent(typeof(UICamera))]
public class CameraAdjuster : MonoBehaviour
{
    #region Public Fields

    /// <summary>
    /// The standard screen width.
    /// </summary>
    public static float StandardWidth = 1280;

    /// <summary>
    /// The standard screen height.
    /// </summary>
    public static float StandardHeight = 720;

    public static float CameraScale = 1f;

    #endregion

    #region Private Fields

    private float deviceWidth;
    private float deviceHeight;

    #endregion

    #region Consts

    private const float ScreenRatio = 16/9f;

    #endregion

    #region Mono

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    private void Awake()
    {
        deviceWidth = Screen.width;
        deviceHeight = Screen.height;
        Adjust();
    }

    /// <summary>
    /// When device aspect is less then standard aspect, adjust the orthographic size of the camera,
    /// in order to make sure the ui in adaptive under different resolution ratio.
    /// </summary>
    private void Adjust()
    {
        var standardAspect = StandardWidth / StandardHeight;
        var deviceAspect = deviceWidth / deviceHeight;
        if(deviceAspect < standardAspect)
        {
            camera.orthographicSize = standardAspect / deviceAspect;
        }

        CameraScale = deviceWidth / StandardWidth;

        Logger.Log("Scale " + CameraScale);
    }

    #endregion
}
