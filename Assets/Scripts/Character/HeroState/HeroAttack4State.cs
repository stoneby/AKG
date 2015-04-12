using UnityEngine;

public class HeroAttack4State : MonoBehaviour
{
    private PlayerControl player;

    void OnEnable()
    {
        player.LastAttack = true;
    }

	void OnDisable()
	{
		player.LastAttack = false;
	}

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }
}
