using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePoint : MonoBehaviour
{
    private Camera camera;
    private Vector3 mousePos;
    private TrailRenderer trailRenderer;
    private BoxCollider boxCollider;
    private bool swiping = false;
    void Start()
    {
        camera = Camera.main;
        trailRenderer = GetComponent<TrailRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        trailRenderer.enabled = false;
        boxCollider.enabled = false;
    }
    void UpdateMousePosition()
    {
        mousePos = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        transform.position = mousePos;
    }
    void UpdateComponents()
    {
        trailRenderer.enabled = swiping;
        boxCollider.enabled = swiping;
    }
    void Update()
    {
        if (GameManager.instance.isGameActive && !GameManager.instance.isGamePaused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                swiping = true;
                UpdateComponents();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                swiping = false;
                UpdateComponents();
            }
            if (swiping)
            {
                UpdateMousePosition();
            }
        }
        /*mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        Debug.Log("Screen Position: " + Input.mousePosition + "_______" + "World Position: " + mousePos);*/

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Target>())
        {
            collision.gameObject.GetComponent<Target>().DestroyTarget();
        }
    }



}
