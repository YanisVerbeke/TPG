using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DragRigidbody : MonoBehaviour
{
    public float forceAmount = 500;

    List<GameObject> paperObjects = new List<GameObject>();

    Rigidbody selectedRigidbody;
    Camera targetCamera;
    Vector3 originalScreenTargetPosition;
    Vector3 originalRigidbodyPos;
    float selectionDistance;

    // Start is called before the first frame update
    void Start()
    {
        targetCamera = GetComponent<Camera>();
        paperObjects = GameObject.FindGameObjectsWithTag("Paper").ToList();
    }

    void Update()
    {
        if (!targetCamera)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            //Check if we are hovering over Rigidbody, if so, select it
            selectedRigidbody = GetRigidbodyFromMouseClick();
            if (selectedRigidbody != null)
            {
                paperObjects = FillPaperList(selectedRigidbody.gameObject);
            }
        }
        if (Input.GetMouseButtonUp(0) && selectedRigidbody)
        {
            //Release selected Rigidbody if there any
            selectedRigidbody = null;
        }
    }

    void FixedUpdate()
    {
        if (selectedRigidbody)
        {
            Vector3 mousePositionOffset = targetCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance)) - originalScreenTargetPosition;
            Vector3 newVelocity = (originalRigidbodyPos + mousePositionOffset - selectedRigidbody.GetComponent<Rigidbody>().transform.position) * forceAmount * Time.deltaTime;
            if (newVelocity.y < 0)
            {
                selectedRigidbody.GetComponent<Rigidbody>().velocity = newVelocity;
                foreach (var item in paperObjects)
                {
                    item.GetComponent<Rigidbody>().velocity = selectedRigidbody.velocity;
                }
            }
        }
    }

    Rigidbody GetRigidbodyFromMouseClick()
    {
        RaycastHit hitInfo;
        Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition);
        bool hit = Physics.Raycast(ray, out hitInfo);
        if (hit && hitInfo.collider.gameObject.CompareTag("Paper"))
        {
            if (hitInfo.collider.gameObject.GetComponent<Rigidbody>())
            {
                selectionDistance = Vector3.Distance(ray.origin, hitInfo.point);
                originalScreenTargetPosition = targetCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance));
                originalRigidbodyPos = hitInfo.collider.transform.position;
                return hitInfo.collider.gameObject.GetComponent<Rigidbody>();
            }
        }

        return null;
    }

    List<GameObject> FillPaperList(GameObject selectedObject)
    {
        List<GameObject> objectList = new List<GameObject>();
        objectList = GameObject.FindGameObjectsWithTag("Paper").ToList();
        objectList.Remove(selectedObject);
        return objectList;
    }
}