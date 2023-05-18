using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveOnCollision : MonoBehaviour
{
    private bool isDissolving;

    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (isDissolving) return;
        StartCoroutine(Dissolve());
        
    }

    private IEnumerator Dissolve()
    {
        isDissolving = true;
        float amount = 0;

        while(amount < 1)
        {
            amount += Time.deltaTime*2;

            GetComponent<Renderer>().material.SetFloat("_Amount", amount);
            yield return null;

        }

        Destroy(gameObject);
    }
}
