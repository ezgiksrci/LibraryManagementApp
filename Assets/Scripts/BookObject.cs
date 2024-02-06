using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// bookObject - bookSO pair for search list items...
public class BookObject : MonoBehaviour
{
    [SerializeField] SearchedBookListUI searchedBooklistUI;
    [SerializeField] KeyValuePair<string, Book> book;
    public static KeyValuePair<string, Book> selectedBook;

    public static event Action OnBookSelected;

    private void OnEnable()
    {
        searchedBooklistUI.OnBookFind += SearchedBooklistUI_OnBookFind;
    }

    private void OnDisable()
    {
        searchedBooklistUI.OnBookFind -= SearchedBooklistUI_OnBookFind;
    }

    private void SearchedBooklistUI_OnBookFind(KeyValuePair<string, Book> book)
    {
        if (book.Equals(default))
        {
            this.book = book;
        }
    }

    public void SetAsSelectedBook()
    {
        if (!selectedBook.Equals(default) || !selectedBook.Equals(book))
        {
            selectedBook = book;
            OnBookSelected?.Invoke();
        }
    }

    public static KeyValuePair<string, Book> GetSelectedBookObject()
    {
        return selectedBook;
    }

    public static void ClearSelectedBookObject()
    {
        selectedBook = default;
    }
}
