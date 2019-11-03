using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using Assets.Scripts.Classes;
public class CharacterController : MonoBehaviour {
    
    [SerializeField] private Animator Animator;
    [SerializeField] private Camera Camera;
    private Transform SelectedTable;
    private NavMeshAgent NavMeshAgent;
    private bool IsOnTable = false;

    public GuiPhoneButtonScript GuiPhone;

    private void Start()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update () {
        DetectTableSelection();
        //If a table is selected the waiter starts moving towards it
        if (SelectedTable)
        {
            MoveTowardsTarget(SelectedTable);
        }
        Animator.SetFloat("MoveSpeed", Mathf.Abs(NavMeshAgent.velocity.z) + Mathf.Abs(NavMeshAgent.velocity.x));
    }
    
    /// <summary>
    /// Makes the waiter move towards the selected target, if he is not already there
    /// </summary>
    /// <param name="target"></param>
    private void MoveTowardsTarget(Transform target)
    {
        if(!IsOnTable)
        {
            NavMeshAgent.SetDestination(target.position);
        }
    }


    /// <summary>
    /// Checks if the user has clicked on a table this frame
    /// </summary>
    private void DetectTableSelection()
    {

        //We ignore clicks if the phone is on fullscreen mode
        if (Input.GetKeyDown(KeyCode.Mouse0) && GuiPhone.PhonePosition != PhonePosition.Fullscreen)
        {
            RaycastHit hit;
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                if (objectHit.gameObject.tag == "Table")
                {
                    SelectedTable = objectHit;
                    IsOnTable = false;
                    NavMeshAgent.isStopped = false;
                }
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        //if the waiter has reached the selected table's trigger, he has reached the table
        if (other.gameObject == SelectedTable.gameObject)
        {
            IsOnTable = true;
            NavMeshAgent.isStopped = true;
            SelectedTable.gameObject.GetComponent<TableScript>().ShowOrder();
        }
    }

}
