using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBook : MonoBehaviour
{
    public event Action<BookSO> OnBookActionButtonClick;

    [SerializeField] SearchedBookListUI searchedBooklistUI;
    [SerializeField] BookSO bookSO;
    public static BookSO selectedBookSO;

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

    public void OnBookSelectButtonClick()
    {
        selectedBookSO = bookSO;
    }

    public void OnLendButtonClick()
    {
        selectedBookSO.LendBook("borrower name", DateTime.Today.AddDays(10f));
    }

    public void OnEditButtonClicked()
    {
        OnBookActionButtonClick?.Invoke(selectedBookSO);
    }
}
