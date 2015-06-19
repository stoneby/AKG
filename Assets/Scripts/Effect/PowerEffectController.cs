using UnityEngine;

public class PowerEffectController : MonoBehaviour
{
    public delegate void Shoot(GameObject go);

    public Shoot OnShoot;

    public void ToShoot()
    {
        if (OnShoot != null)
        {
            OnShoot(gameObject);
        }
    }
}
