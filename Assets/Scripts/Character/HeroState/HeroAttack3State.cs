using UnityEngine;

public class HeroAttack3State : MonoBehaviour
{
    private PlayerControl player;

    void OnEnable()
    {
        player.BoomFight = true;
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }
}
