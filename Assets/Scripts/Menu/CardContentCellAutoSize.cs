using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardContentCellAutoSize : MonoBehaviour
{
    public Transform canvasTransform;

    void Start()
    {
        var size = Mathf.Abs(RectTransformUtility.CalculateRelativeRectTransformBounds(canvasTransform, transform).center.x);
        Debug.Log(size);
        GetComponent<GridLayoutGroup>().cellSize.Set(200, 200);
    }
}
