using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCards : MonoBehaviour
{
    public GameObject test;
    public void Spawn(Vector3 position)


    {
        Instantiate(test).transform.position = position;
    }

    // Update is called once per frame
    public void Update()
    {
     //Clique gauche souris
     if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);

            // faire de la position Z la position du prefab
            Vector3 adjustZ = new Vector3(worldPoint.x, worldPoint.y, test.transform.position.z);

            Spawn(adjustZ);

        }
    }
}
