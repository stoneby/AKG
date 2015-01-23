using System.Collections;
using UnityEngine;

/// <summary>
/// Looping widget (container) base on game object moving.
/// </summary>
/// <remarks>Move widget in between right to left side crossing base widget.</remarks>
[RequireComponent(typeof(UIWidget))]
public class WidgetLooper : MonoBehaviour
{
    /// <summary>
    /// Speed of moving.
    /// </summary>
    public float Speed;

    /// <summary>
    /// Duration of moving.
    /// </summary>
    public float Duration;

    /// <summary>
    /// Base widget for current widiget to move across by.
    /// </summary>
    public UIWidget BaseWidget;

    private UIWidget widget;
    private Vector3 defaultPosition;
    private Vector3 currentPosition;

    private float MinX
    {
        get { return -(BaseWidget.width + widget.width) / 2.0f; }
    }
    private float MaxX
    {
        get { return (BaseWidget.width + widget.width) / 2.0f; }
    }

    public void Play()
    {
        Play(Duration);
    }

    public void Play(float duration)
    {
        if (currentPosition.x < MinX)
        {
            currentPosition.x = MaxX;
            transform.localPosition = currentPosition;
        }

        if (currentPosition.x > MaxX)
        {
            currentPosition.x = MinX;
            transform.localPosition = currentPosition;
        }

        var distance = new Vector3(duration * Speed * BaseWidget.width, 0, 0);

        // using itween here to make consistent with texture looper, to keep in the same frame. NGUI tween has issue by eating frames.
        iTween.MoveTo(gameObject,
            iTween.Hash("position", (transform.localPosition - distance), "time", Duration, "easetype" ,"linear", "islocal", true));

        currentPosition -= distance;
    }

    public void Reset()
    {
        transform.localPosition = defaultPosition;
        currentPosition = defaultPosition;
        StopAllCoroutines();
    }


    public void Loop()
    {
        StartCoroutine(DoLoop());
    }

    private IEnumerator DoLoop()
    {
        while (true)
        {
            Play(Duration);
            yield return new WaitForSeconds(Duration);
        }
    }

    private void Awake()
    {
        widget = GetComponent<UIWidget>();

        defaultPosition = transform.localPosition;
        currentPosition = defaultPosition;
    }
}
