using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChatRoom : MonoBehaviour {
    private Conversation _conv;
    public AvatarBox avatarBox;
    public Bubble bubble;
    public SendBtnClick sendBtnClick;
    public List<Message> lastMessages;
    private bool first = true;

    public Conversation conv
    {
        get
        {
            return _conv;
        }
        set
        {
            _conv = value;
            avatarBox.charID = value.charId;
            sendBtnClick.conv = value;
        }
    }

    public string message
    {
        get
        {
            return bubble.message;
        }
        set
        {
            bubble.message = value;
        }
    }

    IEnumerator Start()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(1);
            yield return StartCoroutine(ChatManager.getChatHistory(conv, (messages) => StartCoroutine(GetMessages(messages))));
        }
    }

    IEnumerator GetMessages(List<Message> messages)
    {
        List<Message> newMessages = new List<Message>();
        if (lastMessages == null || lastMessages.Count == 0)
        {
            newMessages.AddRange(messages);
        }
        else
        {
            for (int i = 0; i < messages.Count; i++)
            {
                if (messages[i].msgId != lastMessages[0].msgId)
                    newMessages.Add(messages[i]);
                else
                    break;
            }
        }
        lastMessages = messages;
        if (first)
            first = false;
        else
        {
            for (int i = newMessages.Count - 1; i >= 0; i--)
            {
                if (newMessages[i].from != ChatManager.currentUser.userName)
                {
                    bubble.message = newMessages[i].data;
                    bubble.gameObject.SetActive(true);
                    string animationName = Words2Anim.convertToAnim(newMessages[i].data);
                    Animator anim = avatarBox.currentAvatar.GetComponent<Animator>();
                    if (anim != null && animationName != null)
                        anim.CrossFade(animationName, 0);
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
    }
}
