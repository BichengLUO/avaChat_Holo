using UnityEngine;
using System.Collections;

public class FollowBtnClick : MonoBehaviour
{
    public SearchItem parentItem;
    public FriendsListManager friendsListManager;

    void OnClick()
    {
        User user = parentItem.user;
        StartCoroutine(ChatManager.follow(user, () => success()));
    }

    void success()
    {
        friendsListManager = GameObject.Find("FriendsGroup").GetComponent<FriendsListManager>();
        StartCoroutine(ChatManager.getFollowers(
           (friends) => friendsListManager.SetFriends(friends)));
    }
}
