using UnityEngine;

public class OneShotEffectController : MonoBehaviour
{
	public string StateName;
    public bool AutoPlay;

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

        if (AutoPlay)
        {
            Play();
        }
    }
}
