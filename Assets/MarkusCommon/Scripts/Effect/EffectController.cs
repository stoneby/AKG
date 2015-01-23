using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    public float Duration;

    private List<ParticleSystem> particleList;
    private List<Animation> animationList;

    public void Play(bool loop)
    {
        if (particleList != null)
        {
            particleList.ForEach(item =>
            {
                item.loop = loop;
                item.Play();
            });
        }

        if (animationList != null)
        {
            animationList.ForEach(item =>
            {
                item.wrapMode = (loop) ? WrapMode.Loop : WrapMode.Once;
                item.Play();
            });
        }
    }

    public void Stop()
    {
        if (particleList != null)
        {
            particleList.ForEach(item => item.Stop());
        }
        if (animationList != null)
        {
            animationList.ForEach(item => item.Stop());
        }
    }

    private float GetDuration()
    {
        var max = particleList.Select(particle => particle.duration).Concat(new[] { float.MinValue }).Max();
        return max;
    }

    private void Awake()
    {
        particleList = new List<ParticleSystem>();
        particleList.AddRange(GetComponentsInChildren<ParticleSystem>());

        animationList = new List<Animation>();
        animationList.AddRange(GetComponentsInChildren<Animation>());

        Duration = GetDuration();
    }
}
