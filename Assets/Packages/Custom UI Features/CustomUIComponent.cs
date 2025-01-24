using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomUIComponent : MonoBehaviour
{

    //based on this yt video https://www.youtube.com/watch?v=oOQvhIg0ntg&list=WL&index=4
    private void Awake()
    {
        Init();
    }

    public abstract void Setup();
    public abstract void Configure();

    private void Init() 
    { 
        Setup();
        Configure();
    }

    private void OnValidate()
    {
        Init();
    }
}
