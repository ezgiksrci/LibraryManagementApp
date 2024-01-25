using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditBook : MonoBehaviour
{
    private SelectBook selectBook;
    private BookSO editableBookSO;

    private void OnEnable()
    {
        selectBook.OnBookActionButtonClick += SelectBook_OnBookActionButtonClick;
    }

    private void OnDisable()
    {
        selectBook.OnBookActionButtonClick -= SelectBook_OnBookActionButtonClick;
    }

    private void SelectBook_OnBookActionButtonClick(BookSO selectedBookSO)
    {
        editableBookSO = selectedBookSO;
        editableBookSO.title = "zzzzzzzzzzzzzz";
    }
}
