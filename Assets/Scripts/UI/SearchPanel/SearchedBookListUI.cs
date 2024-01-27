using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchedBookListUI : MonoBehaviour
{
    [SerializeField] RectTransform libraryContent;
    [SerializeField] SearchBook searchBook;
    [SerializeField] Transform bookTemplate;


    public event Action<BookSO> OnBookFind;

    private void OnEnable()
    {
        searchBook.OnSearchButtonClicked += SearchBook_OnSearchButtonClicked;
    }


    private void OnDisable()
    {
        searchBook.OnSearchButtonClicked -= SearchBook_OnSearchButtonClicked;
    }

    private void Start()
    {
        ClearTheList();
    }


    private void SearchBook_OnSearchButtonClicked(List<BookSO> searchResults)
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

        ScaleContentTransformSize(searchResults.Count);

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

    private void ScaleContentTransformSize(int resultCount)
    {
        if (resultCount <= 4)
        {
            libraryContent.sizeDelta = new Vector2(0, 0);
        }
        else if (resultCount > 4)
        {
            libraryContent.sizeDelta = new Vector2(0, resultCount * 50);
        }
    }
}
