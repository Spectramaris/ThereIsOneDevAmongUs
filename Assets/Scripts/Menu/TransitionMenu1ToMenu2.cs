using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TransitionMenu1ToMenu2 : MonoBehaviour
{
    public VideoClip clip2;
    public GameObject elementsInterface;

    void Awake()
    {
        elementsInterface.SetActive(false);
    }

    void FixedUpdate()
    {
        if(Input.anyKeyDown)
        {
            GetComponent<Animator>().SetTrigger("MenuChange");
            GetComponent<VideoPlayer>().clip = clip2;
            elementsInterface.SetActive(true);
        }
    }
}
