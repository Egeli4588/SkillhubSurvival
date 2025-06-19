using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : SingletonMonobehaviour<CraftingSystem>
{
    [SerializeField] GameObject craftingSysytemUI;
    [SerializeField] GameObject toolScreenUI;

    public List<string> InventoryItemlist = new List<string>();

    [SerializeField] Button toolsButton;


    [SerializeField] Button craftAxeButton;

    [SerializeField] TMP_Text AxeReq1, AxeReq2;

    // bluePrint
    Blueprint AxeBlp = new Blueprint("Axe_Inv", "Stone", "Stick", 3, 3, 2);


    public bool isOpen;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        toolsButton.onClick.AddListener(delegate { OpenToolsCategory(); });
        craftAxeButton.onClick.AddListener(delegate { CraftAnyItem(AxeBlp); });
    }
    void OpenToolsCategory()
    {
        craftingSysytemUI.SetActive(false);
        toolScreenUI.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isOpen = !isOpen;
            craftingSysytemUI.SetActive(isOpen);
            RefreshNeededItems();
            if (!isOpen)
            {
                toolScreenUI.SetActive(isOpen);
            }

        }
    }

    void CraftAnyItem(Blueprint itemToCraft)
    {
        //add ýtem to inventory 
        InventorySystem.Instance.AddToInventory(itemToCraft.itemName);


        //remove all equired items that used
        InventorySystem.Instance.RemoveItem(itemToCraft.Req1,itemToCraft.Req1Amount);
        InventorySystem.Instance.RemoveItem(itemToCraft.Req2, itemToCraft.Req2Amount);

        //refresh the inventory
        InventorySystem.Instance.ReCalculatelist();

        StartCoroutine(calculate());
    
    }


    public IEnumerator calculate() 
    {
       yield return new WaitForSeconds(0.1f);
        InventorySystem.Instance.ReCalculatelist();
        RefreshNeededItems();
    }

    public void RefreshNeededItems()
    {
        int stone_count = 0;
        int stick_count = 0;

        InventoryItemlist = InventorySystem.Instance.itemList;

        foreach (string itemName in InventoryItemlist)
        {
            switch (itemName)
            {
                case "Stone":
                    stone_count++;
                    break;
                case "Stick":
                    stick_count++;
                    break;
                default:
                    break;
            }
        }


        //-----Axe------------

        AxeReq1.text = "3 Stone [" + stone_count + "]";
        AxeReq2.text = "3 Stick [" + stick_count + "]";

        if (stick_count >= 3 && stone_count >= 3)
        {
            craftAxeButton.gameObject.SetActive(true);
        }
        else
        {
            craftAxeButton.gameObject.SetActive(false);
        }
    }
}
