using UnityEngine;

public class DieState : MonoBehaviour 
{
	public AudioClip Clip;

	public float DisappearTime;

    public bool Destory;

	void OnEnable()
	{
		audio.clip = Clip;
		audio.Play();

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
