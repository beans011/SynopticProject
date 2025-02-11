using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugController : MonoBehaviour
{
    [SerializeField] private GUISkin skin;

    private bool showConsole;
    private bool showHelp;
    private bool showStats;
    private bool showFPS = false;

    private float currentFps;
    private float smoothedFps;
    private float smoothingFactor = 0.1f;

    private string input;

    private Vector2 scroll;

    //declare commands here
    public static DebugCommand HELP;
    public static DebugCommand SHOW_STATS;
    public static DebugCommand SPARE_CHANGE;
    public static DebugCommand SHOW_FPS;
    public static DebugCommand<float> ADD_MONEY;
    public static DebugCommand<float> REMOVE_MONEY;
    public static DebugCommand<int> ADD_LEVEL;
    public static DebugCommand<float> SET_BILLS_AMOUNT;
    public static DebugCommand<float> SET_MORALITY_METER;
    public static DebugCommand<int> SET_SHOP_XP;
    public static DebugCommand<int> SPAWN_ITEM;

    public List<object> commandList;

    private void Awake()
    {
        //wtite new commands here
        HELP = new DebugCommand("help", "Shows a list of commands", "help", () =>
        {
            showHelp = !showHelp;

            if (showHelp)
            { 
                showStats = false;
            }
        });

        SHOW_STATS = new DebugCommand("show_stats", "Shows a list of shop stats", "show_stats", () => 
        {
            showStats = !showStats;

            if (showStats)
            {
                showHelp = false;
            }
        });

        SHOW_FPS = new DebugCommand("show_fps", "Shows an fps counter", "show_fps", () => 
        {
            showFPS = !showFPS;
        });

        SPARE_CHANGE = new DebugCommand("spare_change", "Adds 1000 gold", "spare_change", () =>
        {
            ShopStats.AddMoney(1000);
        });

        ADD_MONEY = new DebugCommand<float>("add_money", "Adds set amount of money", "add_money <amount>", (x) =>
        {
            ShopStats.AddMoney(x);
        });

        REMOVE_MONEY = new DebugCommand<float>("remove_money", "Removes set ampunt of money", "remove_money <amount>", (x) =>
        { 
            ShopStats.RemoveMoney(x);
        });

        ADD_LEVEL = new DebugCommand<int>("add_level", "Adds set amount of shop levels", "add_level <amount>", (x) => 
        { 
            ShopStats.SetShopLevel(x);
        });

        SET_BILLS_AMOUNT = new DebugCommand<float>("set_bills_amount", "Set how much the bills cost", "set_bills_amount <amount>", (x) => 
        { 
            ShopStats.SetBillsAmount(x);
        });

        SET_MORALITY_METER = new DebugCommand<float>("set_morality_amount", "Changes the morality meter by set amount", "set_morality_meter <amount>", (x) =>
        { 
            ShopStats.SetMoralityMeter(x);
        });

        SET_SHOP_XP = new DebugCommand<int>("set_shop_xp", "Changes shop xp by set amount", "set_shop_xp <amount>", (x) => 
        { 
            ShopStats.SetShopXP(x); 
        });

        SPAWN_ITEM = new DebugCommand<int>("spawn_item", "Spawns box of items with given ID", "spawn_item <item _id>", (x) => 
        { 
            ProductFactory.instance.SpawnBoxOfItems(x);
        });

        commandList = new List<object>
        {
            //commands go in here
            HELP,
            SHOW_STATS,
            SHOW_FPS,
            SPARE_CHANGE,
            ADD_MONEY,
            REMOVE_MONEY,
            ADD_LEVEL,
            SET_BILLS_AMOUNT,
            SET_MORALITY_METER,
            SET_SHOP_XP,
            SPAWN_ITEM
        };
    }

    private void Start()
    {
        EventManager.CloseCheatConsole += SetShowHelpFalse;
        EventManager.CloseCheatConsole += SetShowShopStatsFalse;

        StartCoroutine(StartCount());
    }

    private IEnumerator StartCount()
    {
    //https://gist.github.com/mstevenson/5103365 - fps counter but changed a little bit for looks

        GUI.depth = 2;
        while (true)
        {
            currentFps = 1f / Time.unscaledDeltaTime;

            // Apply exponential smoothing to calculate the weighted average
            smoothedFps = (smoothingFactor * currentFps) + (1f - smoothingFactor) * smoothedFps;

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void SetShowHelpFalse()
    {
        showHelp = false;
    }

    private void SetShowShopStatsFalse()
    {
        showStats = false;
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

        if (showFPS == true)
        {
            ShowFPSCounter();
        }

        if (!showConsole) { return; }

        UserInputs();

        //box gui stuff
        float y = 0f;

        if (showHelp == true) 
        {
            ShowHelp(y);
            y += 100;
        }

        if (showStats == true) 
        {
            ShowShopStats(y);
            y += 100;
        }

        ShowConsoleTextBox(y);
    }

    private void ShowHelp(float y)
    {
        SetShowShopStatsFalse();

        GUI.Box(new Rect(0, y, Screen.width, 100), "");

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
    }

    private void ShowShopStats(float y)
    {
        SetShowHelpFalse();

        GUI.Box(new Rect(0, y, Screen.width, 100), "");

        Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * commandList.Count);

        scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), scroll, viewport);

        //add in shopstats stats cause cool to see
        var shopStatsList = new List<string>{"Shop Stats: ", 
            "Shop Name: " + ShopStats.GetShopName(), 
            "Money: " + ShopStats.GetMoney().ToString(),
            "Bills: " + ShopStats.GetBillsAmount().ToString(),
            "Morality Meter: " + ShopStats.GetMoralityMeter().ToString(),
            "Shop XP: " + ShopStats.GetShopXP().ToString(),
            "Shop Level: " + ShopStats.GetShopLevel().ToString()
        };

        for (int i = 0; i < shopStatsList.Count; i++)
        {
            string label = shopStatsList[i];

            Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);

            GUI.Label(labelRect, label, skin.GetStyle("label"));
        }

        GUI.EndScrollView();
    }

    private void ShowConsoleTextBox(float y)
    {
        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);

        GUI.SetNextControlName("console");
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input, skin.GetStyle("label"));
        GUI.FocusControl("console");
    }

    private void ShowFPSCounter()
    {
        Rect location = new Rect(1820, 5, 95, 35);
        string text = "FPS: " + Mathf.Round(smoothedFps);
        GUI.Label(location, text);
    }

    private void UserInputs()
    {
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
    }

    private void HandleInput()
    {
        string[] properties = input.Split(" ");

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
                    (commandList[i] as DebugCommand<int>).Invoke(int.Parse(properties[1]));
                }

                else if (commandList[i] as DebugCommand<float> != null)
                {
                    (commandList[i] as DebugCommand<float>).Invoke(float.Parse(properties[1]));
                }
            }
        }
    }
}
