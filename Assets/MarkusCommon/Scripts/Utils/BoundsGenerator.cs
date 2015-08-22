using UnityEngine;

/// <summary>
/// Generator bounds to current game object according to all its children.
/// </summary>
public class BoundsGenerator : MonoBehaviour
{
    public bool IncludeInactiveChild;

    /// <summary>
    /// The generated bounds scale.
    /// </summary>
    /// <remarks>1 as default, which is just bounds in all children.</remarks>
    [Range(0f, 2f)]
    public float Scale = 1f;

    [ContextMenu("Generate")]
    public void Generate()
    {
        if (transform.childCount == 0)
        {
            Debug.LogWarning("There is no children at all, we will not do anything.");
            return;
        }

        var bounds = transform.GetChild(0).GetComponent<Collider>().bounds;
        for (var i = 1; i < transform.childCount; ++i)
        {
            var child = transform.GetChild(i);
            if (!IncludeInactiveChild && !child.gameObject.activeSelf)
            {
                continue;
            }
            bounds.Encapsulate(child.GetComponent<Collider>().bounds);
        }
        var boxCollider = GetComponent<BoxCollider>();
        if (boxCollider == null)
        {
            boxCollider = gameObject.AddComponent<BoxCollider>();
        }

        // convert world space to local space.
        // Note: bounds using world space, but box collider using local space.
        var localMin = transform.InverseTransformPoint(bounds.min);
        var localMax = transform.InverseTransformPoint(bounds.max);
        boxCollider.center = (localMin + localMax) / 2;
        boxCollider.size = localMax - localMin;
        boxCollider.size *= Scale;
    }

    void Start()
    {
        Generate();
    }
}
