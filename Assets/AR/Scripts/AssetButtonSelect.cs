using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetButtonSelect : MonoBehaviour
{
    public int assetNum;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        GameObject.Find("XR Origin").GetComponent<ClickCreateObject>().assetSelected = assetNum;
    }
}
