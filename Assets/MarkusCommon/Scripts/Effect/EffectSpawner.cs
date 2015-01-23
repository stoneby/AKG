using UnityEngine;

/// <summary>
/// Single effect spawner, used to spawner a effect (including particles and aniamtions) 
/// when Play() get called, and destroy when Stop() get called.
/// </summary>
public class EffectSpawner : MonoBehaviour
{
    #region Public Fields

    /// <summary>
    /// Effect prefab to spawn.
    /// </summary>
    public GameObject EffectPrefab;

    /// <summary>
    /// Effect render queue.
    /// </summary>
    public int RenderQueue = global::RenderQueue.FaceEffect;

    #endregion

    #region Private Fields

    /// <summary>
    /// Effect controller.
    /// </summary>
    private EffectController effectController;

    #endregion

    #region Public Methods

    /// <summary>
    /// Duration.
    /// </summary>
    public float Duration
    {
        get { return effectController.Duration; }
    }

    /// <summary>
    /// Play effect.
    /// </summary>
    public void Play()
    {
        Initialize();

        if (effectController == null)
        {
            return;
        }

        effectController.gameObject.SetActive(true);
        effectController.Play(false);
    }

    /// <summary>
    /// Stop effect.
    /// </summary>
    public void Stop()
    {
        if (effectController == null)
        {
            return;
        }

        effectController.Stop();
        effectController.gameObject.SetActive(false);

        Cleanup();
    }

    #endregion

    #region Private Methods

    private void Initialize()
    {
        if (effectController != null)
        {
            return;
        }

        var warningClone = Instantiate(EffectPrefab) as GameObject;
        warningClone.transform.position = transform.position;
        warningClone.transform.parent = transform;
        warningClone.transform.localScale = Vector3.one;

        var renderQueue = warningClone.GetComponent<SetRenderQueue>() ?? warningClone.AddComponent<SetRenderQueue>();
        renderQueue.RenderQueue = RenderQueue;

        effectController = warningClone.GetComponent<EffectController>() ?? warningClone.AddComponent<EffectController>();
        effectController.gameObject.SetActive(false);
    }

    private void Cleanup()
    {
        if (effectController == null)
        {
            return;
        }
        Destroy(effectController.gameObject);
    }

    #endregion

    #region Monos

    private void Awake()
    {
        Initialize();
    }

    #endregion

}
