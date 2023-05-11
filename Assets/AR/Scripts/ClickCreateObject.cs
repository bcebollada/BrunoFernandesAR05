using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ClickCreateObject : MonoBehaviour
{
    public Camera cam;
    public GameObject cubePrefab;

    private bool alreadyPressed;

    public GameObject[] assetsToCreate;

    public int assetSelected; //this number will select the object in the array

    // Update is called once per frame
    void Update()
    {
        Vector2 pos;


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

        if(Physics.Raycast(ray, out RaycastHit hitInfo) && !alreadyPressed)
        {
            alreadyPressed = true;

            var instance = Instantiate(assetsToCreate[assetSelected], hitInfo.point + new Vector3(0, 0.2f, 0), Quaternion.identity);

            // Add an ARAnchor component if it doesn't have one already.
            if (instance.GetComponent<ARAnchor>() == null)
            {
                instance.AddComponent<ARAnchor>();
            }

            Debug.DrawRay(cam.transform.position, cam.transform.forward * 10, Color.red, 4f);

        }

    }
}
