using UnityEngine;

public class EquipabbleItem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Animator animator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            ChoppablrTree selectedTree = SelectionManager.Instance.ChoppablrTree;
            if (selectedTree != null) 
            {
                selectedTree.GetHit(1);
                animator.SetTrigger("hit");
            }
            
           
        }
       
        
    }
}
