using UnityEngine;

public class ChoppablrTree : MonoBehaviour
{
    private bool isPlayerNear;

    public float treeMaxHealth;
    public float treeHealth;
    public GameObject choppedTree;
    private void Start()
    {
        treeHealth = treeMaxHealth;
    }

    void Update()
    {
        isPlayerNear = SelectionManager.Instance.isPlayerNear;


    }
    public void GetHit(float damage)
    
    {
        treeHealth -= damage;
        if (treeHealth <= 0) 
        {
          this.gameObject.SetActive(false);
            Instantiate(choppedTree,transform.position,Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
