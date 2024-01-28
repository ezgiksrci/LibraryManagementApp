using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    public static PageManager Instance { get; private set; }

    public GameObject homePage;
    public GameObject addBookPage;
    public GameObject searchBookPage;
    public GameObject editBookPage;
    public GameObject lendBookPage;
    public GameObject returnBookPage;
    public GameObject viewOverdueBooksPage;

    private void Awake()
    {
        Instance = this;

        addBookPage.SetActive(false);
        searchBookPage.SetActive(false);
        editBookPage.SetActive(false);
        lendBookPage.SetActive(false);
        returnBookPage.SetActive(false);
        viewOverdueBooksPage.SetActive(false);
    }
}
