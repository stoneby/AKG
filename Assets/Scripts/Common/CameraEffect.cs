using UnityEngine;

public class CameraEffect : MonoBehaviour
{
    private Animation anim;

    public void EarthQuake()
    {
        anim.Play("EarthQuake");
    }

    public void UpAndDown()
    {
        anim.Play("UpAndDown");
    }

    void Awake()
    {
        anim = GetComponent<Animation>();
    }
}
