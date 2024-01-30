using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReturnBook : MonoBehaviour
{
    [Header("Popup Message Box Messages:")]
    [SerializeField] string needToSelectWarningMessage;
    [SerializeField] string bookNotBorrowedMessage;
    [SerializeField] string periodExtentionConfirmationMessage;
    [SerializeField] string successMessage;

    // selected book returning
    public void OnClickReturnButton()
    {
        BookSO selectedBookSO = BookObject.GetSelectedBookSO();

        if (selectedBookSO != null && !selectedBookSO.isAvailable)
        {
            selectedBookSO.borrowerName = "";
            selectedBookSO.isOverdued = false;
            selectedBookSO.isAvailable = true;
            selectedBookSO.dueDate = DateTime.MinValue;
            selectedBookSO.dueDateString = null;

            UnityEditor.EditorUtility.SetDirty(selectedBookSO);
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();

            // set selectedBookSO = null
            BookObject.ClearSelectedBookSO();

            MessageBox.Instance.ShowWarningPanel(successMessage);
        }
        // book is not borrowed
        else if (selectedBookSO.isAvailable)
        {
            MessageBox.Instance.ShowWarningPanel(bookNotBorrowedMessage);
        }
        // there is not a selected book
        else
        {
            MessageBox.Instance.ShowWarningPanel(needToSelectWarningMessage);
        }
    }

    // lended book's period extend button method
    public void OnClickExtendPeriodButton()
    {
        BookSO selectedBookSO = BookObject.GetSelectedBookSO();

        if (selectedBookSO != null && !selectedBookSO.isAvailable)
        {
            MessageBox.Instance.ShowConfirmationPanel(periodExtentionConfirmationMessage, ExtendBookPeriod);
        }
        else if (selectedBookSO.isAvailable)
        {
            MessageBox.Instance.ShowWarningPanel(bookNotBorrowedMessage);
        }
        else
        {
            MessageBox.Instance.ShowWarningPanel(needToSelectWarningMessage);
        }
    }

    // extend the selected and lended book's due date for 10 days...
    private void ExtendBookPeriod()
    {
        BookSO selectedBookSO = BookObject.GetSelectedBookSO();

        selectedBookSO.dueDate = selectedBookSO.dueDate.AddDays(10f);
        selectedBookSO.dueDateString = selectedBookSO.dueDate.ToString();

        UnityEditor.EditorUtility.SetDirty(selectedBookSO);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();

        // set selectedBookSO = null
        BookObject.ClearSelectedBookSO();

        MessageBox.Instance.ShowWarningPanel(successMessage);
    }
}
