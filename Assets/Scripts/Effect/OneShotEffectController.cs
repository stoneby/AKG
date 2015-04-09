using UnityEngine;

public class OneShotEffectController : MonoBehaviour
{
    private Animator hitAnimator;

    public void Play()
    {
        hitAnimator.Play("Hit");
    }

    public void OnPlayEnd()
    {
        Destroy(gameObject);
    }

    void Awake()
    {
        hitAnimator = GetComponent<Animator>();
    }

}
