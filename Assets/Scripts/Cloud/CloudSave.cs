using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models;
using Unity.Services.Core;
using UnityEngine;

public class CloudSave : MonoBehaviour
{
    public Dictionary<string, Book> Books
    {
        get { return _books; }
    }

    private static Dictionary<string, Book> _books;

    private Book _book;


    private async void Awake()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        _books = new Dictionary<string, Book>();
    }

    public void AddBook(Book book)
    {
        _book = book;
        SaveData();
    }


    private async void SaveData()
    {
        var client = CloudSaveService.Instance.Data.Player;

        var keys = await client.ListAllKeysAsync();

        var data = new Dictionary<string, object> {
            { _book.ISBN + "_" + keys.Count, _book }
        };

        await client.SaveAsync(data);
    }


    private async void LoadAllData()
    {
        try
        {
            var results = await CloudSaveService.Instance.Data.Player.LoadAllAsync();

            //Debug.Log($"{results.Count} elements loaded!");

            foreach (var result in results)
            {
                //Debug.Log($"Key: {result.Key}, Value: {result.Value.Value}");

                Book deserializedBook = result.Value.Value as Book;

                _books.Add(result.Key, deserializedBook);

                //Debug.Log("Toplam kitap sayısı: " + _books.Count);
            }
        }
        catch (CloudSaveValidationException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveRateLimitedException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveException e)
        {
            Debug.LogError(e);
        }
    }

    public void UpdateBook(KeyValuePair<string, Book> book)
    {
        LoadAllData();
    }
}
