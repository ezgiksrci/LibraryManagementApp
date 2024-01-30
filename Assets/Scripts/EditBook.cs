using System;
using TMPro;
using UnityEngine;

public class EditBook : BasePage
{
    [SerializeField] TMP_InputField ISBNInput;
    [SerializeField] TMP_InputField titleInput;
    [SerializeField] TMP_InputField authorInput;
    [SerializeField] TMP_InputField pageCountInput;
    [SerializeField] TMP_InputField publisherInput;

    [SerializeField] string warningMessage;

    [SerializeField] LibrarySO librarySO;

    private void OnEnable()
    {
        FillSelectedBookInfo();
    }

    private void OnDisable()
    {
        ClearInputFields();
    }

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
        try
        {
            UpdateTheBook();

        }
        catch (Exception)
        {
            MessageBox.Instance.ShowWarningPanel(warningMessage);
        }
    }

    private void UpdateTheBook()
    {
        // update BookSO datas
        BookSO selectedBookSO = BookObject.GetSelectedBookSO();
        
        selectedBookSO.title = titleInput.text;
        selectedBookSO.author = authorInput.text;
        selectedBookSO.pageCount = int.Parse(pageCountInput.text);
        selectedBookSO.publisher = publisherInput.text;

        UnityEditor.EditorUtility.SetDirty(selectedBookSO);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();

        // set selectedBookSO = null
        BookObject.ClearSelectedBookSO();

        ClearInputFields();

        PageManager.Instance.searchBookPage.SetActive(true);
        gameObject.SetActive(false);
    }

    private void ClearInputFields()
    {
        // clear input fields
        ISBNInput.text = "";
        titleInput.text = "";
        authorInput.text = "";
        pageCountInput.text = "";
        publisherInput.text = "";
    }
}
