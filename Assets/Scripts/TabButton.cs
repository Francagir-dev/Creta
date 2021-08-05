using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, ISelectHandler
{
    public Button ThisTabButton
    {
        get => thisTabButton;
        set => thisTabButton = value;
    }

    [SerializeField] private TabGroup tabGroup;

    [SerializeField] private Button thisTabButton;

    // Start is called before the first frame update
    void Start()
    {
        thisTabButton = GetComponent<Button>();
        tabGroup.Subscribe(this);
    }


    public void OnSelect(BaseEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

  
}