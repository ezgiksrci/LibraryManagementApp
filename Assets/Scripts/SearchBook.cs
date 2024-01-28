using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchBook : BasePage
{
    [SerializeField] LibrarySO librarySO;

    public event Action<List<BookSO>> OnSearchButtonClicked;

    [SerializeField] TMP_InputField searchInput;
    [SerializeField] string notSelectedWarningMessage;
    [SerializeField] string noRecordWarningMessage;
    [SerializeField] string notAvailabeWarningMessage;
    [SerializeField] string deleteConfirmationMessage;

    public static List<BookSO> searchResults;

    private void Start()
    {
        searchResults = new List<BookSO>();
    }

    public void OnClickEditButton()
    {
        if (BookObject.GetSelectedBookSO() != null)
        {
            PageManager.Instance.editBookPage.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            MessageBox.Instance.ShowWarningPanel(notSelectedWarningMessage);
        }
    }

    public void OnClickLendButton()
    {
        if (BookObject.GetSelectedBookSO() == null)
        {
            MessageBox.Instance.ShowWarningPanel(notSelectedWarningMessage);
        }
        else if (!BookObject.GetSelectedBookSO().isAvailable)
        {
            MessageBox.Instance.ShowWarningPanel(notAvailabeWarningMessage);
        }
        else
        {
            PageManager.Instance.lendBookPage.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void OnClickDeleteButton()
    {
        if (BookObject.GetSelectedBookSO() == null)
        {
            MessageBox.Instance.ShowWarningPanel(notSelectedWarningMessage);
        }
        else
        {
            MessageBox.Instance.ShowConfirmationPanel(deleteConfirmationMessage, BookObject.GetSelectedBookSO().DeleteBook);
        }
    }

    public void SearchinLibrary()
    {
        string searchQuery = searchInput.text.ToLower(); // Convert the search query to lowercase for case-insensitive search

        // Search for books by title, author, or ISBN
        searchResults.Clear();

        foreach (var book in librarySO.bookSOList)
        {
            if (book.title.ToLower().Contains(searchQuery) ||
                book.author.ToLower().Contains(searchQuery) ||
                book.ISBN.ToLower().Contains(searchQuery))
            {
                searchResults.Add(book);
            }
        }

        // Display search results
        if (searchResults.Count > 0)
        {

            OnSearchButtonClicked?.Invoke(searchResults);

        }
        else
        {
            MessageBox.Instance.ShowWarningPanel(noRecordWarningMessage);

            OnSearchButtonClicked?.Invoke(null);
        }

        // Clear input field
        searchInput.text = "";
    }
}
