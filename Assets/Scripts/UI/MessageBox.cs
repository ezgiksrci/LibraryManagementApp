using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    public static MessageBox Instance { get; private set; }

    [Header("Warning Message Box:")]
    [SerializeField] GameObject warningPanel;
    [SerializeField] Text warningMessageText;

    [Header("Confirmation Message Box:")]
    [SerializeField] GameObject confirmationPanel;
    [SerializeField] Text confirmationMessageText;
    private Action confirmAction;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        warningPanel.SetActive(false);
        confirmationPanel.SetActive(false);
    }

    public void ShowWarningPanel(string message)
    {
        warningPanel.SetActive(true);
        warningMessageText.text = message;
    }

    public void ShowConfirmationPanel(string message, Action confirmAction)
    {
        confirmationPanel.SetActive(true);
        confirmationMessageText.text = message;
        this.confirmAction = confirmAction;
    }

    public void OnClickConfirmButton()
    {
        confirmAction();
        confirmationPanel.SetActive(false);
    }

    private void OnDisable()
    {
        warningMessageText.text = "";
        confirmationMessageText.text = "";
    }
}
