using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CaloriesBar : MonoBehaviour
{
    [SerializeField] float currentCalorie, maxCalorie;
    [SerializeField] TMP_Text calorieCounterText;

    private Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();

    }

    private void Update()
    {
        currentCalorie = PlayerState.Instance.currentCalorie;
        maxCalorie = PlayerState.Instance.maxCalorie;
        slider.value = currentCalorie / maxCalorie;

        calorieCounterText.text = currentCalorie + "/" + maxCalorie;
    }

}
