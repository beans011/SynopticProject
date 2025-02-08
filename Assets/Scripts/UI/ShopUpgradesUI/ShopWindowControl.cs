using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWindowControl : MonoBehaviour
{
    [SerializeField] private GameObject foodSection;
    [SerializeField] private GameObject drinkSection;
    [SerializeField] private GameObject tobaccoSection;
    [SerializeField] private GameObject shelvesSection;
    [SerializeField] private GameObject upgradesSection;

    private void ResetSection()
    {
        foodSection.SetActive(false);
        drinkSection.SetActive(false);
        tobaccoSection.SetActive(false);
        shelvesSection.SetActive(false);
        upgradesSection.SetActive(false);
    }

    public void OpenSection(GameObject name)
    {
        ResetSection();
        name.SetActive(true);
    }

}
