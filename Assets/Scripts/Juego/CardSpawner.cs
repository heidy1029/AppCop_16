using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    public GameObject[] cards; // Array de tarjetas de una familia
    public Transform areaCenter; // Centro del área donde quieres esparcir las tarjetas
    public float areaRadius = 10f; // Radio del área en la que se esparcirán las tarjetas

    private bool isSpawningActive = false;

    // Método para activar la dispersión de las cartas desde el FamilyManager
    public void ActivateSpawning()
    {
        isSpawningActive = true;
        foreach (GameObject card in cards)
        {
            Vector3 randomPosition = GetRandomPosition();
            card.transform.position = randomPosition;
            card.SetActive(true); // Asegurarse de que las tarjetas estén activas
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * areaRadius;
        randomDirection.y = 0; // Mantener la altura constante
        return areaCenter.position + randomDirection;
    }

    private void Start()
    {
        // Inicialmente desactivar todas las tarjetas
        foreach (GameObject card in cards)
        {
            card.SetActive(false); // Desactivar las tarjetas hasta que se active el nivel
        }
    }


}
