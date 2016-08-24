using UnityEngine;
using System.Collections;

public class AvatarChangerItemClick : MonoBehaviour {
    public int charId;
    public AvatarChanger avatarChanger;
    public GameObject avatarSelector;
    public GameObject avatarGazor;

    void OnSelect()
    {
        avatarChanger.charId = charId;
    }

    public void OnGazeEnter()
    {
        Animator anim = GetComponent<Animator>();
        if (anim != null)
            anim.CrossFade(string.Format("greet_0{0}", Random.Range(0, 4)), 0);
        GetComponent<AudioSource>().Play();
        Vector3 avatarGazorPos = avatarGazor.transform.position;
        avatarGazorPos.x = transform.position.x;
        avatarGazor.transform.position = avatarGazorPos;
    }

    public void OnGazeLeave()
    {
        Animator anim = GetComponent<Animator>();
        if (anim != null)
            anim.CrossFade("idle_00", 0);
    }
}
