using UnityEngine;

public class HeroHurtState : MonoBehaviour
{
    public GameObject HurtEffectPrefab;

    private Transform hitLocation;
    private OneShotEffectController effectController;
    private GameObject hitEffect;

    private bool initialized;

    void OnEnable()
    {
        GenerateEffect();

        effectController.Play();
    }

    void Awake()
    {
        hitLocation = transform.Find("Effect/HurtLocation");
    }

    private void GenerateEffect()
    {
        hitEffect = Instantiate(HurtEffectPrefab, hitLocation.position, hitLocation.rotation) as GameObject;
        hitEffect.transform.parent = hitLocation;

        effectController = hitEffect.GetComponent<OneShotEffectController>();
    }
}
