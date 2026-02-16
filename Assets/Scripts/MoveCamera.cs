using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float lookSpeed;
    public float moveSpeed;
    public float focusSpeed;
    public float scrollSpeed;
    private bool looking;
    private bool selected;
    public Camera camera;
    public Transform transform;
    public GameObject selectedObject;
    public GameObject selectionScreen;
    private Vector3 mousePosition;
    private bool isDragging;
    public GameObject placeObject;

    void Start()
    {
        camera = GetComponent<Camera>();
        transform = GetComponent<Transform>();
        selected = false;
        looking = false;
        lookSpeed = 0.5f;
        moveSpeed = 0.5f;
        focusSpeed = 0.05f;
        scrollSpeed = 1f;
        isDragging = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (looking)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                looking = false;
            } else {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                looking = true;
            }
        }

        if (looking)
        {
            float rotationX = Input.GetAxisRaw("Mouse X");
            float rotationY = Input.GetAxisRaw("Mouse Y");
            rotationX *= lookSpeed;
            rotationY *= lookSpeed;
            float lookYLimit = 80f;
            float pitch = transform.localEulerAngles.x - rotationY;
            pitch = Mathf.Clamp(pitch > 180 ? pitch - 360f : pitch, -lookYLimit, lookYLimit);
            float yaw = transform.localEulerAngles.y + rotationX;
            transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);
        } else {
            if (Input.GetMouseButtonDown(0))
            {
                mousePosition = Input.mousePosition;
            }
            if (Input.GetMouseButton(0))
            {
                Vector3 difference = mousePosition - Input.mousePosition;
                if (difference.magnitude > 0.5f && selected)
                {
                    isDragging = true;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (!isDragging)
                {
                    int layerMask = LayerMask.GetMask("CelestialBody");
                    RaycastHit hit;
                    Ray mouseRay = camera.ScreenPointToRay(Input.mousePosition);
                    bool isHit = Physics.Raycast(mouseRay, out hit, Mathf.Infinity, layerMask);
                    if (isHit)
                    {
                        if (!selected) //If no object was already is selected
                        {
                            selectedObject = hit.transform.gameObject;
                            selectedObject.GetComponent<Highlight>().SetOutline(true);//Open the highlight
                            selectionScreen.SetActive(true);
                            selected = true;
                            if (selectedObject.tag == "Star")
                            {
                                Star starObj = selectedObject.GetComponent<StarMovement>().starObject;
                                selectionScreen.GetComponent<Selections>().ActivateScreen(starObj);
                            } else if (selectedObject.tag == "Planet")
                            {
                                Planet planetObj = selectedObject.GetComponent<PlanetMovement>().planetObject;
                                selectionScreen.GetComponent<Selections>().ActivateScreen(planetObj);
                            }
                        } else {
                            selectedObject.GetComponent<Highlight>().SetOutline(false);
                            if (selectedObject != hit.transform.gameObject) //If the selected object is changed
                            {
                                selectedObject = hit.transform.gameObject;
                                selectedObject.GetComponent<Highlight>().SetOutline(true);
                                if (selectedObject.tag == "Star")
                                {
                                    Star starObj = selectedObject.GetComponent<StarMovement>().starObject;
                                    selectionScreen.GetComponent<Selections>().ActivateScreen(starObj);
                                } else if (selectedObject.tag == "Planet")
                                {
                                    Planet planetObj = selectedObject.GetComponent<PlanetMovement>().planetObject;
                                    selectionScreen.GetComponent<Selections>().ActivateScreen(planetObj);
                                } 
                            } else { //If an object is deselected
                                selected = false;
                                selectedObject = null;
                                selectionScreen.SetActive(false);
                            }
                        }
                    }
                } else {
                    Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                    Plane plane = new Plane(Vector3.up, selectedObject.transform.position);
                    if (plane.Raycast(ray, out float distance))
                    {
                        Vector3 point = ray.GetPoint(distance);
                        selectedObject.transform.position = point;
                    }
                    isDragging = false;
                }
            }
        }

        Vector3 tempPos = transform.position;
        float leftRight = Input.GetAxisRaw("Horizontal");
        Vector3 moveVector = transform.right * leftRight;
        float focusIn = Input.GetAxisRaw("Vertical");
        Vector3 focusVector = focusIn * transform.forward;
        Vector3 positionChange = focusVector * focusSpeed + moveVector * moveSpeed;
        transform.position = tempPos + positionChange;

        float zoom = Input.GetAxisRaw("Mouse ScrollWheel");
        float tempView = camera.orthographicSize;
        float zoomAmount = zoom * scrollSpeed;
        camera.orthographicSize = tempView + zoomAmount;

        if (Input.GetMouseButtonDown(1))
        {
            placeObject.SetActive(true);
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.forward, Vector3.zero);
            if (plane.Raycast(ray, out float distance))
            {
                Vector3 point = ray.GetPoint(distance);
                placeObject.GetComponent<AddObject>().mousePos = point;
            }
        }
    }
}
