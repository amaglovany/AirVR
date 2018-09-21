using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlHandlerTwo : MonoBehaviour
{
    private RaycastHit hit;
    private Transform pickedObject;
    private float distance;
    private Vector3 newPosition;
    private Ray ray;

    private bool taken;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            taken = !taken;
        }

        if (taken)
        {
            ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0.5F));

            if (!pickedObject)
            {
                if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Pickable"))
                {
                    if (hit.rigidbody)
                    {
                        hit.rigidbody.velocity = Vector3.zero;
                    }

                    pickedObject = hit.transform;
                    pickedObject.GetComponent<Rigidbody>().useGravity = false;
                    distance = Vector3.Distance(pickedObject.position, Camera.main.transform.position);
                }
            }
            else
            {
                newPosition = ray.GetPoint(distance);
                pickedObject.GetComponent<Rigidbody>().MovePosition(newPosition);
            }
        }
        else
        {
            if (pickedObject != null)
                pickedObject.GetComponent<Rigidbody>().useGravity = true;

            pickedObject = null;
        }
    }
}