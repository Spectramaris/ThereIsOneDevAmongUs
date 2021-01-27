using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TransitionMenu1ToMenu2 : MonoBehaviour
{
    public VideoClip clip1;
    public VideoClip clip2;
    public GameObject elementsInterface;

    int actualClip = 0;
    VideoPlayer vp;

    private void Awake()
    {
        
    }

    void Start()
    {
        vp = GetComponent<VideoPlayer>();
        if (EveryTypes.GameIsLaunched)
        {
            vp.clip = clip2;
            elementsInterface.SetActive(true);
            actualClip = 2;
        }
        else
        {
            elementsInterface.SetActive(false);
            vp.loopPointReached += EndReached;
        }
    }

    void EndReached(VideoPlayer _vp)
    {
        vp.clip = actualClip == 0 ? clip1 : clip2;
        elementsInterface.SetActive(actualClip == 1);

        actualClip++;
    }

    void FixedUpdate()
    {
        if(Input.anyKeyDown && actualClip < 2)
        {
            EndReached(vp);
        }
    }
}
