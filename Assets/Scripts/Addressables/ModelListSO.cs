using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModelList", menuName = "Addressables/Model List")]
public class ModelListSO : ScriptableObject
{
    [SerializeField] private List<string> _modelAddresses;

    public List<string> ModelAddresses => _modelAddresses;
}
