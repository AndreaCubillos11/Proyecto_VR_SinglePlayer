using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MemoryMathGame : MonoBehaviour
{
    public GameObject book;
    public GameObject sequencePanel;
    public Text sequenceText;
    public InputField inputField;
    public Button submitButton;

    private List<string> sequence;
    private bool isGameActive = false;

    void Start()
    {
        sequencePanel.SetActive(false);
        inputField.gameObject.SetActive(false);
        submitButton.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isGameActive)
        {
            StartGame();
        }
    }

    void StartGame()
    {
        isGameActive = true;
        sequence = GenerateSequence();
        sequenceText.text = string.Join(", ", sequence);
        sequencePanel.SetActive(true);
        StartCoroutine(ShowSequence());
    }

    IEnumerator ShowSequence()
    {
        yield return new WaitForSeconds(5);
        sequencePanel.SetActive(false);
        inputField.gameObject.SetActive(true);
        submitButton.gameObject.SetActive(true);
    }

    public void OnSubmit()
    {
        string playerInput = inputField.text;
        string[] playerSequence = playerInput.Split(',');

        if (SequenceEquals(sequence.ToArray(), playerSequence))
        {
            Debug.Log("Correct!");
            // Aquí puedes agregar lógica para avanzar al siguiente nivel o mostrar una pantalla de éxito.
        }
        else
        {
            Debug.Log("Incorrect!");
            // Aquí puedes agregar lógica para mostrar una pantalla de error o reiniciar el juego.
        }
    }

    List<string> GenerateSequence()
    {
        List<string> seq = new List<string>();
        int length = Random.Range(3, 6); // Longitud de la secuencia
        for (int i = 0; i < length; i++)
        {
            int num = Random.Range(1, 10);
            string operation = Random.Range(0, 2) == 0 ? "+" : "-";
            int result = num + (operation == "+" ? Random.Range(1, 10) : -Random.Range(1, 10));
            seq.Add($"{num} {operation} {result}");
        }
        return seq;
    }

    bool SequenceEquals(string[] seq1, string[] seq2)
    {
        if (seq1.Length != seq2.Length)
            return false;

        for (int i = 0; i < seq1.Length; i++)
        {
            if (seq1[i] != seq2[i])
                return false;
        }
        return true;
    }
}
