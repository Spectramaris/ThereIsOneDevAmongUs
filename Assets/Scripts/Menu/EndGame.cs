using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class EndGame : MonoBehaviour
{
    VideoPlayer  vp;

    void Start()
    {
        vp = GetComponent<VideoPlayer>();
        vp.loopPointReached += EndReached;
    }

    void EndReached(VideoPlayer _vp)
    {
        EveryTypes.GameIsLaunched = true;
        SceneManager.LoadScene(0);
    }
}
