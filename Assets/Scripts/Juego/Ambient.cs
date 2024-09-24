using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambient : MonoBehaviour
{
    [SerializeField] private int _id;

    public int Id => _id;

    public void UnlockedAmbient(int id)
    {
        if (_id == id)
        {
            gameObject.tag = "UnlockedPlane";
            gameObject.layer = LayerMask.NameToLayer("UnlockedPlane");

            Debug.Log("Ambient unlocked: tag and layer updated.");
        }
        else
        {
            Debug.Log("Id does not match.");
        }
    }
}
