using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float TransitionTime = 1f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LoadNextScreen();

        }
    }

    public void LoadNextScreen()

    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadScene(int SceneIndex)
    {
        // Play Animation

        transition.SetTrigger("Start");

        // Wait

        yield return new WaitForSeconds(TransitionTime);

        
        // Load Scene

        SceneManager.LoadScene(SceneIndex);
    }

}
