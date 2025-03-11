using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShelfController : MonoBehaviour
{
    [SerializeField] private int selfID;
    [SerializeField] private GameObject shelfCollider;
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

    public void OpenShelfControlUI() //call function when ui bit opens
    {
        npcCanInteract = false;
        
        for (int i = 0;i < shelves.Count;i++)
        {
            shelves[i].SetNPCCanInteract(npcCanInteract);
        }
    }

    public void CloseShelfControlUI() //call this in the close function for the ui bit
    {
        npcCanInteract = true;

        for (int i = 0; i < shelves.Count; i++)
        {
            shelves[i].SetNPCCanInteract(npcCanInteract);
            shelves[i].UpdateInfo();
        }
    }

    public void FlatpackShelf()
    {
        if (CheckForItemOnShelf() == false)
        {
            ProductFactory.instance.SpawnBoxOfShelf(shelf.shelfID);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("stupid there is an item on the shelf");
            UIController.instance.DisplayErrorMessage("Cant remove shelf because there are items on shelf");
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

    public void SetColliderActive()
    {
        shelfCollider.SetActive(true);
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        Destroy(rb);          
    }

    public bool GetCanNPCInteract()
    {
        return npcCanInteract;
    }

    public List<Shelf> GetShelves()
    {
        return shelves;
    }
}
