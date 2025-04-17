using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSystem : MonoBehaviour
{
    public static EquipSystem Instance { get; set; }

    // -- UI -- //
    public GameObject quickSlotsPanel;

    public List<GameObject> quickSlotsList = new List<GameObject>();
    public List<string> itemList = new List<string>();


    public GameObject numbersHolder;

    public int selectedNumber = -1;
    public GameObject selectedItem;

    public GameObject ToolHolder;

    public GameObject selectedItemModel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    private void Start()
    {
        PopulateSlotList();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectQuickSlot(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectQuickSlot(2);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectQuickSlot(3);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectQuickSlot(4);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectQuickSlot(5);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectQuickSlot(6);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SelectQuickSlot(7);

        }


        void SelectQuickSlot(int number)
        {

            if (checkIfSlotIsFull(number) == true)
            {
                if (selectedNumber != number)
                {
                    selectedNumber = number;

                    // Unselect previouslt selected item
                    if (selectedItem != null)
                    {
                        selectedItem.gameObject.GetComponent<InventoryItem>().isSelected = false;
                    }

                    selectedItem = getSelectedItem(number);
                    selectedItem.GetComponent<InventoryItem>().isSelected = true;

                    SetEquippedModel(selectedItem);

                    // Changing the color

                    foreach (Transform child in numbersHolder.transform)
                    {
                        child.transform.Find("Text").GetComponent<Text>().color = Color.gray;
                    }

                    Text toBeChanged = numbersHolder.transform.Find("number" + number).transform.Find("Text").GetComponent<Text>();
                    toBeChanged.color = Color.white;
                }
                else // trying to select the same slot
                {
                    selectedNumber = -1; //null

                    // Unselect previouslt selected item
                    if (selectedItem != null)
                    {
                        selectedItem.gameObject.GetComponent<InventoryItem>().isSelected = false;
                    }

                    DestroySelectedItemModel();



                    // Changing the color

                    foreach (Transform child in numbersHolder.transform)
                    {
                        child.transform.Find("Text").GetComponent<Text>().color = Color.gray;
                    }

                }


            }
        }

        void SetEquippedModel(GameObject selectedItem)
        {

            DestroySelectedItemModel();

            string selectedItemName = selectedItem.name.Replace("(Clone)", "");
            selectedItemModel = Instantiate(Resources.Load<GameObject>(selectedItemName + "_Model")); //,
                                                                                                      //  new Vector3(0.42f, 0.97f, 0.522f), Quaternion.Euler(-3f, -88.5f, -2.93f));  // in vector 3 put the position of the axe and in the euler put the rotation you need
            selectedItemModel.transform.SetParent(ToolHolder.transform, false);
        }






        GameObject getSelectedItem(int slotNumber)
        {
            return quickSlotsList[slotNumber - 1].transform.GetChild(0).gameObject;
        }


        bool checkIfSlotIsFull(int slotNumber)
        {
            if (quickSlotsList[slotNumber - 1].transform.childCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }

    private void PopulateSlotList()
    {
        foreach (Transform child in quickSlotsPanel.transform)
        {
            if (child.CompareTag("QuickSlot"))
            {
                quickSlotsList.Add(child.gameObject);
            }
        }
    }



    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }

    public bool CheckIfFull()
    {

        int counter = 0;

        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount > 0)
            {
                counter += 1;
            }
        }

        if (counter == 7)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DestroySelectedItemModel()
    {
        if (selectedItemModel != null)
        {
            Destroy(selectedItemModel.gameObject);
            selectedItemModel = null;
        }
    }

}