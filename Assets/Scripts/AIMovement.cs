using UnityEngine;
using Unity.Mathematics;


using Random = UnityEngine.Random;

public class AIMovement : MonoBehaviour
{
    Animator animator;

    [Header("Settings")]
    [SerializeField] float movespeed = 0.2f;
    [SerializeField] float walkCounter;
    [SerializeField] float waitCounter;
    [SerializeField] bool isWalking;

    Vector3 stopPosition;
    float walkTime;
    float waitTime;
    float walkDirection;


    private void Start()
    {
        animator = GetComponent<Animator>();
        walkTime = Random.Range(3, 6);
        waitTime = Random.Range(5, 7);

        waitCounter = walkTime;
        waitCounter = waitTime;

        ChooseDirection();
    }

    private void Update()
    {
        if (isWalking)
        {
            animator.SetBool("isRunning", true);

            walkCounter -= Time.deltaTime;

            transform.localRotation = quaternion.Euler(0, walkDirection, 0);
            transform.position += transform.forward * movespeed * Time.deltaTime;

            /*
            switch(walkDirection)
            {
                case 0:
                    transform.localRotation = quaternion.Euler(0, 0, 0);
                    transform.position += transform.forward * movespeed * Time.deltaTime;
                    break;
                case 1:
                    transform.localRotation = quaternion.Euler(0, 90, 0);
                    transform.position += transform.forward * movespeed * Time.deltaTime;
                    break;
                case 2:
                    transform.localRotation = quaternion.Euler(0, -90, 0);
                    transform.position += transform.forward * movespeed * Time.deltaTime;
                    break;
                case 3:
                    transform.localRotation = quaternion.Euler(0, 180, 0);
                    transform.position += transform.forward * movespeed * Time.deltaTime;
                    break;
            }
            */

            if (walkCounter <= 0)
            {
                stopPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                isWalking = false;
                transform.position = stopPosition;
                animator.SetBool("isRunning", false);
                waitCounter = waitTime;
            }
        }
        else
        {
            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0)
            {
                ChooseDirection();
            }
        }
    }

    public void ChooseDirection()
    {
        walkTime = Random.Range(2, 6);
        waitTime = Random.Range(3, 7);

        waitCounter = walkTime;
        waitCounter = waitTime;

        walkDirection = Random.Range(-180f, 180f);

        isWalking = true;
        walkCounter = walkTime;
    }
}
