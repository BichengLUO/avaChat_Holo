using UnityEngine;
using System.Collections;

public class MessageBox : MonoBehaviour {
    public Bubble bubble;
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
            bubble.message = value;
        }
    }
}
