using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DestroyBulletHole : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyHole());
    }
    IEnumerator DestroyHole()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
