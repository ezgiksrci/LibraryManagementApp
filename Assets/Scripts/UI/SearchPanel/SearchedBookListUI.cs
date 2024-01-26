using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchedBookListUI : MonoBehaviour
{
    [SerializeField] RectTransform libraryContent;
    [SerializeField] SearchBook searchButton;
    [SerializeField] Transform bookTemplate;


    public event Action<BookSO> OnBookFind;

    private void OnEnable()
    {
        searchButton.OnSearchButtonClicked += SearchButton_OnSearchButtonClicked;
    }


    private void OnDisable()
    {
        searchButton.OnSearchButtonClicked -= SearchButton_OnSearchButtonClicked;
    }

    private void Start()
    {
        ClearTheList();
    }


    private void SearchButton_OnSearchButtonClicked(List<BookSO> searchResults)
    {
        UpdateVisual(searchResults);
    }

    private void UpdateVisual(List<BookSO> searchResults)
    {
        ClearTheList();

        if (searchResults == null)
        {
            Debug.Log("No matching books found.");
            return;
        }

        if (searchResults.Count <= 4)
        {
            libraryContent.sizeDelta = new Vector2(0, 400);
        }
        else if (searchResults.Count > 4)
        {
            libraryContent.sizeDelta = new Vector2(0, searchResults.Count * 100 + 100);
        }

        foreach (BookSO book in searchResults)
        {
            GameObject newBook = Instantiate(bookTemplate.gameObject, libraryContent);

            SearchedBookFieldsUI searchedBookFieldsUI = newBook.transform.GetComponent<SearchedBookFieldsUI>();
            searchedBookFieldsUI.ISBN_LabelText.text = book.ISBN;
            searchedBookFieldsUI.title_LabelText.text = book.title;
            searchedBookFieldsUI.author_LabelText.text = book.author;
            searchedBookFieldsUI.overdueDate_LabelText.text = book.isAvailable ? "Not borrowed" : book.dueDate.ToShortDateString();
            searchedBookFieldsUI.isOverdue_LabelText.text = book.isOverdued ? "Yes" : "No";

            newBook.SetActive(true);

            OnBookFind?.Invoke(book);
        }
    }

    private void ClearTheList()
    {
        foreach (Transform child in libraryContent.transform)
        {
            if (child == bookTemplate) continue;

            Destroy(child.gameObject);
        }
    }
}
