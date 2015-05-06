using UnityEngine;

public class OneShotEffectController : MonoBehaviour
{
	public string StateName;

    private Animator anim;

    public void Play()
    {
        anim.Play(StateName);
    }

    public void OnPlayEnd()
    {
        Destroy(gameObject);
    }

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
}
