using UnityEngine;
using System.Collections;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;

public class SpeakBtnClick : MonoBehaviour
{
    public bool isListening = false;
    public TextBox textBox;
    public Text buttonText;
    private DictationRecognizer dictationRecognizer;
    private Color originalColor;

    void Start()
    {
        dictationRecognizer = new DictationRecognizer();
        dictationRecognizer.DictationResult += (text, confidence) =>
        {
            textBox.text = text;
            textBox.uiText.text = text;
            Debug.LogFormat("Dictation result: {0}", text);
        };

        dictationRecognizer.DictationHypothesis += (text) =>
        {
            textBox.text = text;
            textBox.uiText.text = text;
            Debug.LogFormat("Dictation hypothesis: {0}", text);
        };

        dictationRecognizer.DictationComplete += (completionCause) =>
        {
            if (completionCause != DictationCompletionCause.Complete)
                Debug.LogErrorFormat("Dictation completed unsuccessfully: {0}.", completionCause);
        };

        dictationRecognizer.DictationError += (error, hresult) =>
        {
            Debug.LogErrorFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
        };
    }

    void OnDestroy()
    {
        dictationRecognizer.Dispose();
    }

    void OnClick()
    {
        if (isListening)
        {
            Renderer rend = GetComponent<Renderer>();
            rend.material.color = originalColor;
            buttonText.text = "Listen";

            dictationRecognizer.Stop();
            isListening = false;
        }
        else
        {
            textBox.text = "";
            textBox.uiText.text = "";
            Renderer rend = GetComponent<Renderer>();
            originalColor = rend.material.color;
            rend.material.color = new Color(0.8f, 0f, 0f);
            buttonText.text = "Stop";

            dictationRecognizer.Start();
            isListening = true;
            StartCoroutine(AutoStop());
        }
    }

    IEnumerator AutoStop()
    {
        yield return new WaitForSeconds(5.0f);
        if (isListening)
        {
            Renderer rend = GetComponent<Renderer>();
            rend.material.color = originalColor;
            buttonText.text = "Listen";

            dictationRecognizer.Stop();
            isListening = false;
        }
    }
}
