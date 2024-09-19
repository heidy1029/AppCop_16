// Assets/Scripts/Juego/Example.cs
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    public List<BirdInfo> birdInfos = new List<BirdInfo>();
    public List<ModelQuestions> allModelQuestions = new List<ModelQuestions>();

    void Start()
    {
        // Inicializar birdInfos
        InitializeBirdInfos();

        // Inicializar allModelQuestions
        InitializeModelQuestions();
    }

    /// <summary>
    /// Inicializa la lista de información de aves.
    /// </summary>
    private void InitializeBirdInfos()
    {
        birdInfos.Add(new BirdInfo
        {
            modelIndex = 0,
            birdName = "Colibrí Esmeralda",
            species = "Chlorostilbon canivetii",
            description = "Un pequeño y colorido pájaro conocido por su capacidad de volar hacia atrás.",
            mainImage = Resources.Load<Sprite>("Images/colibri_esmeralda"),
            habitat = "Bosques tropicales",
            diet = "Néctar y pequeños insectos",
            reproduction = "Anidan en árboles pequeños, con nidos de hojas y musgo",
            size = "3-4 pulgadas",
            funFact1 = "Los colibríes baten sus alas hasta 80 veces por segundo.",
            funFact2 = "Son las únicas aves que pueden volar hacia atrás.",
            secondaryImage = Resources.Load<Sprite>("Images/colibri_habitat"),
            location = "Centroamérica y el sur de México",
            bibliography = "National Geographic, 2020"
        });

        birdInfos.Add(new BirdInfo
        {
            modelIndex = 1,
            birdName = "Águila Calva",
            species = "Haliaeetus leucocephalus",
            description = "El símbolo nacional de los Estados Unidos, conocido por su cabeza blanca.",
            mainImage = Resources.Load<Sprite>("Images/aguila_calva"),
            habitat = "Bosques y costas",
            diet = "Peces y pequeños mamíferos",
            reproduction = "Anidan en grandes árboles, con nidos de ramas",
            size = "28-40 pulgadas",
            funFact1 = "Pueden alcanzar velocidades de 100 km/h en vuelo.",
            funFact2 = "Viven hasta 20 años en la naturaleza.",
            secondaryImage = Resources.Load<Sprite>("Images/aguila_habitat"),
            location = "América del Norte",
            bibliography = "Smithsonian Institution, 2018"
        });

        // Agrega más aves aquí con distintos modelIndex
        birdInfos.Add(new BirdInfo
        {
            modelIndex = 2,
            birdName = "Colibrí de Garganta Púrpura",
            species = "Archilochus colubris",
            description = "Conocido por su brillante garganta púrpura y comportamiento territorial.",
            mainImage = Resources.Load<Sprite>("Images/colibri_garganta_purpura"),
            habitat = "Bosques y jardines floridos",
            diet = "Néctar, insectos y arañas",
            reproduction = "Anidan en pequeñas cavidades o colgantes hechas de fibras finas",
            size = "3 pulgadas",
            funFact1 = "Puede batir sus alas hasta 80 veces por segundo.",
            funFact2 = "Tiene un pico largo y delgado adaptado para alimentarse de néctar.",
            secondaryImage = Resources.Load<Sprite>("Images/colibri_garganta_purpura_habitat"),
            location = "América del Norte",
            bibliography = "BirdLife International, 2019"
        });

        birdInfos.Add(new BirdInfo
        {
            modelIndex = 3,
            birdName = "Colibrí Pecho Leonado",
            species = "Selasphorus platycercus",
            description = "Destaca por su pecho de color leonado y su vuelo ágil.",
            mainImage = Resources.Load<Sprite>("Images/colibri_pecho_leonado"),
            habitat = "Bosques templados y jardines",
            diet = "Néctar de flores y pequeños insectos",
            reproduction = "Construye nidos de forma de copa usando fibras finas y telarañas",
            size = "3.5 pulgadas",
            funFact1 = "Es uno de los colibríes más comunes en América del Norte.",
            funFact2 = "Puede entrar en estado de torpor durante la noche para conservar energía.",
            secondaryImage = Resources.Load<Sprite>("Images/colibri_pecho_leonado_habitat"),
            location = "América del Norte y Central",
            bibliography = "Cornell Lab of Ornithology, 2021"
        });
    }

    /// <summary>
    /// Inicializa la lista de preguntas agrupadas por modelo.
    /// </summary>
    private void InitializeModelQuestions()
    {
        // Preguntas para modelIndex 0
        allModelQuestions.Add(new ModelQuestions
        {
            modelIndex = 0,
            questions = new List<Question>
            {
                new Question
                {
                    questionText = "¿Cuál es una característica distintiva del macho de la especie Green-crowned Woodnymph (Thalurania fannyi)?",
                    answers = new List<string>
                    {
                        "A) La corona del macho es de un verde brillante",
                        "B) La cola del macho es completamente negra",
                        "C) El pecho del macho es de color rojo brillante",
                        "D) La garganta del macho es de color gris"
                    },
                    correctAnswerIndex = 0
                },
                new Question
                {
                    questionText = "¿Dónde suelen habitar las ninfas del bosque (Thalurania)?",
                    answers = new List<string>
                    {
                        "A) En áreas abiertas y con matorrales",
                        "B) En el sotobosque de bosques húmedos de tierras bajas y en bosques secundarios avanzados adyacentes",
                        "C) En desiertos y áreas áridas",
                        "D) En regiones montañosas y áreas secas"
                    },
                    correctAnswerIndex = 1
                },
                new Question
                {
                    questionText = "¿Qué tipo de dieta tienen las ninfas del bosque (Thalurania)?",
                    answers = new List<string>
                    {
                        "A) Se alimentan principalmente de insectos y frutas",
                        "B) Prefieren flores de una amplia variedad de epífitas, arbustos y árboles pequeños, y ocasionalmente recogen artrópodos",
                        "C) Se alimentan exclusivamente de néctar de flores grandes como Heliconia y Costus",
                        "D) Consumen solo néctar de flores de grandes árboles como Inga"
                    },
                    correctAnswerIndex = 2
                }
            }
        });

        // Preguntas para modelIndex 1
        allModelQuestions.Add(new ModelQuestions
        {
            modelIndex = 1,
            questions = new List<Question>
            {
                new Question
                {
                    questionText = "¿Cuál es la característica distintiva del colibrí pecho leonado que lo diferencia de otros colibríes de tamaño similar?",
                    answers = new List<string>
                    {
                        "A) Es completamente verde por encima y beige por debajo",
                        "B) Tiene un color rojo intenso por debajo y verde en la parte superior",
                        "C) Su pecho es de color rosa brillante",
                        "D) Su pico es más largo y curvo que el de otros colibríes"
                    },
                    correctAnswerIndex = 0
                },
                new Question
                {
                    questionText = "¿En qué tipo de hábitat es más común encontrar al colibrí pecho leonado?",
                    answers = new List<string>
                    {
                        "A) En los bordes de los bosques abiertos.",
                        "B) En los bosques secos y áridos",
                        "C) En los bosques húmedos de montaña",
                        "D) En los campos de cultivo y praderas"
                    },
                    correctAnswerIndex = 2
                },
                new Question
                {
                    questionText = "¿Qué característica específica en la garganta del macho adulto del colibrí pecho leonado puede ayudar a identificarlo?",
                    answers = new List<string>
                    {
                        "A) Una mancha rosada en el centro de la garganta",
                        "B) Una mancha negra en la garganta",
                        "C) Un color verde brillante en la garganta",
                        "D) Una banda blanca en la garganta"
                    },
                    correctAnswerIndex = 0
                }
            }
        });

        // Preguntas para modelIndex 2
        allModelQuestions.Add(new ModelQuestions
        {
            modelIndex = 2,
            questions = new List<Question>
            {
                new Question
                {
                    questionText = "¿Dónde suelen buscar alimento los colibríes de garganta púrpura?",
                    answers = new List<string>
                    {
                        "A) En el suelo y en áreas abiertas",
                        "B) En las copas de los árboles en flor, donde los machos defienden los territorios florales",
                        "C) En los matorrales y arbustos bajos",
                        "D) En las áreas rocosas cerca de corrientes de agua"
                    },
                    correctAnswerIndex = 1
                },
                new Question
                {
                    questionText = "¿Cuál es una característica distintiva de los machos de colibrí de garganta púrpura en vuelo?",
                    answers = new List<string>
                    {
                        "A) Tienen una cola recta y sin bifurcar",
                        "B) Presentan manchas blancas en los flancos que se pueden ver en vuelo",
                        "C) Su garganta es de color verde brillante.",
                        "D) El pecho es de color rojo intenso"
                    },
                    correctAnswerIndex = 1
                },
                new Question
                {
                    questionText = "¿Cómo es el nido de los colibríes de garganta púrpura?",
                    answers = new List<string>
                    {
                        "A) Es una estructura grande y abierta hecha de ramitas.",
                        "B) Es un nido en forma de copa hecho de fibra fina y telaraña, ubicado sobre ramas gruesas de árboles altos.",
                        "C) Es un nido colgante hecho de hojas y ramitas.",
                        "D) Es una cavidad en un tronco de árbol."
                    },
                    correctAnswerIndex = 1
                }
            }
        });

        // Preguntas para modelIndex 3
        allModelQuestions.Add(new ModelQuestions
        {
            modelIndex = 3,
            questions = new List<Question>
            {
                new Question
                {
                    questionText = "¿Dónde suelen buscar alimento los colibríes de garganta púrpura?",
                    answers = new List<string>
                    {
                        "A) En el suelo y en áreas abiertas.",
                        "B) En las copas de los árboles en flor, donde los machos defienden los territorios florales.",
                        "C) En los matorrales y arbustos bajos.",
                        "D) En las áreas rocosas cerca de corrientes de agua"
                    },
                    correctAnswerIndex = 1
                },
                new Question
                {
                    questionText = "¿Cuál es una característica distintiva de los machos de colibrí de garganta púrpura en vuelo?",
                    answers = new List<string>
                    {
                        "A) Tienen una cola recta y sin bifurcar.",
                        "B) Presentan manchas blancas en los flancos que se pueden ver en vuelo.",
                        "C) Su garganta es de color verde brillante.",
                        "D) El pecho es de color rojo intenso"
                    },
                    correctAnswerIndex = 1
                },
                new Question
                {
                    questionText = "¿Cómo es el nido de los colibríes de garganta púrpura?",
                    answers = new List<string>
                    {
                        "A) Es una estructura grande y abierta hecha de ramitas.",
                        "B) Es un nido en forma de copa hecho de fibra fina y telaraña, ubicado sobre ramas gruesas de árboles altos.",
                        "C) Es un nido colgante hecho de hojas y ramitas.",
                        "D) Es una cavidad en un tronco de árbol."
                    },
                    correctAnswerIndex = 1
                }
            }
        });
    }

    /// <summary>
    /// Obtiene la información del ave basada en el modelIndex.
    /// </summary>
    /// <param name="modelIndex">Índice del modelo.</param>
    /// <returns>Objeto BirdInfo correspondiente.</returns>
    public BirdInfo GetBirdInfo(int modelIndex)
    {
        return birdInfos.Find(b => b.modelIndex == modelIndex);
    }

    /// <summary>
    /// Obtiene la imagen del modelo basada en el modelIndex.
    /// </summary>
    /// <param name="modelIndex">Índice del modelo.</param>
    /// <returns>Sprite de la imagen del modelo.</returns>
    public Sprite GetModelImage(int modelIndex)
    {
        BirdInfo birdInfo = GetBirdInfo(modelIndex);
        if (birdInfo != null)
        {
            return birdInfo.mainImage;
        }
        else
        {
            Debug.LogError($"No se encontró BirdInfo para el modelIndex: {modelIndex}");
            return null; // O algún Sprite por defecto si lo prefieres
        }
    }
}
