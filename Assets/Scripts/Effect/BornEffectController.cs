using UnityEngine;

public class BornEffectController : MonoBehaviour
{
    public OneShotEffectController NormalEffect;
    
    public void OnPlayNormalEffect()
    {
        NormalEffect.gameObject.SetActive(true);
    }

    public void OnShake()
    {
        CameraEffect.Instance.UpAndDown();
    }

    void Awake()
    {
    }
}
