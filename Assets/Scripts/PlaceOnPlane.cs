using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// Listens for touch events to create objects on a AR Plane based on AR Raycast
/// Originally take form Unity Technologies AR Foundation samples
/// <seealso cref="https://github.com/Unity-Technologies/arfoundation-samples/blob/master/Assets/Scripts/PlaceOnPlane.cs"/>
///
/// Will also perform a raycast and tell the object to spin <seealso cref="Spin"/>
/// </summary>
public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField]
    private ARRaycastManager raycastManager = null;

    [SerializeField]
    private GameObject prefabToPlace = null;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Start is called before the first frame update
    void Start()
    {
        if (raycastManager == null)
        {
            raycastManager = GetComponent<ARRaycastManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (SpinCubes(touch) == false)
                {
                    CreateCubes(touch);
                }
            }
        }
    }

    private void CreateCubes(Touch touch)
    {
        if (raycastManager.Raycast(touch.position, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            Pose post = hits[0].pose;
            Instantiate(prefabToPlace, post.position, post.rotation);
        }
    }

    private bool SpinCubes(Touch touch)
    {
        Ray ray = Camera.main.ScreenPointToRay(touch.position);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Debug.Log("Clicked " + hitInfo.transform.gameObject.name);
            if (hitInfo.transform.TryGetComponent(out Spin spin))
            {
                spin.StartSpin();
                return true;
            }
        }
        return false;
    }
}
