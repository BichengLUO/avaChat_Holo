using UnityEngine;
using UnityEngine.UI;

public class ListItem : MonoBehaviour {
    public Text userNameText;
    public Text topicText;
    public Texture[] allThumbnails;
    public User user;
    public Conversation conv;

    private int _thumbID = 0;
    private string _userName = "";
    private string _topic = "";

    public void OnSelect()
    {
        if (conv != null)
        {

        }
        else if (user != null)
        {

        }
    }

    public int thumbID
    {
        get
        {
            return _thumbID;
        }
        set
        {
            _thumbID = value;
            Renderer rend = GetComponent<Renderer>();
            rend.material.SetTexture("_MainTex", allThumbnails[value]);
        }
    }

    public string userName
    {
        get
        {
            return _userName;
        }
        set
        {
            _userName = value;
            userNameText.text = value;
        }
    }

    public string topic
    {
        get
        {
            return _topic;
        }
        set
        {
            _topic = value;
            topicText.text = value;
        }
    }
}
