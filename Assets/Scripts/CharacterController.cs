using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using Assets.Scripts.Classes;
public class CharacterController : MonoBehaviour {
    
    private Animator Animator;
    private Transform SelectedTable;
    private NavMeshAgent NavMeshAgent;

    public GuiPhoneButtonScript GuiPhone;

    void Start()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
    }

    void Update () {
        DetectTableSelection();
        Animator.SetFloat("MoveSpeed", Mathf.Abs(NavMeshAgent.velocity.z) + Mathf.Abs(NavMeshAgent.velocity.x));
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                if (objectHit.gameObject.tag == "Table")
                {
                    SelectedTable = objectHit;
                    NavMeshAgent.SetDestination(objectHit.position);
                }
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        //if the waiter has reached the selected table's trigger, he has reached the table
        if (other.gameObject == SelectedTable.gameObject)
        {
            SelectedTable.gameObject.GetComponent<TableScript>().ShowOrder();
        }
    }

}
