using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class TabGroup : MonoBehaviour
{
    [FormerlySerializedAs("m_TabButtons")] [SerializeField]
    private List<TabButton> tabButtons;

    [SerializeField] private Color tabIdle;
    [SerializeField] private Color tabHover;
    [SerializeField] private Color tabActive;
    [SerializeField] private TabButton selectedTab;
    [SerializeField] private List<GameObject> objectsToSwap;
    private int m_Index = 0;

    public void Subscribe(TabButton button)
    {
        if (tabButtons == null)
            tabButtons = new List<TabButton>();

        tabButtons.Add(button);
        Debug.Log(m_Index);
    }

    /// <summary>
    /// Selected (Es decir si esta siendo seleccionado)
    /// </summary>
    /// <param name="button"></param>
    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        Debug.Log(button.name + " was selected");
    }

    /// <summary>
    /// Not Selected, or exit selection
    /// </summary>
    /// <param name="button"></param>
    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    /// <summary>
    /// Clicked or accepted
    /// </summary>
    /// <param name="button"></param>
    public void OnTabSelected(TabButton button)
    {
        selectedTab = button;
        ResetTabs();
        Debug.Log(button.name + "was accepted");
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
                objectsToSwap[i].SetActive(true);
            else
                objectsToSwap[i].SetActive(false);
        }
    }

    public void ResetTabs()
    {
        foreach (TabButton tabButton in tabButtons)
        {
            if (selectedTab != null && tabButton == selectedTab)
                continue;
        }
    }
}