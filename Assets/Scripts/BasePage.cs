using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePage : MonoBehaviour
{
    [SerializeField] GameObject page;
    public static event EventHandler OnPageOpen;
    public static event EventHandler OnPageClose;



    public void ShowPage()
    {
        page.SetActive(true);
        OnPageOpen?.Invoke(this, EventArgs.Empty);
    }

    public void HidePage()
    {
        page.SetActive(false);
        OnPageClose?.Invoke(this, EventArgs.Empty);
    }
}
