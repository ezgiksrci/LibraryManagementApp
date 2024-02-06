using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class LendBook : BasePage
{
    private CloudSave cloudSave;

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
        var selectedBook = BookObject.GetSelectedBookObject();

        selectedBook.Value.isOverdued = false;
        selectedBook.Value.isAvailable = false;
        selectedBook.Value.borrowerName = borrowerNameInput.text;

        int borrowingPeriod = int.Parse(borrowingPeriodInput.text);
        selectedBook.Value.dueDate = DateTime.Now.AddDays(borrowingPeriod);
        selectedBook.Value.dueDateString = selectedBook.Value.dueDate.ToString();

        cloudSave.UpdateBook(selectedBook);

        //Debug.Log(selectedBook.dueDate);

        // set selectedBook = null
        BookObject.ClearSelectedBookObject();
    }
}
