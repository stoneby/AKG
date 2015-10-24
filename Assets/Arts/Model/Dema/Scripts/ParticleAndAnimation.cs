using UnityEngine;

public class ParticleAndAnimation : MonoBehaviour
{
    void Start()
    {
        PlayOnce();
    }

    [ContextMenu("Play Loop")]
    public void PlayLoop()
    {
        var pss = GetComponentsInChildren<ParticleSystem>(true);
        foreach (var ps in pss)
        {
            ps.loop = true;
            ps.Play();
        }
        var anis = GetComponentsInChildren<Animation>(true);
        foreach (var an in anis)
        {
            an.wrapMode = WrapMode.Loop;
            an.Play();
        }
    }

    [ContextMenu("Play Once")]
    public void PlayOnce()
    {
        var pss = GetComponentsInChildren<ParticleSystem>(true);
        foreach (var ps in pss)
        {
            ps.loop = false;
            ps.Play();
        }
        var anis = GetComponentsInChildren<Animation>(true);
        foreach (var an in anis)
        {
            an.wrapMode = WrapMode.Once;
            an.Play();
        }
    }
}
