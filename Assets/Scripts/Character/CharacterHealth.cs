using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public int TotalHealth;
    public int CurrentHealth;
    public int HurtHealth;

	public string TriggerState;

    public GameObject MessageListener;

    public float NormalizedHealth { get { return 1f * CurrentHealth / TotalHealth; } }
    public bool Dead { get { return CurrentHealth <= 0; } }

    private Animator animator;

	public void AddHealth(int health)
	{
		CurrentHealth = (CurrentHealth + health);
		CurrentHealth = (CurrentHealth > TotalHealth) ? TotalHealth : CurrentHealth;

		SendMessage("UpdateHealth", NormalizedHealth, SendMessageOptions.DontRequireReceiver);
	}

    public void Hurt()
    {
        CurrentHealth -= HurtHealth;

        SendMessage("UpdateHealth", NormalizedHealth, SendMessageOptions.DontRequireReceiver);

        if (CurrentHealth <= 0)
        {
			animator.SetTrigger(TriggerState);
            MessageListener.SendMessage("Dead", gameObject, SendMessageOptions.DontRequireReceiver);
        }
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        CurrentHealth = TotalHealth;

        if (MessageListener == null)
        {
            MessageListener = gameObject;
        }
    }
}
