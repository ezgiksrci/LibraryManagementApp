using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddBook : MonoBehaviour
{
    [SerializeField] LibrarySO librarySO;

    [SerializeField] TMP_InputField ISBNInput;
    [SerializeField] TMP_InputField titleInput;
    [SerializeField] TMP_InputField authorInput;
    [SerializeField] TMP_InputField pageCountInput;
    [SerializeField] TMP_InputField publisherInput;


    public void OnAddBookButtonClicked()
    {
        try
        {
            // create a new instance of the Book ScriptableObject
            BookSO newBook = ScriptableObject.CreateInstance<BookSO>();
            newBook.ISBN = ISBNInput.text;
            newBook.title = titleInput.text;
            newBook.author = authorInput.text;
            newBook.pageCount = int.Parse(pageCountInput.text);
            newBook.publisher = publisherInput.text;
            newBook.librarySO = librarySO;

            // add the new book to the list
            librarySO.bookSOList.Add(newBook);

            // save the new bookSO asset
            string path = "Assets/Resources/Books/" + ISBNInput.text + "_" + librarySO.bookSOList.Count + ".asset";
            UnityEditor.AssetDatabase.CreateAsset(newBook, path);
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();

            // clear input fields
            ISBNInput.text = "";
            titleInput.text = "";
            authorInput.text = "";
            pageCountInput.text = "";
            publisherInput.text = "";
        }

        catch (System.Exception)
        {
            MessageBox.Instance.ShowWarningPanel("Please make sure you enter the information correctly.");
        }
    }
}