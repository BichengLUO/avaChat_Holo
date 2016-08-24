using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour {
    public Text messageText;
    private string _message;
    private float originalY;

    void Start()
    {
        originalY = transform.position.y;
    }

    void Update()
    {
        if (transform.position.y < originalY)
        {
            Vector3 newPosition = transform.position;
            newPosition.y += 0.01f;
            transform.position = newPosition;
        }
    }

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
            float newY = originalY - 0.1f;
            Vector3 newPosition = transform.position;
            newPosition.y = newY;
            transform.position = newPosition;
        }
    }

	void OnSelect()
    {
        
    }
}
