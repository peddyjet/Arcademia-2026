using UnityEngine;

public class FollowAndOrbit2D : MonoBehaviour
{
    public Transform target;
    public float radius = 2f;
    public float orbitSpeed = 180f; // degrees per second

    void Update()
    {
        if (target == null) return;

        float angle = Time.time * orbitSpeed;
        float rad = angle * Mathf.Deg2Rad;

        Vector3 offset = new Vector3(
            Mathf.Cos(rad) * radius,
            Mathf.Sin(rad) * radius,
            0f
        );

        transform.position = target.position + offset;
    }
}