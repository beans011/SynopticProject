using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfDetection : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    private List<string> tagsHit = new List<string>();
    private List<string> badTags = new List<string>();
    private string goodTag = "shop";

    private void OnTriggerEnter(Collider other)
    {
        if (!tagsHit.Contains(other.tag)) 
        {
            tagsHit.Add(other.tag);
        }
             
        for (int i = 0; i < tagsHit.Count; i++) 
        {
            //Debug.Log(tagsHit[i]);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < tagsHit.Count; i++) 
        { 
            if (other.tag == tagsHit[i]) 
            { 
                tagsHit.RemoveAt(i);
            }
        }

        for (int i = 0; i < tagsHit.Count; i++)
        {
            //Debug.Log(tagsHit[i]);
        }
    }

    public bool CheckTags()
    {
        for (int i = 0; i < tagsHit.Count; i++)
        {
            //Debug.Log(tagsHit[i]);
        }

        if (tagsHit.Contains(goodTag))
        {
            if (tagsHit.Contains("wall") == true || tagsHit.Contains("shelf") == true)
            {
                return false;
            }

            return true;
        }
        
       return false;
    }
}
