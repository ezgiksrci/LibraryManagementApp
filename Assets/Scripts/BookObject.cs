using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookObject : MonoBehaviour
{
    [SerializeField] SearchedBookListUI searchedBooklistUI;
    [SerializeField] BookSO bookSO;
    public static BookSO selectedBookSO;

    public static event Action OnBookSelected;

    private void OnEnable()
    {
        searchedBooklistUI.OnBookFind += SearchedBooklistUI_OnBookFind;
    }

    private void OnDisable()
    {
        searchedBooklistUI.OnBookFind -= SearchedBooklistUI_OnBookFind;
    }

    private void SearchedBooklistUI_OnBookFind(BookSO bookSO)
    {
        if (this.bookSO == null)
        {
            this.bookSO = bookSO;
        }
    }

    public void SetAsSelectedBook()
    {
        if (selectedBookSO == null || selectedBookSO != bookSO)
        {
            selectedBookSO = bookSO;
            OnBookSelected?.Invoke();
        }
    }

    public static BookSO GetSelectedBookSO()
    {
        return selectedBookSO;
    }

    public static void ClearSelectedBookSO()
    {
        selectedBookSO = null;
    }
}
