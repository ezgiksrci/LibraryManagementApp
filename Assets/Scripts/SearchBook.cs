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

    //public static SearchBook Instance { get; private set; }

    //private void Awake()
    //{
    //    Instance = this;
    //}

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
            Debug.Log("There is not a selected book.");
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
