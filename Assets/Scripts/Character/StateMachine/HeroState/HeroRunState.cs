using UnityEngine;

public class HeroRunState : MonoBehaviour
{
	public AudioClip Clip;
    public GameObject RunEffect;

    private Transform runLocation;
    private Animator runAnimator;

    void OnEnable()
    {
		GetComponent<AudioSource>().clip = Clip;
		GetComponent<AudioSource>().Play();
    }

    void OnDisable()
    {
        //runAnimator.gameObject.SetActive(false);
    }

    void Awake()
    {
        runLocation = transform.Find("Effect/RunLocation");

        var runEffect = Instantiate(RunEffect, runLocation.position, runLocation.rotation) as GameObject;
        runEffect.transform.parent = runLocation;
        runAnimator = runEffect.GetComponent<Animator>();
		runAnimator.gameObject.SetActive(false);
    }
}
