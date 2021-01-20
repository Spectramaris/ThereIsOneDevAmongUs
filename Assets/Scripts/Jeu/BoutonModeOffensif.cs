using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonModeOffensif : MonoBehaviour
{
    private void OnMouseDown()
    {
        transform.parent.transform.parent.GetComponent<BoardMinion>().ActualMode = EveryTypes.BoardMode.Attaque;
        Destroy(transform.parent.gameObject);
    }
}