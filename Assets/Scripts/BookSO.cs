using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBook", menuName = "Library/Book")]
public class BookSO : ScriptableObject
{
    public LibrarySO librarySO;

    public string ISBN;
    public string title;
    public string author;
    public string publisher;
    public int pageCount;
    public bool isAvailable = true;
    public string borrowerName;
    public System.DateTime dueDate;
    public bool isOverdued = false;

    public static event Action OnBookDelete;



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

    public void DeleteBook()
    {
        ScriptableObject scriptableObjectToDelete = this;
        string assetPath = AssetDatabase.GetAssetPath(scriptableObjectToDelete);

        librarySO.bookSOList.Remove(this);
        
        OnBookDelete?.Invoke();

        // Delete the asset file
        AssetDatabase.DeleteAsset(assetPath);

        // Destroy the ScriptableObject instance
        DestroyImmediate(scriptableObjectToDelete, true);

    }
}