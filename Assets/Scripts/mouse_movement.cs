using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse_movement : MonoBehaviour
{
    private float mouseSensitivity = 50f;
    public float ConstantMouseSensitivity = 50f;
    public Transform playerBody;
    public float xRotation = 0f;
    public float minLimit = -90f;
    public float maxLimit = 90f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mouseSensitivity = ConstantMouseSensitivity;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minLimit, maxLimit);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        //Zooming in
        ZoomIn();
    }

    void ZoomIn(){
        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * 10f;
        fov = Mathf.Clamp(fov, 0, 110);
        mouseSensitivity = ConstantMouseSensitivity * (fov/60f);
        Camera.main.fieldOfView=fov;
        //Debug.Log(fov);
    }

    public void ChangeMinMaxLimit(int x)
    {
        if(x==0){
            minLimit = -90f; maxLimit = 90f;
        }else if(x==1){
            minLimit = -160f; maxLimit = -60f;
            xRotation=-90f;
        }else if(x==2){
            minLimit = 60f; maxLimit = 160f;
            xRotation=90f;
        }
    }
}
