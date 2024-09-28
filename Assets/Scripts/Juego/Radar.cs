using UnityEngine;
using UnityEngine.UI;

public class Radar : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float radarRange = 15f; // El rango del radar
    public RectTransform radarPanel; // Panel del radar en el Canvas
    public GameObject radarIconPrefab; // Prefab del icono verde que representa los objetos
    public GameObject playerIcon; // Imagen azul que representa al jugador en el radar
    public float radarIconSize = 10; // Tamaño del icono en el radar

    private void Start()
    {
        // Posicionamos el icono del player en el centro
        playerIcon.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        // Actualizamos el radar en el inicio
        UpdateRadar();
    }

    private void Update()
    {
        // Continuamente actualizamos el radar mientras el personaje se mueva
        UpdateRadar();
    }

    void UpdateRadar()
    {
        // Limpiamos los iconos anteriores del radar
        foreach (Transform child in radarPanel)
        {
            if (child.gameObject != playerIcon) // No eliminamos el icono del jugador
            {
                Destroy(child.gameObject);
            }
        }

        // Encontramos todos los objetos con el tag "Card"
        GameObject[] cards = GameObject.FindGameObjectsWithTag("Card");

        foreach (GameObject card in cards)
        {
            // Calculamos la distancia desde el jugador hasta el objeto
            Vector3 offset = card.transform.position - player.position;

            // Si el objeto está dentro del rango del radar
            if (offset.magnitude <= radarRange)
            {
                // Rotamos el offset según la rotación del jugador para que siga su orientación
                Vector3 relativePosition = Quaternion.Euler(0, -player.eulerAngles.y, 0) * offset;

                // Creamos un nuevo ícono en el radar
                GameObject icon = Instantiate(radarIconPrefab, radarPanel);
                icon.GetComponent<RectTransform>().sizeDelta = new Vector2(radarIconSize, radarIconSize);

                // Calculamos la posición del objeto en el radar (normalizado)
                float x = (relativePosition.x / radarRange) * (radarPanel.rect.width / 2f);
                float y = (relativePosition.z / radarRange) * (radarPanel.rect.height / 2f);

                // Posicionamos el icono en la posición correcta dentro del panel del radar
                icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
            }
        }
    }
}
