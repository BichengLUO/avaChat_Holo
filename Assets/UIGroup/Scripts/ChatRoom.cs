using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChatRoom : MonoBehaviour {
    private Conversation _conv;
    public AvatarBox avatarBox;
    public Bubble bubble;
    public SendBtnClick sendBtnClick;
    public List<Message> lastMessages;

    public Conversation conv
    {
        get
        {
            return _conv;
        }
        set
        {
            _conv = value;
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
                if (messages[i].msgId != lastMessages[lastMessages.Count - 1].msgId)
                    newMessages.Add(messages[i]);
                else
                    break;
            }
        }
        for (int i = 0; i < newMessages.Count; i++)
        {
            bubble.message = newMessages[i].data;
            bubble.enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
        lastMessages = messages;
    }
}
