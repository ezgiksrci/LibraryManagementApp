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

    //public static EditBook Instance { get; private set; }

    //private void Awake()
    //{
    //    Instance = this;
    //}

    private void OnEnable()
    {
        FillSelectedBookInfo();
    }

    private void OnDisable()
    {
        ClearInputFields();
    }

    //private void Start()
    //{
    //    BookObject.OnBookSelected += BookObject_OnBookSelected;
    //}

    //private void OnDestroy()
    //{
    //    BookObject.OnBookSelected -= BookObject_OnBookSelected;
    //}

    //private void BookObject_OnBookSelected()
    //{
    //    FillSelectedBookInfo();
    //}

    private void FillSelectedBookInfo()
    {
        if (BookObject.GetSelectedBookSO() != null)
        {
            ISBNInput.text = BookObject.GetSelectedBookSO().ISBN;
            titleInput.text = BookObject.GetSelectedBookSO().title;
            authorInput.text = BookObject.GetSelectedBookSO().author;
            pageCountInput.text = BookObject.GetSelectedBookSO().pageCount.ToString();
            publisherInput.text = BookObject.GetSelectedBookSO().publisher;
        }
    }

    public void OnUpdateButtonClick()
    {
        //
        // Error handling controls...
        //

        if (true)
        {
            UpdateTheBook();
        }
        else
        {
            Debug.Log("Warning...");
        }
    }

    private void UpdateTheBook()
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

        ClearInputFields();

        PageManager.Instance.searchBookPage.SetActive(true);
        gameObject.SetActive(false);
    }

    private void ClearInputFields()
    {
        // Clear input fields
        ISBNInput.text = "";
        titleInput.text = "";
        authorInput.text = "";
        pageCountInput.text = "";
        publisherInput.text = "";
    }
}
