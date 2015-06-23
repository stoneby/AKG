using UnityEngine;

public class DieState : MonoBehaviour 
{
	public float DisappearTime;

    public bool Destory;

	void OnEnable()
	{
		rigidbody2D.velocity = Vector2.zero;

        Invoke("OnDie", DisappearTime);
	}

    private void OnDie()
    {
        if (Destory)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

	void Awake()
	{
	}
}
