using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Vector3 pos;
    private void Awake()
    {
        pos = this.transform.position;
    }
}
