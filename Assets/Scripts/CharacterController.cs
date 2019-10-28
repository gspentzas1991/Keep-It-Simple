using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
public class CharacterController : MonoBehaviour {
    
    [SerializeField] private Animator animator;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform selectedTable;
    private NavMeshAgent navMeshAgent;
    private bool isOnTable = false;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update () {
        DetectTableSelection();
        //If a table is selected the waiter starts moving towards it
        if (selectedTable)
        {
            MoveTowardsTarget(selectedTable);
        }
        animator.SetFloat("MoveSpeed", Mathf.Abs(navMeshAgent.velocity.z) + Mathf.Abs(navMeshAgent.velocity.x));
    }
    
    /// <summary>
    /// Makes the waiter move towards the selected target, if he is not already there
    /// </summary>
    /// <param name="target"></param>
    private void MoveTowardsTarget(Transform target)
    {
        if(!isOnTable)
        {
            navMeshAgent.SetDestination(target.position);
        }
    }


    /// <summary>
    /// Checks if the user has clicked on a table this frame
    /// </summary>
    private void DetectTableSelection()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                if (objectHit.gameObject.tag == "Table")
                {
                    selectedTable = objectHit;
                    isOnTable = false;
                    navMeshAgent.isStopped = false;
                }
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        //if the waiter has reached the selected table's trigger, he has reached the table
        if (other.gameObject == selectedTable.gameObject)
        {
            isOnTable = true;
            navMeshAgent.isStopped = true;
        }
    }

}
