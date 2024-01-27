using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchBook : BasePage
{
    public event Action<List<BookSO>> OnSearchButtonClicked;

    [SerializeField] TMP_InputField searchInput;
    [SerializeField] LibrarySO librarySO;

    public static List<BookSO> searchResults;

    private void Start()
    {
        EditBook.OnPanelOpen += EditBook_OnPanelOpen;
        EditBook.OnPanelClose += EditBook_OnPanelClose;

        searchResults = new List<BookSO>();
    }

    private void OnDestroy()
    {
        EditBook.OnPanelOpen -= EditBook_OnPanelOpen;
        EditBook.OnPanelClose -= EditBook_OnPanelClose;
    }

    private void EditBook_OnPanelClose(object sender, EventArgs e)
    {
        ShowPanel();
    }

    private void EditBook_OnPanelOpen(object sender, EventArgs e)
    {
        HidePanel();
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
            // Debug.Log("Search Results:");

            OnSearchButtonClicked?.Invoke(searchResults);

        }
        else
        {
            // Debug.LogWarning("No matching books found.");

            OnSearchButtonClicked?.Invoke(null);
        }

        // Clear input field
        searchInput.text = "";
    }
}
