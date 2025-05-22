using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaterBar : MonoBehaviour
{
    [SerializeField] float currentWater, maxWater;
    [SerializeField] TMP_Text waterCounterText;

    private Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();

    }

    private void Update()
    {
        currentWater = PlayerState.Instance.currentWater;
        maxWater = PlayerState.Instance.maxWater;
        slider.value = currentWater / maxWater;

        waterCounterText.text = currentWater + "/" + maxWater;
    }
}
