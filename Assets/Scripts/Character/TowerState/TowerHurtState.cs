using UnityEngine;

public class TowerHurtState : MonoBehaviour
{
    private PlayerControl player;
    private MonsterHUDController hudController;
    private CharacterHealth health;

    private Animator anim;

    void OnEnable()
    {
        //hudController.HpValue = health.NormalizedHealth;
        //hudController.Show(true);
    }
   
    /// <summary>
    /// Decision point.
    /// </summary>
    /// <remarks>To idle state or duang state.</remarks>
    public void OnDecision()
    {
        if (player.BoomFight && player.LastAttack)
        {
            anim.SetTrigger("Duang");
        }
    }

    void Awake()
    {
        hudController = GetComponent<MonsterHUDController>();
        health = GetComponent<CharacterHealth>();

        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }
}
