using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoucouWhenPressed : MonoBehaviour
{ 
// To use this example, attach this script to an empty GameObject.
// Create three buttons (Create>UI>Button). Next, select your
// empty GameObject in the Hierarchy and click and drag each of your
// Buttons from the Hierarchy to the Your First Button, Your Second Button
// and Your Third Button fields in the Inspector.
// Click each Button in Play Mode to output their message to the console.
// Note that click means press down and then release.


    //Make sure to attach these Buttons in the Inspector
    public Button m_Deck1, m_Deck2, m_Deck3, m_Deck4, m_Deck5;

    void Start()
    {
        //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
   
        m_Deck1.onClick.AddListener(() => ButtonClicked(1));
        m_Deck1.onClick.AddListener(TaskOnClick);
        m_Deck2.onClick.AddListener(() => ButtonClicked(2));
        m_Deck2.onClick.AddListener(TaskOnClick);
        m_Deck3.onClick.AddListener(() => ButtonClicked(3));
        m_Deck3.onClick.AddListener(TaskOnClick);
        m_Deck4.onClick.AddListener(() => ButtonClicked(4));
        m_Deck4.onClick.AddListener(TaskOnClick);
        m_Deck5.onClick.AddListener(() => ButtonClicked(5));
        m_Deck5.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        //Output this to console when Button1 or Button3 is clicked
        Debug.Log("You have clicked the button!");
    }

    void TaskWithParameters(string message)
    {
        //Output this to console when the Button2 is clicked
        Debug.Log(message);
    }

    void ButtonClicked(int buttonNo)
    {
        //Output this to console when the Button3 is clicked
        Debug.Log("Tu as choisi le deck = " + buttonNo);
    }
}
