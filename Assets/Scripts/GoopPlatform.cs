using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoopPlatform : MonoBehaviour
{
    private List<Vector3> slimeContacts;
    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        slimeContacts = new List<Vector3>();
        rend = GetComponent<Renderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddContact(Vector3 contactPoint)
    {
        
        
        slimeContacts.Add(contactPoint);
        rend.material.SetVector("_Contact", contactPoint);
        
        
    }
}
