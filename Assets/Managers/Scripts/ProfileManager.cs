using UnityEngine;
using System.Collections;

public class ProfileManager : MonoBehaviour {
    public User user;
    public GameObject avatarChangerPrefab;
    public GameObject avatarChanger;
    public ListItem selfListItem;

    public void SetUser(User user)
    {
        selfListItem.user = user;
        selfListItem.callback = (u, c) =>
        {
            if (avatarChanger == null)
            {
                avatarChanger = Instantiate(avatarChangerPrefab);
                avatarChanger.GetComponent<AvatarChanger>().charId = user.charID;
            }
        };
    }
}
