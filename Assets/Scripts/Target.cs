using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private BoxCollider targetCollide;
    public ParticleSystem explosionParticle;
    public ParticleSystem boomParticle;
    public GameObject scoreEffect;

    private float minSpeed = 12;
    private float maxSpeed = 18;
    private float maxTorque = 10;
    private float xRange = 6;
    private float ySpawnPos = -6;

    public TextMeshProUGUI scoreEffectText;

    public int pointValue;

    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        targetCollide = GetComponent<BoxCollider>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(),RandomTorque(),RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
        scoreEffectText = scoreEffect.GetComponentInChildren<TextMeshProUGUI>();
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
    private void Update()
    {
        if (transform.position.y > 0)
        {
            targetCollide.enabled = true;
        }
        else if (transform.position.y < -11)
        {
            Destroy(gameObject);
        }
        else
        {
            targetCollide.enabled = false;
        }
    }

    private void OnMouseDown()
    {
        if (GameManager.instance.isGameActive && !GameManager.instance.isGamePaused && GameManager.instance.bulletsLeft > 0)
        {
            if (gameObject.CompareTag("Bad"))
            {
                scoreEffectText.text = "BOOM!!!!";
                Instantiate(boomParticle, transform.position, boomParticle.transform.rotation);
                GameManager.instance.GameOver();   
            }
            else if (gameObject.CompareTag("Bonus"))
            {
                scoreEffectText.text = "+"+pointValue;
                Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
                GameManager.instance.UpdateScore(pointValue);
                GameManager.instance.StartBonus();
            }
            else
            {
                scoreEffectText.text = "+"+pointValue;
                Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
                GameManager.instance.UpdateScore(pointValue);
            }
            Instantiate(scoreEffect, transform.position, scoreEffect.transform.rotation);
            Destroy(gameObject);
            GameManager.instance.onTargetCount++;
            if (GameManager.instance.isPowerUp)
            {
                GameManager.instance.timeLeft += 2;
            }
        }
    }
}
