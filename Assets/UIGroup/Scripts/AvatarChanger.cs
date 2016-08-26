using UnityEngine;
using System.Collections;

public class AvatarChanger : MonoBehaviour {
    private int _charId;
    public GameObject avatarSelector;
    public GameObject[] avatars;

    public int charId
    {
        get
        {
            return _charId;
        }
        set
        {
            _charId = value;
            Vector3 avatarSelectorPos = avatarSelector.transform.position;
            avatarSelectorPos.x = avatars[value].transform.position.x;
            avatarSelector.transform.position = avatarSelectorPos;

            StartCoroutine(ChatManager.updateCharID(value, () => success()));
        }
    }

    void success()
    {
        ProfileManager profileManager = GameObject.Find("ProfileGroup").GetComponent<ProfileManager>();
        profileManager.Refresh();
    }
}
