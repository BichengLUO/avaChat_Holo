using UnityEngine;
using System.Collections;

public class ProfileManager : MonoBehaviour {
    public GameObject avatarChangerPrefab;
    public GameObject avatarChanger;
    public ListItem selfListItem;

    void Start()
    {
        selfListItem.user = ChatManager.currentUser;
        selfListItem.callback = (u, c) =>
        {
            if (avatarChanger == null)
            {
                avatarChanger = Instantiate(avatarChangerPrefab);
                avatarChanger.GetComponent<AvatarChanger>().charId = ChatManager.currentUser.charID;
            }
        };
    }

    public void Refresh()
    {
        selfListItem.user = ChatManager.currentUser;
    }
}
