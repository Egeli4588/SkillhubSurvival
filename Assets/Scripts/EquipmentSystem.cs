using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSystem : SingletonMonobehaviour<EquipmentSystem>
{
    public GameObject quickSlotsPanel;

    public List<GameObject> quickSlotslist = new List<GameObject>();
    public List<string> itemList = new List<string>();

    public Transform numbersHolder;


    public int selectedNumber = -1;
    public GameObject selectedItem;

    public Transform toolHolder;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectQuickSlot(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectQuickSlot(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectQuickSlot(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectQuickSlot(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectQuickSlot(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectQuickSlot(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SelectQuickSlot(7);
        }

    }

    private void SelectQuickSlot(int number)
    {

        if (ChechkIfSlotIsFull(number))
        {
            if (selectedNumber != number)
            {
                selectedNumber = number;

                if (selectedItem != null)
                {
                    selectedItem.GetComponent<InventoryItem>().isSelected = false;
                }

                selectedItem = quickSlotslist[number - 1].transform.GetChild(0).gameObject;

                selectedItem.GetComponent<InventoryItem>().isSelected = true;


                selectedItemModel(selectedItem);

                foreach (Transform child in numbersHolder.transform)

                {
                    Image img = child.GetComponent<Image>();
                    if (img != null)
                    {
                        child.GetComponent<Image>().color = Color.white;
                    }


                }

                Image toBeChanged = numbersHolder.transform.Find("Number" + number).GetComponent<Image>();
                toBeChanged.color = Color.green;

            }

            else
            {
                selectedNumber = -1;

                if (selectedItem != null)
                {
                    selectedItem.GetComponent<InventoryItem>().isSelected = false;
                    selectedItem = null;
                }



                foreach (Transform child in numbersHolder.transform)

                {
                    Image img = child.GetComponent<Image>();
                    if (img != null)
                    {
                        child.GetComponent<Image>().color = Color.white;
                    }


                }


            }



        }


    }

    private void selectedItemModel(GameObject item)
    {
        string selectedItemName = selectedItem.name.Replace("_Inv(Clone)","");
        GameObject itemModel = Instantiate(Resources.Load<GameObject>(selectedItemName + "_Model")
            , new Vector3(0.4f, 0.2f,- 1.2f),Quaternion.Euler(0,-2,0));
        itemModel.transform.SetParent(toolHolder,false);

    }

    private bool ChechkIfSlotIsFull(int number)
    {
        if (quickSlotslist[number - 1].transform.childCount > 0)
        {
            return true;

        }
        else
        {

            return false;
        }
    }

    public void AddToQuickSlot(GameObject itemToEquip)

    {
        GameObject avaliableSlot = FindNextEmptySlot();
        itemToEquip.transform.SetParent(avaliableSlot.transform, false);

        string cleanName = itemToEquip.name.Replace("_Inv(Clone)", "");
        itemList.Add(cleanName);
        InventorySystem.Instance.ReCalculatelist();
    }

    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in quickSlotslist)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }


    public bool ChechkIfFull()
    {
        int counter = 0;
        foreach (GameObject slot in quickSlotslist)
        {
            if (slot.transform.childCount > 0)
            {
                counter++;
            }
        }
        if (counter == 7)
        {
            return true;

        }
        else
        {
            return false;
        }
    }
}
