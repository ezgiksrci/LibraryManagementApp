using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReturnBook : MonoBehaviour
{
    [SerializeField] string needToSelectWarningMessage;
    [SerializeField] string bookNotBorrowedMessage;
    [SerializeField] string periodExtentionConfirmationMessage;
    [SerializeField] string successMessage;

    public void OnClickReturnButton()
    {
        if (BookObject.GetSelectedBookSO() != null && !BookObject.GetSelectedBookSO().isAvailable)
        {
            BookObject.GetSelectedBookSO().borrowerName = "";
            BookObject.GetSelectedBookSO().isOverdued = false;
            BookObject.GetSelectedBookSO().isAvailable = true;
            BookObject.GetSelectedBookSO().dueDate = DateTime.MinValue;

            MessageBox.Instance.ShowWarningPanel(successMessage);
        }
        else if (BookObject.GetSelectedBookSO().isAvailable)
        {
            MessageBox.Instance.ShowWarningPanel(bookNotBorrowedMessage);
        }
        else
        {
            MessageBox.Instance.ShowWarningPanel(needToSelectWarningMessage);
        }
    }

    public void OnClickExtendPeriodButton()
    {
        if (BookObject.GetSelectedBookSO() != null && !BookObject.GetSelectedBookSO().isAvailable)
        {
            MessageBox.Instance.ShowConfirmationPanel(periodExtentionConfirmationMessage, ExtendBookPeriod);
        }
        else if (BookObject.GetSelectedBookSO().isAvailable)
        {
            MessageBox.Instance.ShowWarningPanel(bookNotBorrowedMessage);
        }
        else
        {
            MessageBox.Instance.ShowWarningPanel(needToSelectWarningMessage);
        }

    }

    private void ExtendBookPeriod()
    {
        BookObject.GetSelectedBookSO().dueDate.AddDays(10);
        MessageBox.Instance.ShowWarningPanel(successMessage);
    }
}
