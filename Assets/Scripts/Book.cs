using System;
using UnityEngine;

public class Book
{
    public string ISBN;
    public string title;
    public string author;
    public string publisher;
    public int pageCount;
    public bool isAvailable = true;
    public string borrowerName;
    public DateTime dueDate;
    public string dueDateString;
    public bool isOverdued = false;
}
