using UnityEngine;

public class HeroRunState : MonoBehaviour
{
    public GameObject RunEffect;

    private Transform runLocation;
    private Animator runAnimator;

    void OnEnable()
    {
        runAnimator.gameObject.SetActive(true);
        runAnimator.Play("Run");
    }

    void OnDisable()
    {
        runAnimator.gameObject.SetActive(false);
    }

    void Awake()
    {
        runLocation = transform.Find("Effect/RunLocation");
        var runEffect = Instantiate(RunEffect, runLocation.position, runLocation.rotation) as GameObject;
        runEffect.transform.parent = runLocation;
        runAnimator = runEffect.GetComponent<Animator>();
    }
}
