using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfController : MonoBehaviour
{
    [SerializeField] private int selfID;
    private ShopShelf shelf;
    private List<Shelf> shelves = new List<Shelf>();
    private bool npcCanInteract = true;

    void Start()
    {
        shelf = ProductFactory.instance.GetShelf(selfID);

        for (int i = 0; i < transform.childCount; i++) //make sure the shelves only shelf child objects
        {
            Transform childShelf = transform.GetChild(i);
            Shelf shelf = childShelf.GetComponent<Shelf>();

            if (shelf != null)
            {
                shelves.Add(shelf);
            }
        }        
    }

    public void OpenShelfControlUI()
    {
        npcCanInteract = false;
        
        for (int i = 0;i < shelves.Count;i++)
        {
            shelves[i].SetNPCCanInteract(npcCanInteract);
        }

        //CALL A FUNCTION HERE OPEN UI SECTION
    }

    public void CloseShelfControlUI() //call this in the close function for the ui bit
    {
        npcCanInteract = true;

        for (int i = 0; i < shelves.Count; i++)
        {
            shelves[i].SetNPCCanInteract(npcCanInteract);
        }
    }

    public void FlatpackShelf()
    {
        if (CheckForItemOnShelf() == false)
        {
            ProductFactory.instance.SpawnBoxOfShelf(shelf.shelfID, gameObject.transform);
            Destroy(gameObject);
        }
        else
        {
            //LOGIC HERE TO DISPLAY AN ERROR MESSAGE OR SOMETHING WHEN THEY AND DESTROY SHELF
            //have some sort of function to call in here that passes data from here to the controller and acts as a constructor 
            //take in this object for easiness on connecting to it
        }
    }

    private bool CheckForItemOnShelf()
    {
        for (int i = 0; i < transform.childCount; i++) //make sure the shelves only shelf child objects
        {
            Transform childShelf = transform.GetChild(i);
            Shelf shelf = childShelf.GetComponent<Shelf>();

            if (shelf != null)
            {
                if (shelf.IsThereItemOnShelf() == true)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
