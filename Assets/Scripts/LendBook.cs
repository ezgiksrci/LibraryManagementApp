using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public void LendTheBook()
    {
        BookSO bookSO = BookObject.GetSelectedBookSO();
        string assetPath = AssetDatabase.GetAssetPath(bookSO);

        bookSO.isOverdued = false;
        bookSO.isAvailable = false;
        bookSO.borrowerName = borrowerNameInput.text;

        int borrowingPeriod = int.Parse(borrowingPeriodInput.text);
        bookSO.dueDate = DateTime.Now.AddDays(borrowingPeriod);

        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();

        // Set selectedBookSO = null
        BookObject.ClearSelectedBookSO();
    }
}
