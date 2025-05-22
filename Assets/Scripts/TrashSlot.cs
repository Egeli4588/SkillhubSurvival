using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrashSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject trashAlertUI;
    private TMP_Text textToModify;

    public Sprite trashClosed;
    public Sprite trashOpen;


    private Image trashImageComponent;

    private Button yesBtn, noBtn;

    GameObject draggedItem
    {
        get
        {

            return DragDrop.itemBeingDragged;
        }

    }

    private GameObject itemTobeDeleted;

    public string itemName
    {
        get
        {


            return draggedItem.GetComponent<InventoryItem>().itemName;
        }



    }



    private void Start()
    {
        trashImageComponent = transform.Find(("TrashImage")).GetComponent<Image>();
        textToModify = trashAlertUI.transform.Find("TrashAlertText").GetComponent<TMP_Text>();

        yesBtn = trashAlertUI.transform.Find("yes").GetComponent<Button>();
        yesBtn.onClick.AddListener(delegate { DeleteItem(); });


        noBtn = trashAlertUI.transform.Find("no").GetComponent<Button>();
        noBtn.onClick.AddListener(delegate { CancelDeletion(); });
    }

    private void DeleteItem()
    {
        trashImageComponent.sprite = trashClosed;
        DestroyImmediate(itemTobeDeleted.gameObject);
        InventorySystem.Instance.ReCalculatelist();
        CraftingSystem.Instance.RefreshNeededItems();
        trashAlertUI.SetActive(false);
    }

    private void CancelDeletion()
    {
        trashImageComponent.sprite = trashClosed;
        trashAlertUI.SetActive(false);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (draggedItem.GetComponent<InventoryItem>().isTrashable == true)
        {
            itemTobeDeleted = draggedItem.gameObject;
            StartCoroutine(NotifyBeforeDeletion());

        }
    }

    IEnumerator NotifyBeforeDeletion()
    {
        trashAlertUI.SetActive(true);
        textToModify.text = "Are you sure to delete " + itemName + "?";
        yield return new WaitForSeconds(1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (draggedItem != null && draggedItem.GetComponent<InventoryItem>().isTrashable == true)
        {
            trashImageComponent.sprite = trashOpen;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (draggedItem != null && draggedItem.GetComponent<InventoryItem>().isTrashable == true)
        {
            trashImageComponent.sprite = trashClosed;
        }
    }
}
