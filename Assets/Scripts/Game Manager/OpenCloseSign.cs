using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class OpenCloseSign : MonoBehaviour
{
    [SerializeField] private GameObject textObj;
    private TextMeshPro text;
    private bool setState = false;
    private bool onCooldown = false;
    private float cooldownTime = 0.1f;

    private void Start()
    {       
        text = textObj.GetComponent<TextMeshPro>();
        SetSignState(setState);
    }

    public void SignState()
    {
        if (onCooldown) return;

        setState = !setState;
        SetSignState(setState);

        StartCoroutine(CooldownRoutine());
    }

    private void SetSignState(bool ans)
    {
        if (ans == true) 
        {
            text.color = Color.green;
            EventManager.OnSetShopOpen();
        }

        else if (ans == false)
        {
            text.color = Color.grey;
            EventManager.OnSetShopOpen();
        }
    }

    private IEnumerator CooldownRoutine()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        onCooldown = false;
    }
}
