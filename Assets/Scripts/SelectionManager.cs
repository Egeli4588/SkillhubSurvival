using TMPro;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] GameObject interactableText;
    TMP_Text interactionText;

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
            }
            else
            {
                interactableText.SetActive(false);
            }
        }
        else
        {
            interactableText.SetActive(false);
        }
    }
}
