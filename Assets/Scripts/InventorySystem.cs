using System;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : SingletonMonobehaviour<InventorySystem>
{
    protected override void Awake()
    {
        base.Awake();
    }

    [SerializeField] GameObject InventoryScreenUI;
    public bool isOpen;
    [SerializeField] bool isFull;

    [SerializeField] List<GameObject> slotList = new List<GameObject>();
    public List<string> itemList = new List<string>();
    private GameObject itemToAdd;
    private GameObject whatSlotToEquip;




    private void Start()
    {
        InventoryScreenUI.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isOpen = !isOpen;
            InventoryScreenUI.SetActive(isOpen);



        }
    }

    public void AddToInventory(string itemName)
    {

        whatSlotToEquip = FindNextEmptySlot();
        itemToAdd = (GameObject)Instantiate(Resources.Load<GameObject>(itemName), whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotToEquip.transform);

        string result = itemName.Replace("_Inv","");
        itemList.Add(result);
    }

    public bool CheckIfFull()
    {
        int counter = 0;
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                counter++;
            }
        }

        return counter == slotList.Count;
    }

    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in slotList)
        {
            //Eðer içinde item olmayan bir slot bulursan gönder yoksa boþ bir obje gönder
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }

    public void RemoveItem(string nameToRemove, int amaountToRemove)
    {
        if (nameToRemove == null) { return; }
        int counter = amaountToRemove;
        for (int i = slotList.Count - 1; i >= 0; i--)
        {
            if (counter == 0) break;


            if (slotList[i].transform.childCount > 0)
            {

                if (slotList[i].transform.GetChild(0).name == nameToRemove + "_Inv(Clone)" && counter != 0)
                {

                    Destroy(slotList[i].transform.GetChild(0).gameObject);
                    counter--;
                }

            }
        }
    }

    public void ReCalculatelist()
    {
        itemList.Clear();
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                string name = slot.transform.GetChild(0).name;
                string stringRemove = "_Inv(Clone)";
                string result = name.Replace(stringRemove, "");
                itemList.Add(result);
            }

        }
    }
}
