using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        StartCoroutine(PlayAudio());      
    }

    private IEnumerator PlayAudio()
    {
        this.GetComponent<AudioSource>().Play();
        foreach (Renderer renderer in this.GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = false;
        }
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
}
