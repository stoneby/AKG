using UnityEngine;

public class HurtState : MonoBehaviour
{
    private MonsterHUDController hudController;
    private CharacterHealth health;

    void OnEnable()
    {
        //hudController.HpValue = health.NormalizedHealth;
        //hudController.Show(true);
    }

    void Awake()
    {
        hudController = GetComponent<MonsterHUDController>();
        health = GetComponent<CharacterHealth>();
    }
}
