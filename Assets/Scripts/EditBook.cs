using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditBook : BasePage
{
    [SerializeField] TMP_InputField ISBNInput;
    [SerializeField] TMP_InputField titleInput;
    [SerializeField] TMP_InputField authorInput;
    [SerializeField] TMP_InputField pageCountInput;
    [SerializeField] TMP_InputField publisherInput;

    [SerializeField] LibrarySO librarySO;

    public void OnClickEditButton()
    {
        if (BookObject.GetSelectedBookSO() != null)
        {
            ShowPage();
            FillSelectedBookInfo();
        }
        else
        {
            Debug.Log("There is not a selected book.");
        }
    }

    private void FillSelectedBookInfo()
    {
        ISBNInput.text = BookObject.GetSelectedBookSO().ISBN;
        titleInput.text = BookObject.GetSelectedBookSO().title;
        authorInput.text = BookObject.GetSelectedBookSO().author;
        pageCountInput.text = BookObject.GetSelectedBookSO().pageCount.ToString();
        publisherInput.text = BookObject.GetSelectedBookSO().publisher;
    }

    public void UpdateTheBook()
    {
        // Update BookSO datas
        BookObject.GetSelectedBookSO().title = titleInput.text;
        BookObject.GetSelectedBookSO().author = authorInput.text;
        BookObject.GetSelectedBookSO().pageCount = int.Parse(pageCountInput.text);
        BookObject.GetSelectedBookSO().publisher = publisherInput.text;

        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();

        // Set selectedBookSO = null
        BookObject.ClearSelectedBookSO();

        // Clear input fields
        ISBNInput.text = "";
        titleInput.text = "";
        authorInput.text = "";
        pageCountInput.text = "";
        publisherInput.text = "";

        HidePage();
    }
}
