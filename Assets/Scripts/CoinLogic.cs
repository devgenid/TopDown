using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinLogic : MonoBehaviour
{
    private AudioSource audioSource;
    private MeshRenderer meshRenderer;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward,1.0f);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            meshRenderer.enabled = false; //for components enabled = false, for gameobjects setactive
            audioSource.Play();
            Invoke("DestroyCoin", 0.3f);
            
        }
        
    }

    private void DestroyCoin ()
    {
        Destroy(gameObject);
    }
}
