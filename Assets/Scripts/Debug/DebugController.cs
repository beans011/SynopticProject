using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugController : MonoBehaviour
{
    [SerializeField] private GUISkin skin;

    bool showConsole;
    bool showHelp;

    string input;

    Vector2 scroll;

    //declare commands here
    public static DebugCommand HELP;
    public static DebugCommand SPARE_CHANGE;
    public static DebugCommand<float> ADD_MONEY;
    public static DebugCommand<float> REMOVE_MONEY;

    public List<object> commandList;

    private void Awake()
    {
        //wtite new commands here
        HELP = new DebugCommand("help", "Shows a list of commands", "help", () =>
        {
            showHelp = true;
        });

        SPARE_CHANGE = new DebugCommand("spare_change", "Adds 1000 gold", "spare_change", () =>
        {
            ShopStats.AddMoney(1000);
        });

        ADD_MONEY = new DebugCommand<float>("add_money", "Adds set amount of money", "add_money <amount>", (x) =>
        {
            ShopStats.AddMoney(x);
        });

        commandList = new List<object>
        {
            //commands go in here
            HELP,
            SPARE_CHANGE,
            ADD_MONEY
        };
    }

    private void Start()
    {
        EventManager.CloseCheatConsole += SetShowHelpFalse;
    }

    private void SetShowHelpFalse()
    {
        showHelp = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            showConsole = !showConsole;

            if (showConsole == true)
            {
                EventManager.OnOpenCheatConsole();
            }

            else
            {
                EventManager.OnCloseCheatConsole();
            }
        }     
    }

    private void OnGUI()
    {
        GUI.skin = skin;

        if (!showConsole) { return; }

        //basic show and close of ultimate hacker tool
        if (Event.current.type == EventType.KeyDown && (Event.current.keyCode == KeyCode.BackQuote))
        {
            showConsole = !showConsole;

            if (showConsole == true)
            {
                EventManager.OnOpenCheatConsole();
            }

            else
            {
                EventManager.OnCloseCheatConsole();
            }
        }

        //stupid system that allows me to press enter with out having to install input manager just for this too work
        if (Event.current.type == EventType.KeyDown && (Event.current.keyCode == KeyCode.Return))
        {
            HandleInput();
            input = "";
        }

        //box gui stuff
        float y = 0f;

        if (showHelp == true) 
        {
            GUI.Box(new Rect(0, y, Screen.width,100), "");

            Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * commandList.Count);

            scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), scroll, viewport);

            for (int i = 0; i < commandList.Count; i++) 
            { 
                DebugCommandBase command = commandList[i] as DebugCommandBase;

                string label = $"{command.commandFormat} - {command.commandDescription}";

                Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);

                GUI.Label(labelRect, label, skin.GetStyle("label"));
            }

            GUI.EndScrollView();

            y += 100;
        }

        GUI.Box(new Rect(0, y, Screen.width, 30), "");        
        GUI.backgroundColor = new Color(0, 0, 0, 0);

        GUI.SetNextControlName("console");
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input, skin.GetStyle("label"));
        GUI.FocusControl("console");
    }

    private void HandleInput()
    {
        string[] properties = input.Split(' ');

        for (int i = 0; i < commandList.Count; i++) 
        { 
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if (input.Contains(commandBase.commandID)) 
            {   //add in other data types when needed
                if (commandList[i] as DebugCommand != null)
                {
                    (commandList[i] as DebugCommand).Invoke();
                }

                else if (commandList[i] as DebugCommand<int> != null)
                {
                    (commandList[i] as DebugCommand<int>).Invoke(int.Parse(properties[i]));
                }

                else if (commandList[i] as DebugCommand<float> != null)
                {
                    (commandList[i] as DebugCommand<float>).Invoke(int.Parse(properties[i]));
                }
            }
        }
    }
}
