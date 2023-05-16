using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CrosshairController : MonoBehaviour
{
    private new Camera camera;
    private Vector3 mousePos;
    public GameObject bulletHole;
    public GameObject background;
    void Start()
    {
        camera = Camera.main;
    }
    void UpdateMousePosition()
    {
        mousePos = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));
        transform.position = mousePos;
    }
    void Update()
    {
        if (GameManager.instance.isGameActive && !GameManager.instance.isGamePaused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 holePos = new Vector3(transform.position.x, transform.position.y, 2);
                Instantiate(bulletHole, holePos, bulletHole.transform.rotation);
            }
            UpdateMousePosition();
        }
    }




}
