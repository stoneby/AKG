using UnityEngine;

public class BornEffectController : MonoBehaviour
{
    public OneShotEffectController NormalEffect;

    private PlayerControl player;
    private Camera mainCamera;

    public void OnPlayNormalEffect()
    {
        NormalEffect.gameObject.SetActive(true);
    }

    public void OnShake()
    {
        mainCamera.GetComponent<Animation>().Play();
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        mainCamera = Camera.main;
    }
}
