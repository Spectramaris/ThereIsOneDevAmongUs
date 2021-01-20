using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonModeDefensif : MonoBehaviour
{
    private void OnMouseDown()
    {
        transform.parent.transform.parent.GetComponent<BoardMinion>().ActualMode = EveryTypes.BoardMode.Defense;
        Destroy(transform.parent.gameObject);
    }
}