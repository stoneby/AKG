using UnityEngine;

public class MonsterInit : MonoBehaviour
{
    private Transform groundCheck;

    void Update()
    {
        var hit = Physics2D.Linecast(transform.position, groundCheck.position, LayerMask.GetMask("Ground"));
        var grounded = (hit.collider != null);
        if (grounded)
        {
            rigidbody2D.isKinematic = true;
            collider2D.isTrigger = true;
            enabled = false;

			if (hit.collider.tag.Equals("FlowStone"))
			{
				Debug.LogWarning("Monster hits on the flow stone.");
				transform.parent = hit.collider.transform;
			}
        }
    }

    void Awake()
    {
        groundCheck = transform.Find("GroundCheck");
    }
}
