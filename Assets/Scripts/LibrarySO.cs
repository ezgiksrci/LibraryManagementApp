using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newLibrary", menuName = "Library/Library")]
public class LibrarySO : ScriptableObject
{
    public List<Book> bookSOList;
}
