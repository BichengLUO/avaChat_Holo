using UnityEngine;
using UnityEngine.UI;

public class Label : MonoBehaviour {
    private string _text;
    public Text uiText;

    public string text
    {
        get
        {
            return _text;
        }
        set
        {
            _text = value;
            uiText.text = value;
        }
    }
}
