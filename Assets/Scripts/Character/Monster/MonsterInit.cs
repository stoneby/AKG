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
        }
    }

    void Awake()
    {
        groundCheck = transform.Find("GroundCheck");
    }
}
