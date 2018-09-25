using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlHandlerThree : MonoBehaviour
{
    private RaycastHit hit;
    private Transform pickedObject;
    private float distance;
    private Vector3 newPosition;
    private Ray ray;

    private bool triedToTake;

    private float timeWhenClicked;
    private float timeWhenPointed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            triedToTake = !triedToTake;
            timeWhenClicked = Time.timeSinceLevelLoad;
        }

        if (triedToTake)
        {
            ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0.5F));

            if (!pickedObject)
            {
                timeWhenPointed = Time.timeSinceLevelLoad - timeWhenClicked;
                if (timeWhenPointed > 0.05F)
                {
                    triedToTake = false;
                    return;
                }

                if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Pickable"))
                {
                    if (hit.rigidbody)
                    {
                        hit.rigidbody.velocity = Vector3.zero;
                    }

                    pickedObject = hit.transform;
                    distance = Vector3.Distance(pickedObject.position, Camera.main.transform.position);
                    if (distance > 2.2F)
                    {
                        pickedObject = null;
                        Basket.Instance.isTaken = false;
                        triedToTake = false;
                        return;
                    }

                    distance = 0.5F;
                    pickedObject.GetComponent<Rigidbody>().useGravity = false;
                    Scanner.Instance.isTaken = true;
                    // ToDO: x=-45 rotation
                    //Scanner.Instance.gameObject.transform.rotation = new Quaternion(-45f, 0f, 0f, 0f);
                    GameplayUI.Instance.DisplayMessage("Check the Basket through the Gate", 26,
                        Color.yellow);
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
            Scanner.Instance.isTaken = false;
        }
    }
}