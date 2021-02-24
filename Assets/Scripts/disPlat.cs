using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AGDDPlatformer;

public class disPlat : MonoBehaviour
{
    public float killTime = 1.0f;
    public float respawnTime = 1.0f;
    private BoxCollider2D collider;
    private GameObject spriteObject;
    // Start is called before the first frame update
    void Start()
    {
        collider = gameObject.GetComponent<BoxCollider2D>();
        spriteObject = gameObject.transform.GetChild(0).gameObject;
        Debug.Log(spriteObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator enableCollider() {
        yield return new WaitForSeconds(respawnTime);
        collider.enabled = true;
        spriteObject.SetActive(true);
    }

    IEnumerator disableCollider() {
        yield return new WaitForSeconds(killTime);
        collider.enabled = false;
        spriteObject.SetActive(false);
        StartCoroutine("enableCollider");
        
    }

    void OnTriggerEnter2D (Collider2D other) {
        StartCoroutine("disableCollider");
    }
}
