using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : SingletonMonobehaviour<CraftingSystem>
{
    [SerializeField] GameObject craftingSysytemUI;
    [SerializeField] GameObject toolScreenUI;

    [SerializeField] Button toolsButton;


    [SerializeField] Button craftAxeButton;

    [SerializeField] TMP_Text AxeReq1, AxeReq2;

    public bool isOpen;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        toolsButton.onClick.AddListener(delegate { OpenToolsCategory(); });
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

            if (!isOpen)
            {
                toolScreenUI.SetActive(isOpen);
            }

        }
    }
}
