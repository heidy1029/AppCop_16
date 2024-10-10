using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class TriviaManagerInfo : MonoBehaviour
{
    public TMP_Text preguntaTexto;  // TextMeshPro para mostrar la pregunta
    public TMP_Text[] opcionesTexto;  // Array de TextMeshPro para las opciones de respuesta
    private int indicePreguntaActual;

    private List<Pregunta> preguntas = new List<Pregunta>();
    private Pregunta preguntaActual;
    void Start()
    {
        // Definir las preguntas dentro del script
        preguntas.Add(new Pregunta(
            "¿Cuántos tipos de ecosistemas representativos del Valle Geográfico del Río Cauca se encuentran en Cali?",
            new string[] { "3", "5", "7", "6" },
            1
        ));

        preguntas.Add(new Pregunta(
            "¿Cuál es el ecosistema dominante en la zona urbana de Cali?",
            new string[] { "Bosque cálido húmedo en piedemonte", "Arbustales y matorrales medio seco", "Bosque cálido seco", "Bosque medio húmedo" },
            2
        ));

        preguntas.Add(new Pregunta(
            "¿Cuántos ríos influyen en la zona urbana o periurbana de Cali?",
            new string[] { "5", "6", "8", "7" },
            2
        ));

        preguntas.Add(new Pregunta(
            "¿Cuál es el objetivo principal del Grupo Conservación de Ecosistemas del DAGMA?",
            new string[] { "Mejorar la calidad del agua en los ríos de Cali", "Desarrollar la gestión integral de los ecosistemas del Municipio de Santiago de Cali", "Restaurar el bosque seco tropical", "Promover el turismo en los parques naturales" },
            1
        ));

        preguntas.Add(new Pregunta(
            "Una de las responsabilidades del Grupo Conservación de Ecosistemas del DAGMA es:",
            new string[] { "Controlar la expansión urbana en zonas rurales", "Promover la construcción de nuevas reservas naturales", "Coordinar y ejecutar acciones de restauración, protección, conservación y monitoreo de los ecosistemas", "Fiscalizar el uso de terrenos en áreas industriales" },
            2
        ));

        preguntas.Add(new Pregunta(
            "¿Qué actividades de sensibilización realiza el Grupo Conservación de Ecosistemas del DAGMA?",
            new string[] { "Proyectos de vivienda sostenible", "Talleres, charlas y celebración de fechas ambientales", "Desarrollo de proyectos agroindustriales", "Consultoría para la construcción de presas" },
            1
        ));

        preguntas.Add(new Pregunta(
            "¿Cuál de los siguientes es un Ecoparque en Cali?",
            new string[] { "Ecoparque Cerro de la Bandera", "Parque Nacional del Chicamocha", "Parque Metropolitano Simón Bolívar", "Parque de los Nevados" },
            0
        ));

        preguntas.Add(new Pregunta(
            "¿Cuál de los siguientes es un humedal urbano en Cali?",
            new string[] { "Humedal El Retiro", "Humedal Chingaza", "Humedal La Conejera", "Humedal La Florida" },
            0
        ));

        preguntas.Add(new Pregunta(
            "¿Cuál de los siguientes es un predio de conservación en Cali?",
            new string[] { "La Yolanda", "La Tatacoa", "El Cocuy", "Sierra Nevada" },
            0
        ));

        preguntas.Add(new Pregunta(
            "¿Cuál de los siguientes ecoparques cuenta con un jardín diseñado especialmente para polinizadores?",
            new string[] { "Ecoparque Cerro de la Bandera", "Ecoparque Tres Cruces Bataclán", "Parque Ambiental Corazón de Pance", "Reserva Ecológica Distrital Cuenca Media del Río Lili" },
            2
        ));

        preguntas.Add(new Pregunta(
            "¿En qué lugar se han encontrado nuevas especies para la ciencia, aunque su acceso es restringido para visitantes generales?",
            new string[] { "Predios de conservación administrados por el Dagma", "Ecoparque Pisamos", "Ecoparque Tres Cruces Bataclán", "Ecoparque Cerro de la Bandera" },
            0
        ));

        preguntas.Add(new Pregunta(
            "¿Cuál de estos lugares ofrece la oportunidad de observar aves a lo largo de senderos cercanos a un humedal?",
            new string[] { "Reserva Ecológica Distrital Cuenca Media del Río Lili", "Ecoparque Cerro de la Bandera", "Ecoparque Pisamos", "Parque Ambiental Corazón de Pance" },
            0
        ));

        preguntas.Add(new Pregunta(
            "¿Qué ecoparque tiene casetas de observación de aves ubicadas al costado de senderos adecuados para caminantes?",
            new string[] { "Ecoparque Pisamos", "Ecoparque Cerro de la Bandera", "Parque Ambiental Corazón de Pance", "Ecoparque Tres Cruces Bataclán" },
            1
        ));

        preguntas.Add(new Pregunta(
            "¿Cuál de los siguientes lugares tiene como uno de sus usos principales la recreación pasiva y la educación ambiental?",
            new string[] { "Ecoparque Tres Cruces Bataclán", "Ecoparque Cerro de la Bandera", "Predio de Conservación El Danubio", "Reserva Ecológica Distrital Cuenca Media del Río Lili" },
            0
        ));

        // Llamar a la función que selecciona y muestra una pregunta aleatoria
        MostrarPreguntaAleatoria();
    }

    // Función para seleccionar una pregunta aleatoria y mostrarla en el Canvas
    void MostrarPreguntaAleatoria()
    {
        System.Random random = new System.Random();
        indicePreguntaActual = random.Next(preguntas.Count);
        preguntaActual = preguntas[indicePreguntaActual];

        // Mostrar la pregunta y las opciones en el Canvas
        preguntaTexto.text = preguntaActual.pregunta;
        for (int i = 0; i < opcionesTexto.Length; i++)
        {
            opcionesTexto[i].text = preguntaActual.opciones[i];
        }
    }

    // Función para verificar la respuesta seleccionada
    public void SeleccionarRespuesta(int indiceOpcion)
    {
        if (indiceOpcion == preguntaActual.indiceCorrecto)
        {
            // Si es correcta, pasa a la siguiente pregunta
            Debug.Log("¡Respuesta correcta!");
            MostrarPreguntaAleatoria();
        }
        else
        {
            // Si no es correcta, muestra un mensaje o realiza una acción
            Debug.Log("Respuesta incorrecta, intenta de nuevo.");
        }
    }

    // Clase para representar una pregunta
    [System.Serializable]
    public class Pregunta
    {
        public string pregunta;
        public string[] opciones;
        public int indiceCorrecto;

        public Pregunta(string pregunta, string[] opciones, int indiceCorrecto)
        {
            this.pregunta = pregunta;
            this.opciones = opciones;
            this.indiceCorrecto = indiceCorrecto;
        }
    }
}
