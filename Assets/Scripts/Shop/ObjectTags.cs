using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTags : MonoBehaviour
{
   [SerializeField] private List<string> tags = new List<string>();

    public string CheckTags(string x)
    {
        for (int i = 0; i < tags.Count; i++) 
        { 
            if (tags[i] == x)
            {
                return tags[i];
            }
            
        }

        return null;
    }
}
