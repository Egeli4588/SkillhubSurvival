using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{

    public bool isTrashable;

    private GameObject itemInfoUI;

    private TMP_Text itemNameText;
    private TMP_Text itemDescriptionText;
    private TMP_Text itemFunctionalityText;

    public string itemName, itemDescription, itemFunctionality;

    //Consumption
    private GameObject itemPendingConsumption;
    public bool isConsumable;

    public float healthEffect;
    public float calorieEffect;
    public float hydrationEffect;


    private void Start()
    {
        itemInfoUI = InventorySystem.Instance.itemInfoUI;
        itemNameText = itemInfoUI.transform.Find("ItemName").GetComponent<TMP_Text>();
        itemDescriptionText = itemInfoUI.transform.Find("ItemDescription").GetComponent<TMP_Text>();
        itemFunctionalityText = itemInfoUI.transform.Find("ItemFunctionality").GetComponent<TMP_Text>();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        itemInfoUI.SetActive(true);
        itemNameText.text = itemName;
        itemDescriptionText.text = itemDescription;
        itemFunctionalityText.text = itemFunctionality;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemInfoUI.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (isConsumable)
            {
                itemPendingConsumption = gameObject;
                ConsumeTheItem(healthEffect, calorieEffect, hydrationEffect);
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (isConsumable && itemPendingConsumption == gameObject)
            {
                DestroyImmediate(gameObject);
                InventorySystem.Instance.ReCalculatelist();
                CraftingSystem.Instance.RefreshNeededItems();
            }
        }
    }

    private void ConsumeTheItem(float health, float calorie, float hydration)
    {
        itemInfoUI.SetActive(false);

        HealthEffectCalculation(health);
        CaloriesEffectCalculation(calorie);
        HydrationEffectCalculation(hydration);
    }

    private void HydrationEffectCalculation(float hydration)
    {
        float hydrationBeforeConsumption = PlayerState.Instance.currentWater;
        float maxHydration = PlayerState.Instance.maxWater;

        if (hydration != 0)
        {
            if ((hydrationBeforeConsumption + hydration) > maxHydration)
            {
                PlayerState.Instance.currentWater = maxHydration;
            }
            else
            {
                hydrationBeforeConsumption += hydration;
                PlayerState.Instance.currentWater = hydrationBeforeConsumption;
            }
        }
    }

    private void CaloriesEffectCalculation(float calorie)
    {
        float calorieBeforeConsumption = PlayerState.Instance.currentCalorie;
        float maxCalorie = PlayerState.Instance.maxCalorie;

        if (calorie != 0)
        {
            if ((calorieBeforeConsumption + calorie) > maxCalorie)
            {
                PlayerState.Instance.currentCalorie = maxCalorie;
            }
            else
            {
                calorieBeforeConsumption += calorie;
                PlayerState.Instance.currentCalorie = calorieBeforeConsumption;
            }
        }
    }

    private void HealthEffectCalculation(float health)
    {
        float healthBeforeConsumption = PlayerState.Instance.currentHealth;
        float maxHealth = PlayerState.Instance.maxHealth;

        if (health != 0)
        {
            if ((healthBeforeConsumption + health) > maxHealth)
            {
                PlayerState.Instance.currentHealth = maxHealth;
            }
            else
            {
                healthBeforeConsumption += health;
                PlayerState.Instance.currentHealth = healthBeforeConsumption;
            }
        }
    }
}
