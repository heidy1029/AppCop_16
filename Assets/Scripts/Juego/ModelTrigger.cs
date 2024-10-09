using UnityEngine;
using UnityEngine.UI;

public class ModelTrigger : MonoBehaviour
{
    [SerializeField] private int _birdType;
    [SerializeField] private int _modelIndex;
    [SerializeField] private Image _modelImage;
    [SerializeField] private Image _modelImage2;




    public void Configure(Sprite sprite, int birdType, int triviaId)
    {
        _modelImage.sprite = sprite;
        _modelImage2.sprite = sprite;
        _birdType = birdType;
        _modelIndex = triviaId;

        TriviaManager.Instance.AddModel(_birdType, _modelIndex); // Agrega el modelo al diccionario
    }

    private void Update()
    {
        transform.Rotate(Vector3.right * 30 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {


            Destroy(this.gameObject); // Destruye el trigger
            TriviaManager.Instance.LoadQuestions(_modelIndex); // Inicia la trivia con el modelIndex correcto
        }
    }

}