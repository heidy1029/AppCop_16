using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ModelLoader : MonoBehaviour
{
    public ModelListSO modelListSO; // Referencia al ScriptableObject

    void Start()
    {
        LoadModelsFromScriptableObject();
    }

    private void LoadModelsFromScriptableObject()
    {
        foreach (string address in modelListSO.ModelAddresses)
        {
            Addressables.LoadAssetAsync<GameObject>(address).Completed += OnModelLoaded;
        }
    }

    private void OnModelLoaded(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject modelInstance = Instantiate(obj.Result);
            modelInstance.transform.position = Vector3.zero;
        }
        else
        {
            Debug.LogError("Failed to load model: " + obj.DebugName);
        }
    }
}
