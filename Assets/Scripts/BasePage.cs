using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePage : MonoBehaviour
{
    [SerializeField] GameObject panel;
    public static event EventHandler OnPanelOpen;
    public static event EventHandler OnPanelClose;


    public void ShowPanel()
    {
        panel.SetActive(true);
        OnPanelOpen?.Invoke(this, EventArgs.Empty);
    }

    public void HidePanel()
    {
        panel.SetActive(false);
        OnPanelClose?.Invoke(this, EventArgs.Empty);
    }
}
