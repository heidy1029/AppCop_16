using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    public List<ModelQuestions> allModelQuestions = new List<ModelQuestions>
    {
        new ModelQuestions
        {
            modelIndex = 0,
            questions = new List<Question>
            {
                    new Question{ questionText = "¿Cuál es una característica distintiva del macho de la especie Green-crowned Woodnymph (Thalurania fannyi)?",
                        answers = new List<string> { "A) La corona del macho es de un verde brillante", "B) La cola del macho es completamente negra", "C) El pecho del macho es de color rojo brillante", "D) La garganta del macho es de color gris" },
                        correctAnswerIndex = 0},

                    new Question { questionText = "¿Dónde suelen habitar las ninfas del bosque (Thalurania)?",
                        answers = new List<string> { "A) En áreas abiertas y con matorrales", "B) En el sotobosque de bosques húmedos de tierras bajas y en bosques secundarios avanzados adyacentes", "C) En desiertos y áreas áridas", "D) En regiones montañosas y áreas secas" },
                        correctAnswerIndex = 1},

                    new Question {questionText = "¿Qué tipo de dieta tienen las ninfas del bosque (Thalurania)?",
                        answers = new List<string> { "A) Se alimentan principalmente de insectos y frutas", "B) Prefieren flores de una amplia variedad de epífitas, arbustos y árboles pequeños, y ocasionalmente recogen artrópodos", "C) Se alimentan exclusivamente de néctar de flores grandes como Heliconia y Costus", "D) Consumen solo néctar de flores de grandes árboles como Inga" },
                        correctAnswerIndex = 2}
            }
        },
        new ModelQuestions
        {
            modelIndex = 1,
            questions = new List<Question>
                {
                    new Question
                    {
                        questionText = "¿Cuál es la característica distintiva del colibrí pecho leonado que lo diferencia de otros colibríes de tamaño similar?",
                        answers = new List<string> { "A) Es completamente verde por encima y beige por debajo", "B) Tiene un color rojo intenso por debajo y verde en la parte superior", "C) Su pecho es de color rosa brillante", "D) Su pico es más largo y curvo que el de otros colibríes" },
                        correctAnswerIndex = 0
                    },
                    new Question
                    {
                        questionText = "¿En qué tipo de hábitat es más común encontrar al colibrí pecho leonado?",
                        answers = new List<string> { "A) En los bordes de los bosques abiertos.", "B) En los bosques secos y áridos", "C) En los bosques húmedos de montaña", "D) En los campos de cultivo y praderas" },
                        correctAnswerIndex = 2
                    },
                    new Question
                    {
                        questionText = "¿Qué característica específica en la garganta del macho adulto del colibrí pecho leonado puede ayudar a identificarlo?",
                        answers = new List<string> { "A) Una mancha rosada en el centro de la garganta","B) Una mancha negra en la garganta", "C) Un color verde brillante en la garganta", "D) Una banda blanca en la garganta" },
                        correctAnswerIndex = 0
                    }
                }
        },
        new ModelQuestions
        {
                modelIndex = 2,
                questions = new List<Question>
                {
                    new Question
                    {
                        questionText = "¿Dónde suelen buscar alimento los colibríes de garganta púrpura?",
                        answers = new List<string> { "A) En el suelo y en áreas abiertas", "B) En las copas de los árboles en flor, donde los machos defienden los territorios florales", "C) En los matorrales y arbustos bajos", "D) En las áreas rocosas cerca de corrientes de agua" },
                        correctAnswerIndex = 1
                    },
                    new Question
                    {
                        questionText = "¿Cuál es una característica distintiva de los machos de colibrí de garganta púrpura en vuelo?",
                        answers = new List<string> { "A) Tienen una cola recta y sin bifurcar", "B) Presentan manchas blancas en los flancos que se pueden ver en vuelo", "C) Su garganta es de color verde brillante.","D) El pecho es de color rojo intenso" },
                        correctAnswerIndex = 1
                    },
                    new Question
                    {
                        questionText = "Cómo es el nido de los colibríes de garganta púrpura?",
                        answers = new List<string> { "A) Es una estructura grande y abierta hecha de ramitas.", "B) Es un nido en forma de copa hecho de fibra fina y telaraña, ubicado sobre ramas gruesas de árboles altos.", "C) Es un nido colgante hecho de hojas y ramitas.","D) Es una cavidad en un tronco de árbol." },
                        correctAnswerIndex = 1
                    }
                }
        },
            new ModelQuestions
         {
                modelIndex = 3,
                questions = new List<Question>
                {
                    new Question
                    {
                        questionText = "¿Dónde suelen buscar alimento los colibríes de garganta púrpura?",
                        answers = new List<string> { "A) En el suelo y en áreas abiertas.", "B) En las copas de los árboles en flor, donde los machos defienden los territorios florales.", "C) En los matorrales y arbustos bajos.","D) En las áreas rocosas cerca de corrientes de agua" },
                        correctAnswerIndex = 1
                    },
                    new Question
                    {
                        questionText = "¿Cuál es una característica distintiva de los machos de colibrí de garganta púrpura en vuelo?",
                        answers = new List<string> { "A) Tienen una cola recta y sin bifurcar.", "B) Presentan manchas blancas en los flancos que se pueden ver en vuelo.", "C) Su garganta es de color verde brillante.","D) El pecho es de color rojo intenso" },
                        correctAnswerIndex = 1
                    },
                    new Question
                    {
                        questionText = "¿ómo es el nido de los colibríes de garganta púrpura?",
                        answers = new List<string> { "A) Es una estructura grande y abierta hecha de ramitas.", "B) Es un nido en forma de copa hecho de fibra fina y telaraña, ubicado sobre ramas gruesas de árboles altos.", "C) Es un nido colgante hecho de hojas y ramitas.","D) Es una cavidad en un tronco de árbol" },
                        correctAnswerIndex = 1
                    }
                }
        }
};
}