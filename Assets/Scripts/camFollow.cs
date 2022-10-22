
using UnityEngine;

public class camFollow : MonoBehaviour
{
    public Transform player;
    public float movspeed = 0.125f; // 0-1
    public Vector3 offset;

    private void LateUpdate()
    {
        transform.position = player.position + offset;
    }
}
