using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This object is paired with the ActivatedMover.cs component

public class TriggerObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToTrigger;
    [SerializeField] private LayerMask layerToCollide;
    [SerializeField] private bool deactivate = false;
    //[SerializeField] private bool disapearOnContact = false;

    private ActivatedObject theObject;

    private void Start()
    {
        theObject = objectToTrigger.GetComponent<ActivatedObject>();
        if(theObject == null) Debug.Log("Can't find object to activate");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Get the layer of the object we just collided with
        LayerMask theLayer = collision.gameObject.layer;
  
        //Stupid shit we have to do to see if the mask and the layer are the same
        if ((layerToCollide.value & (1 << theLayer.value)) != 0)
        {
            // yup
            Debug.Log("Mask found it");
            theObject.activate();

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        LayerMask theLayer = collision.gameObject.layer;

        //Stupid shit we have to do to see if the mask and the layer are the same
        if ((layerToCollide.value & (1 << theLayer.value)) != 0)
        {
            // yup
            Debug.Log("Mask found it");
            theObject.deactivate();

        }
    }

}
