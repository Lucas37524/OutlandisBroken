using System.Collections;

using System.Collections.Generic;

using UnityEngine;



[RequireComponent(typeof(Animator))]

public class EquipableItem : MonoBehaviour

{



    public Animator animator;





    // Start is called before the first frame update

    void Start()

    {

        animator = GetComponent<Animator>();

    }



    // Update is called once per frame

    void Update()

    {



        if (Input.GetMouseButtonDown(0) && // Left Mouse Button

            InventorySystem.Instance.isOpen == false &&

            CraftingSystem.instance.isOpen == false &&

            SelectionManager.Instance.handIsVisible == false

            )

        {



            animator.SetTrigger("hit");



        }



    }



    public void GetHit()

    {

        GameObject selectedTree = SelectionManager.Instance.selectedTree;



        if (selectedTree != null)

        {

            selectedTree.GetComponent<ChoppableTree>().GetHit();

        }

    }







}