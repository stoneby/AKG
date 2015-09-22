using UnityEngine;

public class CameraEffect : Singleton<CameraEffect>
{
    public string EarthQuakeEffect;
    public string UpAndDownEffect;

    private Animation anim;

    public void EarthQuake()
    {
        anim.Play(EarthQuakeEffect);
    }

    public void UpAndDown()
    {
        anim.Play(UpAndDownEffect);
    }

    void Awake()
    {
        anim = transform.parent.GetComponent<Animation>();
    }
}
