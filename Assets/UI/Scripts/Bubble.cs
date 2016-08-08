using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour {
    public Text messageText;
    private string _message;

    public string message
    {
        get
        {
            return _message;
        }
        set
        {
            _message = value;
            messageText.text = value;
        }
    }

	void OnSelect()
    {
        message = "This is a simple test!";
    }
}
