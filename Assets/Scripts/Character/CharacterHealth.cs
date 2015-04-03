using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public int CurrentHealth;
    public int HurtHealth;

    private Animator animator;

    public void Hurt()
    {
        CurrentHealth -= HurtHealth;
        animator.SetTrigger("Hurt");

        if (CurrentHealth < 0)
        {
            animator.SetTrigger("Die");
        }
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
}
