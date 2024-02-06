using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SearchedBookListUI : MonoBehaviour
{
    private CloudSave cloudSave;

    [SerializeField] RectTransform libraryContent;
    [SerializeField] SearchBook searchBook;
    [SerializeField] Transform bookTemplate; //template for search result item

    public event Action<KeyValuePair<string, Book>> OnBookFind;

    private void OnEnable()
    {
        searchBook.OnSearchButtonClicked += SearchBook_OnSearchButtonClicked;
        //Book.OnBookDelete += SearchBook_OnBookDelete;
        ClearTheList();
    }

    private void OnDisable()
    {
        searchBook.OnSearchButtonClicked -= SearchBook_OnSearchButtonClicked;
        //Book.OnBookDelete -= SearchBook_OnBookDelete;

    }
    private void SearchBook_OnBookDelete()
    {
        UpdateVisual(cloudSave.Books);
    }

    private void Start()
    {
        ClearTheList();
    }


    private void SearchBook_OnSearchButtonClicked(Dictionary<string, Book> searchResults)
    {
        UpdateVisual(searchResults);
    }

    private void UpdateVisual(Dictionary<string, Book> searchResults)
    {
        ClearTheList();

        if (searchResults == null)
        {
            Debug.Log("No matching books found.");
            return;
        }

        ScaleContentTransformSize(searchResults.Count);

        foreach (var book in searchResults)
        {
            GameObject newBook = Instantiate(bookTemplate.gameObject, libraryContent);

            SearchedBookFieldsUI searchedBookFieldsUI = newBook.transform.GetComponent<SearchedBookFieldsUI>();
            searchedBookFieldsUI.ISBN_LabelText.text = book.Value.ISBN;
            searchedBookFieldsUI.title_LabelText.text = book.Value.title;
            searchedBookFieldsUI.author_LabelText.text = book.Value.author;
            searchedBookFieldsUI.publisher_LabelText.text = book.Value.publisher;
            searchedBookFieldsUI.overdueDate_LabelText.text = book.Value.isAvailable ? "Not borrowed" : book.Value.dueDateString.Split()[0];


            if (!book.Value.isAvailable && book.Value.dueDate.Date < DateTime.Today.Date)
            {
                book.Value.isOverdued = true;
                searchedBookFieldsUI.isOverdue_LabelText.text = "Yes";

            }
            else
            {
                book.Value.isOverdued = false;
                searchedBookFieldsUI.isOverdue_LabelText.text = "No";
            }

            if (searchedBookFieldsUI.borrowerName_LabelText != null)
            {
                searchedBookFieldsUI.borrowerName_LabelText.text = book.Value.borrowerName;
            }

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

    // to adjust the size of the content field based on the number of search results
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
