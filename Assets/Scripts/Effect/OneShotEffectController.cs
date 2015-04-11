using UnityEngine;

public class OneShotEffectController : MonoBehaviour
{
	public string StateName;

    private Animator hitAnimator;

    public void Play()
    {
        hitAnimator.Play(StateName);
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
