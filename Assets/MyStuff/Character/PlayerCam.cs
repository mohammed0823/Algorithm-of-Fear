using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    public Text text;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {

        if (GameUIControl.IsPaused | QuizManage.examActive)
            return;
        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        
        Ray selector = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        
        //Ray selector = Camera.main.ScreenPointToRay(Input.mousePosition);

        int layerMask = ~LayerMask.GetMask("Player");

        if (Physics.Raycast(selector, out RaycastHit hit, 5, layerMask))
        {
            GameObject selectedObject = hit.collider.gameObject;

            if (selectedObject.CompareTag("Selectable"))
            {
                selectedObject = hit.collider.gameObject;
                text.text = selectedObject.name;

                if (Input.GetKeyDown(KeyCode.E))
                {

                    Interactable interactable = selectedObject.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        interactable.Interact(selectedObject);
                    }
                }
            }
            else
            {
                text.text = "";
            }
        }
        else{
            text.text = "";
        }
    }
}
