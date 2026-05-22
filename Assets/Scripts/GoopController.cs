using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoopController : MonoBehaviour
{

    public GoopPlatform contactedPlatform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        contactedPlatform = collision.gameObject.GetComponent<GoopPlatform>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (contactedPlatform != null)
        {
            contactedPlatform.AddContact(transform.position);
            
        }
    }
}
