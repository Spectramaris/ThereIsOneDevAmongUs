using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardUIResumeDragNDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    bool dragged = false;
    Vector3 initialPosition;

    private void Update()
    {
        if (dragged)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SetMaskable(false);
        initialPosition = transform.position;
        dragged = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SetMaskable(true);
        transform.position = initialPosition;
        dragged = false;

        var eventDa = new PointerEventData(EventSystem.current);
        eventDa.position = Input.mousePosition;
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDa, results);
        var onDeck = results.Where(x => x.gameObject.name == "Zone Deck").FirstOrDefault();

        if (!onDeck.isValid)
        {
            GameObject.Find("Managers").GetComponent<DecksManager>().RemoveCardToDeck(GetComponent<CardPrefabUIResume>().minionSO);
        }
    }

    private void SetMaskable(bool maskable)
    {
        GetComponent<Image>().maskable = maskable;
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().maskable = maskable;
        transform.GetChild(1).GetComponent<Image>().maskable = maskable;
        transform.GetChild(2).GetComponent<TextMeshProUGUI>().maskable = maskable;
    }

    public void OnDrag(PointerEventData eventData)
    {
    }
}
