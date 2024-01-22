using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddBook : MonoBehaviour
{
    [SerializeField] TMP_InputField bookISBN;
    [SerializeField] TMP_InputField bookTitle;
    [SerializeField] TMP_InputField bookAuthor;
    [SerializeField] TMP_InputField numberofCopies;
    [SerializeField] TMP_InputField publisher;


    public void AddNewBook()
    {
        // Create a new Book object with the provided details
        Book book = new Book
        {
            bookISBN = bookISBN.text,
            bookTitle = bookTitle.text,
            bookAuthor = bookAuthor.text,
            publisher = publisher.text,
        };

        // Determine the index based on the existing count of books with the same ISBN
        int index = PlayerPrefs.GetInt(bookISBN.text + "_Count", 0);


        int numofCopies = int.Parse(numberofCopies.text);
        for (int i = 0; i < numofCopies; i++)
        {
            // Increment the count for this ISBN
            index++;
            PlayerPrefs.SetInt(bookISBN.text + "_Count", index);

            // Save the new book with the ISBN and index as the key
            string key = bookISBN.text + "_" + index;
            string json = JsonUtility.ToJson(book);
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
        }
    }
}
