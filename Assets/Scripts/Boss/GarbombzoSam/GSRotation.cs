using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GSRotation : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, 360) * Time.deltaTime);
    }
}
