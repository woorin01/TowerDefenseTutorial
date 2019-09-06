﻿using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool doMovement = true;

    public float panSpeed = 30f;
    public float scrollSpeed = 10f;
    public float panBoaderThickness = 10f;
    public float minY = 10f;
    public float maxY = 80f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            doMovement = !doMovement;

        if (!doMovement)
            return;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBoaderThickness)
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        if (Input.GetKey("s") || Input.mousePosition.y <= panBoaderThickness)
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        if (Input.GetKey("a") || Input.mousePosition.x <= panBoaderThickness)
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBoaderThickness)
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;

        pos.y -= scroll * 500f * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }
}