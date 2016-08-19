using UnityEngine;
using System.Collections;

public class SearchItem : MonoBehaviour {
    public ListItem item;
    private User _user;

    public User user
    {
        get
        {
            return _user;
        }
        set
        {
            _user = value;
            item.thumbID = value.charID;
            item.userName = value.userName;
            item.topic = "";
        }
    }
}
