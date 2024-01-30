using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class LendBook : BasePage
{
    [Header("Selected Book Infos")]
    [SerializeField] TextMeshProUGUI ISBNText;
    [SerializeField] TextMeshProUGUI bookTitleText;
    [SerializeField] TextMeshProUGUI bookAuthorText;

    [Header("Borrowing Infos")]
    [SerializeField] TMP_InputField borrowerNameInput;
    [SerializeField] TMP_InputField borrowingPeriodInput;

    // book lending process with borrower name and borrowing period
    public void LendTheBook()
    {
        BookSO selectedBookSO = BookObject.GetSelectedBookSO();

        selectedBookSO.isOverdued = false;
        selectedBookSO.isAvailable = false;
        selectedBookSO.borrowerName = borrowerNameInput.text;

        int borrowingPeriod = int.Parse(borrowingPeriodInput.text);
        selectedBookSO.dueDate = DateTime.Now.AddDays(borrowingPeriod);
        selectedBookSO.dueDateString = selectedBookSO.dueDate.ToString();

        UnityEditor.EditorUtility.SetDirty(selectedBookSO);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();

        //Debug.Log(selectedBookSO.dueDate);

        // set selectedBookSO = null
        BookObject.ClearSelectedBookSO();
    }
}
