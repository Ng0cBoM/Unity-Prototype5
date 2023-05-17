using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CrosshairController : MonoBehaviour
{
    private new Camera camera;
    private Vector3 mousePos;
    public GameObject bulletHole;
    public GameObject bulletLoading;
    private bool isLoading;
    private int currentOnTargetCount;

    
    void Start()
    {
        camera = Camera.main;
        isLoading = false;
        currentOnTargetCount = 0;
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
            if (GameManager.instance.bulletsLeft > 0)
            {
                isLoading = false;
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 holePos = new Vector3(transform.position.x, transform.position.y, 2);
                    Instantiate(bulletHole, holePos, bulletHole.transform.rotation);
                    if (!GameManager.instance.isBonus) GameManager.instance.UpdateBullet();
                    currentOnTargetCount++;
                    
                    if (currentOnTargetCount > GameManager.instance.onTargetCount)
                    {
                        currentOnTargetCount = 0;
                        GameManager.instance.onTargetCount = 0;
                        GameManager.instance.isPowerUp = false;
                    }
                    else if (currentOnTargetCount >= 5)
                    {
                        GameManager.instance.isPowerUp = true;
                        UIManager.instance.UIBlinkingTimeTex();
                    }
                }
            }            
            else if (GameManager.instance.bulletsLeft == 0  && isLoading == false)
            {
                bulletLoading.SetActive(true);
                StartCoroutine(Loading());
            }
            UpdateMousePosition();
        }
    }
    IEnumerator Loading()
    {
        isLoading = true;
        yield return new WaitForSeconds(2);
        GameManager.instance.BulletReload();
        bulletLoading.SetActive(false);
    }




}
