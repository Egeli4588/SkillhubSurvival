using TMPro;
using UnityEngine;

public class SelectionManager : SingletonMonobehaviour<SelectionManager>
{
    [SerializeField] GameObject interactableText;
    TMP_Text interactionText;

    [SerializeField] GameObject handImage;
    [SerializeField] GameObject axeImage;
    [SerializeField] GameObject whiteCirticle;

    public bool isPlayerNear;

    public ChoppablrTree ChoppablrTree;
    protected override void Awake()
    {
        base.Awake();

        if (interactionText != null) {
        
            interactableText.SetActive(false);  
        }
    }
    private void Start()
    {
        interactionText = interactableText.GetComponent<TMP_Text>();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10))
        {
            InteractableObject selectedInteractable;
            var selectionTransform = hit.transform;

            selectionTransform.TryGetComponent<InteractableObject>(out selectedInteractable);

            if (selectedInteractable != null)
            {
                interactionText.text = selectedInteractable.GetItemName();
                interactableText.SetActive(true);

                if (selectionTransform.CompareTag("Collectable"))
                {
                    handImage.SetActive(true);
                    whiteCirticle.SetActive(false);

                }
                else if (selectionTransform.CompareTag("Choppable"))
                {
                    axeImage.SetActive(true);
                    whiteCirticle.SetActive(false);
                    isPlayerNear = true;
                    ChoppablrTree =hit.transform.GetComponent<ChoppablrTree>(); 
              
                }
                else
                {
                    ChoppablrTree = null;
                }
            }
            else
            {
                interactableText.SetActive(false);
                handImage.SetActive(false);
                axeImage.SetActive(false);
                whiteCirticle.SetActive(true);
                ChoppablrTree = null;
            }

            //iventorye ýtem ekle
            if (Input.GetMouseButtonDown(0) && selectionTransform.CompareTag("Collectable") && !InventorySystem.Instance.CheckIfFull())
            {

                AddItemToInventory(selectedInteractable);
            }





        }
        else
        {
            interactableText.SetActive(false);
            //hiç bir þeye deðmiyorsa
            handImage.SetActive(false);
            whiteCirticle.SetActive(true);
            axeImage.SetActive(false);
        }



    }

    void AddItemToInventory(InteractableObject selectedInteractable)
    {
        string pickedItemName = selectedInteractable.GetItemName();
        pickedItemName += "_Inv";
        InventorySystem.Instance.AddToInventory(pickedItemName);
        Destroy(selectedInteractable.gameObject);

        Debug.Log(pickedItemName + " Picked up");
    }
}
