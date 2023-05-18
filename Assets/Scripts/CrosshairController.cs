using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CrosshairController : MonoBehaviour
{
    private new Camera camera;
    private Vector3 mousePos;
    public GameObject bulletHole;
    public ParticleSystem shootParticleSystem;

    private bool isLoading;
    private int currentOnTargetCount;
    float timeLoadingLeft = 2f;


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
                    UIManager.instance.SetAnimationShooting();
                    Instantiate(shootParticleSystem, transform.position, shootParticleSystem.transform.rotation);
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
                UIManager.instance.SetFillCrosshairAmount(0);
                Loading();
            }
            UpdateMousePosition();
        }
    }

    void Loading()
    {
        timeLoadingLeft -= Time.deltaTime;
        UIManager.instance.SetFillCrosshairAmount((2f - timeLoadingLeft) / 2f);
        Debug.Log(timeLoadingLeft);
        if (timeLoadingLeft <= 0)
        {
            GameManager.instance.BulletReload();
            isLoading = true;
            timeLoadingLeft = 2f;
        }
    }
}
