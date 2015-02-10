using AnimationOrTween;
using UnityEngine;

public class FlowingGroundController : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.LogWarning("On trigger endter");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.LogWarning("On trigger exit");
        collider2D.isTrigger = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.LogWarning("On collision enter.");

        var rigid = other.rigidbody;
        var velocityUp = rigid.velocity.y > 0;
        var center = collider2D.bounds.center;
        var size = collider2D.bounds.size / 2;
        var colliderCenter = rigid.collider2D.bounds.center;
        var colliderSize = rigid.collider2D.bounds.size / 2;

        //collider2D.isTrigger = true;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        Debug.LogWarning("On collision exit");
    }
}
