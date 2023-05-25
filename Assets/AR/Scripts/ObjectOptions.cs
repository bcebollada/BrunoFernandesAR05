using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;

public class ObjectOptions : MonoBehaviour
{
    public GameObject selectedObj;
    private bool alreadyPressed;
    public Camera cam;

    public float rotAmount;
    public float moveAmount;

    public GameObject uiOptions;

    private EventSystem eventSystem;


    private void Start()
    {
        eventSystem = EventSystem.current;
    }

    public void RotateLeft()
    {
        if(selectedObj == null) return;
        selectedObj.transform.Rotate(0, -2*  rotAmount,0);
    }

    public void RotateRight()
    {
        if (selectedObj == null) return;
        selectedObj.transform.Rotate(0, 2 * rotAmount, 0);
    }

    public void MoveUp()
    {
        if (selectedObj == null) return;
        selectedObj.transform.position += selectedObj.transform.forward * moveAmount;
    }

    public void MoveDown()
    {
        if (selectedObj == null) return;
        selectedObj.transform.position += -selectedObj.transform.forward * moveAmount;
    }

    public void MoveRight()
    {
        if (selectedObj == null) return;
        selectedObj.transform.position += selectedObj.transform.right * moveAmount;
    }

    public void MoveLeft()
    {
        if (selectedObj == null) return;
        selectedObj.transform.position += -selectedObj.transform.right * moveAmount;
    }

    public void DestroyObj()
    {
        if (selectedObj == null) return;
        Destroy(selectedObj.gameObject);
    }

    private void Update()
    {
        Vector2 pos;

        if (selectedObj != null)
        {
            uiOptions.SetActive(true);

            // Get the screen position of the selected game object
            Vector3 objectScreenPosition = Camera.main.WorldToScreenPoint(selectedObj.transform.position);

            // Set the UI element's position to match the selected game object's screen position
            uiOptions.transform.position = objectScreenPosition;
        }
        else uiOptions.SetActive(false);


#if UNITY_EDITOR
        if (Input.GetMouseButton(0) == false)
        {
            alreadyPressed = false;
            return;
        }


        pos = Input.mousePosition;


#else
        if(Input.touchCount == 0)
        {

            alreadyPressed = false;
            return;
        }
            

        pos = Input.GetTouch(0).position;

#endif

        Debug.Log($"Clicked: {pos}");

        Ray ray = cam.ScreenPointToRay(pos);

        if (Physics.Raycast(ray, out RaycastHit hitInfo) && !alreadyPressed)
        {
            // Check if the hit object is a UI element
            if (eventSystem.IsPointerOverGameObject())
            {
                // The raycast hit a UI element, do not proceed with object selection
                return;
            }
            else
            {
                foreach (UnityEngine.Touch touch in Input.touches)
                {
                    int id = touch.fingerId;
                    if (EventSystem.current.IsPointerOverGameObject(id))
                    {
                        // ui touched
                        return;
                    }
                }
            }

            alreadyPressed = true; 
            if(hitInfo.collider != null)
            {
                if (hitInfo.collider.transform.gameObject.CompareTag("Furniture"))
                {
                    Debug.Log("Furniture hitted: " + hitInfo.transform.gameObject.name);
                    
                    GameObject clickedObj = hitInfo.collider.transform.gameObject;

                    if (clickedObj == selectedObj) //user clicked on the obj already selected
                    {
                        //selectedObj = null;
                    }
                    else selectedObj = clickedObj; //is a new obj
                 }
                else //user clicked on enviorment
                {
                    selectedObj = null; 
                }
            }
        }
        


    }
}
