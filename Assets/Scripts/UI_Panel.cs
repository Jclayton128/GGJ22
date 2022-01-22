using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UI_Panel : MonoBehaviour
{
    protected List<GameObject> elements = new List<GameObject>();
    protected GameController gcRef;

    protected virtual void Awake()
    {

        FindAllChildElements();
        HideAllChildElements();

    }

    protected virtual void Start()
    {
        gcRef = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void FindAllChildElements()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            elements.Add(transform.GetChild(i).gameObject);
        }
    }
    private void HideAllChildElements()
    {
        foreach (var elem in elements)
        {
            elem.SetActive(false);
        }
    }


    public virtual void ShowHideElements(bool shouldShow)
    {
        foreach (var elem in elements)
        {
            elem.SetActive(shouldShow);
        }
    }
}
