using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CicleDayNight : MonoBehaviour
{
    public int rotationScale = 10;
    void Update()
    {
        transform.Rotate(rotationScale * Time.deltaTime, 0, 0);
    }
}
