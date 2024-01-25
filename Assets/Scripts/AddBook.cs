using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddBook : MonoBehaviour
{
    [SerializeField] TMP_InputField ISBNInput;
    [SerializeField] TMP_InputField titleInput;
    [SerializeField] TMP_InputField authorInput;
    [SerializeField] TMP_InputField pageCountInput;
    [SerializeField] TMP_InputField publisherInput;

    [SerializeField] LibrarySO librarySO;

    public void OnAddBookButtonClicked()
    {
        // Create a new instance of the Book ScriptableObject
        BookSO newBook = ScriptableObject.CreateInstance<BookSO>();
        newBook.ISBN = ISBNInput.text;
        newBook.title = titleInput.text;
        newBook.author = authorInput.text;
        newBook.pageCount = int.Parse(pageCountInput.text);
        newBook.publisher = publisherInput.text;

        // Add the new book to the list
        librarySO.bookSOList.Add(newBook);

        // Save the new Book asset
        string path = "Assets/Resources/Books/" + ISBNInput.text + "_" + librarySO.bookSOList.Count + ".asset";
        UnityEditor.AssetDatabase.CreateAsset(newBook, path);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();

        // Clear input fields
        ISBNInput.text = "";
        titleInput.text = "";
        authorInput.text = "";
        pageCountInput.text = "";
        publisherInput.text = "";
    }
}