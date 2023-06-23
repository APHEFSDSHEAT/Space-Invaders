using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireOnSide : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileCoolDown = 0.1f;
    [SerializeField] AudioClip projectileSFX;
    [SerializeField][Range(0, 1)] float projectileSFXVolume;
    [SerializeField] float sideShotCooldown = 3f;
    float coolDown;

    Coroutine firingCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        coolDown = sideShotCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
        coolDown -= Time.deltaTime;
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        /*if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }*/
    }

    private IEnumerator FireContinuously()
    {
        while (coolDown < 0)
        {
            AudioSource.PlayClipAtPoint(projectileSFX, Camera.main.transform.position, projectileSFXVolume);
            GameObject laser = Instantiate(laserPrefab, transform.position, transform.rotation) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            coolDown = sideShotCooldown;
            yield return new WaitForSeconds(projectileCoolDown);
        }

    }


}

