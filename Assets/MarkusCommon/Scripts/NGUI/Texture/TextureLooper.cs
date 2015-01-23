using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Looping texture by UV animation.
/// </summary>
[RequireComponent(typeof(UITexture))]
public class TextureLooper : MonoBehaviour
{
    public enum Style
    {
        Horizontal,
        Vertical,
        Both
    }

    public Style LoopStyle;

    /// <summary>
    /// Speed of texture moving.
    /// </summary>
    public float Speed;

    /// <summary>
    /// Duration of texture moving.
    /// </summary>
    public float Duration;

    public float CurrentPosition { get; set; }

    private UITexture texture;

    public void Play()
    {
        CurrentPosition = 0f;
        GoTo(0f, 1f, Duration);
    }

    public void Play(float time)
    {
        var from = CurrentPosition;
        var to = CurrentPosition - time * Speed;
        GoTo(from, to, time);
    }

    public void Loop()
    {
        StartCoroutine(DoLoop());
    }

    public void Reset()
    {
        CurrentPosition = 0f;
        GoTo(0f, 0f, 0f);
        StopAllCoroutines();
    }

    /// <summary>
    /// Go to target potion of the whole texture.
    /// </summary>
    /// <param name="from">From value in between range [0, 1]</param>
    /// <param name="to">To value in between range [0, 1]</param>
    /// <param name="duration">Duration from from to to.</param>
    private void GoTo(float from, float to, float duration)
    {
        Logger.Log("Go by time: " + duration + ", from: " + CurrentPosition + ", to: " + to);
        if (LoopStyle == Style.Horizontal)
        {
            iTween.ValueTo(gameObject,
                iTween.Hash("from", from, "to", to, "time", duration, "easetype", iTween.EaseType.linear,
            "onupdate", (Action<object>) (value =>
            {
                CurrentPosition = (float)value;
                texture.uvRect = new Rect(CurrentPosition, texture.uvRect.y, texture.uvRect.width,
                    texture.uvRect.height);
            })));
        }
        else if (LoopStyle == Style.Vertical)
        {
            iTween.ValueTo(gameObject, iTween.Hash("from", from, "to", to, "time", duration, "easetype", iTween.EaseType.linear,
                "onupdate", (Action<object>)(value =>
                {
                    CurrentPosition = (float)value;
                    texture.uvRect = new Rect(texture.uvRect.x, CurrentPosition, texture.uvRect.width,
                        texture.uvRect.height);
                })));
        }
        else if (LoopStyle == Style.Both)
        {
            LoopStyle = Style.Horizontal;
            GoTo(from, to, Duration);
            LoopStyle = Style.Vertical;
            GoTo(from, to, Duration);
            LoopStyle = Style.Both;
        }
    }

    private IEnumerator DoLoop()
    {
        while (true)
        {
            Play();
            yield return new WaitForSeconds(Duration);
        }
    }

    private void Awake()
    {
        texture = GetComponent<UITexture>();
        CurrentPosition = 0f;
    }
}
