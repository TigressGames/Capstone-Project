using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;

public class DragAndDrop : MonoBehaviour
{
    public OVRInput.Controller controller;
    private GameObject grabbedObject;

    public GameObject BigExplosion;
    public GameObject SmallExplosion;
    
    void Update()
    {
        #region VRController
        // Get the currently active VR device
        var vrDevice = VRDevice.Device;
        if (vrDevice == null)
            return;

        // Get the primary input device (the controller)
        var inputDevice = vrDevice.PrimaryInputDevice;
        if (inputDevice == null)
            return;
        #endregion

        if (inputDevice.GetButtonDown(VRButton.One))
        {
            Click();
        }

        if (inputDevice.GetButtonUp(VRButton.One))
        {
            Drop();
        }
    }

    private void Click()
    {
        var hitResult = VRDevice.Device.PrimaryInputDevice.Pointer.CurrentRaycastResult;
        if (hitResult.gameObject != null)
        {
            if (hitResult.gameObject.tag == "Object")
            {
                grabbedObject = hitResult.gameObject;
                grabbedObject.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                grabbedObject.transform.position = transform.position;
                grabbedObject.transform.parent = transform;
            }
        }
    }

    private void Drop()
    {
        if (grabbedObject != null)
        { 
            if (grabbedObject != null && grabbedObject.gameObject.name == "ShatterCube")
            {
                BigBoom();
            }
            else
            {
                if (grabbedObject != null && grabbedObject.gameObject.name == "BigShatter")
                {
                    Debug.Log("Small explosion");
                    SmallBoom();
                }
                else
                {
                    grabbedObject.transform.parent = null;
                    grabbedObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;

                    grabbedObject.gameObject.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(controller);

                    grabbedObject = null;

                }
            }
        }
    }

    private void BigBoom()
    {
         grabbedObject.transform.parent = null;
         grabbedObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;

         grabbedObject.gameObject.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(controller);

         grabbedObject = null;

         Instantiate(BigExplosion, gameObject.transform.position, Quaternion.identity);
         Destroy(grabbedObject);
    }

    private void SmallBoom()
    {
        grabbedObject.transform.parent = null;
        grabbedObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;

        grabbedObject.gameObject.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(controller);

        grabbedObject = null;

        Instantiate(SmallExplosion, gameObject.transform.position, Quaternion.identity);
        Destroy(BigExplosion);
    }
}
