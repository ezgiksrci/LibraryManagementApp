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
    public DateTime dueDate;
    public bool isOverdued = false;

    public static event Action OnBookDelete;

    public void DeleteBook()
    {
        ScriptableObject scriptableObjectToDelete = this;
        string assetPath = AssetDatabase.GetAssetPath(scriptableObjectToDelete);

        librarySO.bookSOList.Remove(this);

        OnBookDelete?.Invoke();

        // Delete the asset file
        AssetDatabase.DeleteAsset(assetPath);

        // Destroy the SO instance
        DestroyImmediate(scriptableObjectToDelete, true);

    }
}