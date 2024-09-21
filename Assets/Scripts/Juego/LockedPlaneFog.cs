using UnityEngine;

public class LockedPlaneFog : MonoBehaviour
{
    [SerializeField] private Material _fogMaterial; 

    private void Start()
    {
        GameObject[] lockedPlanes = GameObject.FindGameObjectsWithTag("LockedPlane");

        foreach (GameObject plane in lockedPlanes)
        {
            MeshRenderer renderer = plane.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.material = _fogMaterial;
            }
        }
    }
}