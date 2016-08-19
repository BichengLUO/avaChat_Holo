﻿using UnityEngine;
using System.Collections;

public class AvatarBox : MonoBehaviour {
    private int _charID = 0;
    public GameObject[] allAvatars;
    public GameObject currentAvatar;

    public int charID
    {
        get
        {
            return _charID;
        }
        set
        {
            _charID = value;
            currentAvatar = Instantiate(allAvatars[value]);
        }
    }
}
