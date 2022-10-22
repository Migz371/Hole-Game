using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textDestroy : MonoBehaviour
{
    public float secondsToDestroy = 1f;

    private void Start()
    {
        Destroy(gameObject, secondsToDestroy);
    }

}
