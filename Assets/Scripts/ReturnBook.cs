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
        var selectedBook = BookObject.GetSelectedBookObject();

        if (!BookObject.GetSelectedBookObject().Equals(default) && !selectedBook.Value.isAvailable)
        {
            selectedBook.Value.borrowerName = "";
            selectedBook.Value.isOverdued = false;
            selectedBook.Value.isAvailable = true;
            selectedBook.Value.dueDate = DateTime.MinValue;
            selectedBook.Value.dueDateString = null;

            //UnityEditor.EditorUtility.SetDirty(selectedBook);
            //UnityEditor.AssetDatabase.SaveAssets();
            //UnityEditor.AssetDatabase.Refresh();

            // set selectedBookSO = null
            BookObject.ClearSelectedBookObject();

            MessageBox.Instance.ShowWarningPanel(successMessage);
        }
        // book is not borrowed
        else if (selectedBook.Value.isAvailable)
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
        var selectedBook = BookObject.GetSelectedBookObject();

        if (!BookObject.GetSelectedBookObject().Equals(default) && !selectedBook.Value.isAvailable)
        {
            MessageBox.Instance.ShowConfirmationPanel(periodExtentionConfirmationMessage, ExtendBookPeriod);
        }
        else if (selectedBook.Value.isAvailable)
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
        var selectedBook = BookObject.GetSelectedBookObject();

        selectedBook.Value.dueDate = selectedBook.Value.dueDate.AddDays(10f);
        selectedBook.Value.dueDateString = selectedBook.Value.dueDate.ToString();

        //UnityEditor.EditorUtility.SetDirty(selectedBook);
        //UnityEditor.AssetDatabase.SaveAssets();
        //UnityEditor.AssetDatabase.Refresh();

        // set selectedBookSO = null
        BookObject.ClearSelectedBookObject();

        MessageBox.Instance.ShowWarningPanel(successMessage);
    }
}
