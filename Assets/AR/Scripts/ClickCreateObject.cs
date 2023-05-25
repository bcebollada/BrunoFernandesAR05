using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class ClickCreateObject : MonoBehaviour
{
    public Camera cam;
    public GameObject cubePrefab;

    private bool alreadyPressed;

    public GameObject[] assetsToCreate;

    public int assetSelected; //this number will select the object in the array

    private ObjectOptions objectOptions;

    public TMP_Text selectedAssetText;

    private void Awake()
    {
        objectOptions = GetComponent<ObjectOptions>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos;
        selectedAssetText.text = "Selected asset: " + assetsToCreate[assetSelected].name;


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

        if(Physics.Raycast(ray, out RaycastHit hitInfo) && !alreadyPressed && (objectOptions.selectedObj == null))
        {
            if (hitInfo.collider.gameObject.CompareTag("Furniture")) return; //clicked on object

            if((hitInfo.normal.y < 1.2) && (hitInfo.normal.y > 0.8)) //planar is horizontal 
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
}
