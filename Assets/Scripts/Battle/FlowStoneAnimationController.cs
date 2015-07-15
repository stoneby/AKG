using UnityEngine;

public class FlowStoneAnimationController : MonoBehaviour
{
    public float StartTime;

    private Animator animator;

    void Flowing()
    {
        animator.enabled = true;
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    void Start()
    {
        Invoke("Flowing", StartTime);
    }
}
