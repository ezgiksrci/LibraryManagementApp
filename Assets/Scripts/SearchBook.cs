using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchBook : BasePage
{
    private CloudSave cloudSave;
    public event Action<Dictionary<string, Book>> OnSearchButtonClicked;

    [SerializeField] TMP_InputField searchInput;
    [SerializeField] string notSelectedWarningMessage;
    [SerializeField] string noRecordWarningMessage;
    [SerializeField] string notAvailabeWarningMessage;
    [SerializeField] string deleteConfirmationMessage;

    public static Dictionary<string, Book> searchResults;

    private void Start()
    {
        searchResults = new Dictionary<string, Book>();
    }

    public void OnClickEditButton()
    {
        if (!BookObject.GetSelectedBookObject().Equals(default))
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
        if (!BookObject.GetSelectedBookObject().Equals(default))
        {
            MessageBox.Instance.ShowWarningPanel(notSelectedWarningMessage);
        }
        else if (!BookObject.GetSelectedBookObject().Value.isAvailable)
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
        if (!BookObject.GetSelectedBookObject().Equals(default))
        {
            MessageBox.Instance.ShowWarningPanel(notSelectedWarningMessage);
        }
        else
        {
            //MessageBox.Instance.ShowConfirmationPanel(deleteConfirmationMessage, BookObject.GetSelectedBookSO().DeleteBook);
        }
    }

    public void SearchinLibrary()
    {
        string searchQuery = searchInput.text.ToLower(); // Convert the search query to lowercase for case-insensitive search

        // Search for books by title, author, or ISBN
        searchResults.Clear();

        foreach (var book in cloudSave.Books)
        {
            if (book.Value.title.ToLower().Contains(searchQuery) ||
                book.Value.author.ToLower().Contains(searchQuery) ||
                book.Value.ISBN.ToLower().Contains(searchQuery))
            {
                if (!book.Value.isAvailable)
                {
                    book.Value.dueDate = DateTime.Parse(book.Value.dueDateString);
                }

                searchResults.Add(book.Key, book.Value);
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

    public void SearchbyBorrowerName()
    {
        if (searchInput.text == null || searchInput.text == "")
        {
            MessageBox.Instance.ShowWarningPanel("Please enter a search query...");
            return;
        }

        string searchQuery = searchInput.text.ToLower();

        // Search for borrower
        searchResults.Clear();

        foreach (var book in cloudSave.Books)
        {
            if ((book.Value.borrowerName.ToLower().Contains(searchQuery)))
            {
                searchResults.Add(book.Key, book.Value);

                if (!book.Value.isAvailable)
                {
                    book.Value.dueDate = DateTime.Parse(book.Value.dueDateString);
                }
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
