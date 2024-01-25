using UnityEngine;

[CreateAssetMenu(fileName = "NewBook", menuName = "Library/Book")]
public class BookSO : ScriptableObject
{
    public string ISBN;
    public string title;
    public string author;
    public string publisher;
    public int pageCount;
    public bool isAvailable = true;
    public string borrowerName;
    public System.DateTime dueDate;


    // Method to set the book as borrowed
    public void LendBook(string borrower, System.DateTime dueDate)
    {
        isAvailable = false;
        this.borrowerName = borrower;
        this.dueDate = dueDate;
    }

    // Method to return the book
    public void ReturnBook()
    {
        isAvailable = true;
        borrowerName = "";
        dueDate = System.DateTime.MinValue; // Reset due date
    }
}