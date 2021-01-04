using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    private void Awake()
    {
        Invoke("Destruct", 1f);
    }

    void Destruct()
    {
        Destroy(gameObject);
    }
}
