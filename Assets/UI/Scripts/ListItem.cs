using UnityEngine;
using UnityEngine.UI;
using System;

public class ListItem : MonoBehaviour {
    public Text userNameText;
    public Text topicText;
    public GameObject background;
    public Texture[] allThumbnails;
    public Action<User, Conversation> callback;

    private int _thumbID = 0;
    private string _userName = "";
    private string _topic = "";
    private User _user;
    private Conversation _conv;

    public void OnSelect()
    {
        if (callback != null)
            callback(user, conv);
    }

    public void OnGazeEnter()
    {
        background.SetActive(true);
        GetComponent<AudioSource>().Play();
    }

    public void OnGazeLeave()
    {
        background.SetActive(false);
    }

    public User user
    {
        get
        {
            return _user;
        }
        set
        {
            _user = value;
            thumbID = value.charID;
            userName = value.userName;
            topic = "";
        }
    }

    public Conversation conv
    {
        get
        {
            return _conv;
        }
        set
        {
            _conv = value;
            thumbID = value.charId;
            userName = value.name;
            topic = value.topic;
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
