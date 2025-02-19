using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class UIController : MonoBehaviour
{
    //PURPOSE OF UI CONTROLLER:
    //singlwton that only controls large scale ui events like changing canvas and large scale objects like main menu
    //all small like changing time displays will be done through events attached to the object it changes
    [Header("UI Elements")]
    [SerializeField] private GameObject nileWebsite;
    [SerializeField] private GameObject phoneMenu;

    private List<GameObject> uiElements = new List<GameObject>();

    [Header("Clipboard Stuff")]
    [SerializeField] private GameObject clipBoardMenu;

    [Header("Error message")]
    [SerializeField] private GameObject errorMsgObj;
    private Queue<string> errorMessageQueue = new Queue<string>();
    private bool isDisplaying = false;

    #region Singelton stuff
    public static UIController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    private void Start()
    {
        EventManager.GamePause += ResetCanvas;

        EventManager.OpenTabMenu += OpenPhoneMenu;

        EventManager.CloseTabMenu += ResetCanvas;

        ConfigureList();
        ResetCanvas();
    }

    #region Phone and Website Stuff
    private void ConfigureList() //add new kack to the list when new ones are made
    {
        uiElements.Add(nileWebsite);
        uiElements.Add(phoneMenu);
        uiElements.Add(clipBoardMenu);
    }

    private void ResetCanvas()
    {
        foreach (GameObject element in uiElements) 
        { 
            element.SetActive(false);
        }
    }

    public void OpenNileWebsite()
    {
        ResetCanvas();
        nileWebsite.SetActive(true);
    }

    public void OpenPhoneMenu()
    {
        ResetCanvas();
        phoneMenu.SetActive(true);
    }
    #endregion

    #region Clipboard Stuff
    public void ConfigureShelfControlUI(Item itemOnShelf, GameObject shelf)
    {
        clipBoardMenu.SetActive(true);
        Debug.Log(itemOnShelf.productName + shelf.name);
        clipBoardMenu.GetComponent<ClipBoard_controller>().SetUpClipboardUI(itemOnShelf, shelf);
    }

    public void ConfigureShelfControlUI(GameObject shelf)
    {
        clipBoardMenu.SetActive(true);
        Debug.Log(shelf.name);
        clipBoardMenu.GetComponent<ClipBoard_controller>().SetUpClipboardUI(shelf);
    }

    public bool IsClipboardActive()
    {
        return clipBoardMenu.activeSelf;
    }
    #endregion

    #region User Error Message Stuff
    public void DisplayErrorMessage(string message)
    {
        errorMessageQueue.Enqueue(message);

        if (!isDisplaying)
        {
            StartCoroutine(ProcessQueue());
        }
    }

    IEnumerator ProcessQueue()
    {
        if (errorMessageQueue.Count == 0)
        {
            isDisplaying = false;
            yield break;
        }

        isDisplaying = true;
        errorMsgObj.SetActive(true);
        errorMsgObj.GetComponent<TextMeshProUGUI>().text = errorMessageQueue.Dequeue();

        yield return new WaitForSeconds(2);

        errorMsgObj.SetActive(false);

        yield return new WaitForSeconds(0.2f);

        StartCoroutine(ProcessQueue()); //recursion cause its cool
    }
    #endregion
}
