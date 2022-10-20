using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changePos : MonoBehaviour
{
    public PolygonCollider2D hole2DCollider;

    private void FixedUpdate()
    {
        if(transform.hasChanged == true)
        {
            transform.hasChanged = false;
            hole2DCollider.transform.position = new Vector2(transform.position.x, transform.position.z);

        }
    }
}
