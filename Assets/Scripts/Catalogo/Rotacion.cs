using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotacion : MonoBehaviour
{
    [SerializeField] List<GameObject> cartas;
    [SerializeField] int cartasPorPagina = 3;
    [SerializeField] GameObject contenedorCartas;
    [SerializeField] Button botonSiguiente;
    [SerializeField] Button botonAnterior;

    int paginaActual = 0;
    int totalPaginas;

    private void Start()
    {
        totalPaginas = Mathf.CeilToInt((float)cartas.Count / cartasPorPagina) - 1;
        MostrarPagina(paginaActual);
        ActualizarBotones();
    }

    public void SiguientePagina()
    {
        if (paginaActual < totalPaginas)
        {
            paginaActual++;
            MostrarPagina(paginaActual);
            ActualizarBotones();
        }
    }

    public void PaginaAnterior()
    {
        if (paginaActual > 0)
        {
            paginaActual--;
            MostrarPagina(paginaActual);
            ActualizarBotones();
        }
    }

    public void ActualizarCartas()
    {
        // Este método puede ser llamado cuando se agregue una nueva imagen.
        totalPaginas = Mathf.CeilToInt((float)cartas.Count / cartasPorPagina) - 1;
        MostrarPagina(paginaActual);
        ActualizarBotones();
    }

    private void MostrarPagina(int pagina)
    {
        foreach (GameObject carta in cartas)
        {
            carta.SetActive(false);
        }

        int inicio = pagina * cartasPorPagina;
        int fin = Mathf.Min(inicio + cartasPorPagina, cartas.Count);

        for (int i = inicio; i < fin; i++)
        {
            cartas[i].SetActive(true);
        }
    }

    private void ActualizarBotones()
    {
        botonAnterior.interactable = paginaActual > 0;
        botonSiguiente.interactable = paginaActual < totalPaginas;
    }
}


/*public class Rotacion : MonoBehaviour
{
    [SerializeField] List<GameObject> cartas; // Lista de todas las cartas
    [SerializeField] int cartasPorPagina = 8; // Número de cartas por página
    [SerializeField] GameObject contenedorCartas; // Panel donde se mostrarán las cartas
    [SerializeField] Button botonSiguiente;
    [SerializeField] Button botonAnterior;
    int paginaActual = 0;
    int totalPaginas;

    private void Start()
    {
        totalPaginas = Mathf.CeilToInt((float)cartas.Count / cartasPorPagina) - 1;
        MostrarPagina(paginaActual);
        ActualizarBotones();
    }

    public void SiguientePagina()
    {
        if (paginaActual < totalPaginas)
        {
            paginaActual++;
            MostrarPagina(paginaActual);
            ActualizarBotones();
        }
    }

    public void PaginaAnterior()
    {
        if (paginaActual > 0)
        {
            paginaActual--;
            MostrarPagina(paginaActual);
            ActualizarBotones();
        }
    }

    private void MostrarPagina(int pagina)
    {
        // Ocultar todas las cartas primero
        foreach (GameObject carta in cartas)
        {
            carta.SetActive(false);
        }

        // Mostrar solo las cartas de la página actual
        int inicio = pagina * cartasPorPagina;
        int fin = Mathf.Min(inicio + cartasPorPagina, cartas.Count);

        for (int i = inicio; i < fin; i++)
        {
            cartas[i].SetActive(true);
        }
    }

    private void ActualizarBotones()
    {
        botonAnterior.interactable = paginaActual > 0;
        botonSiguiente.interactable = paginaActual < totalPaginas;
    }
}*/