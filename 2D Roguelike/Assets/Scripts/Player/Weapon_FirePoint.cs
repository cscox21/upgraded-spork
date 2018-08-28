 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_FirePoint : MonoBehaviour
{
    public float fireRate = 0;
    public float Damage = 10;
    public LayerMask whatToHit;

    public Transform projectile;
    public Transform muzzleFlashPrefab;
    float timeToSpawnEffect = 0;
    public float effectSpawnRate = 10;

    float timeToFire = 0;
    Transform firePoint;

    public string shotSoundName;
    private AudioManager audioManager;

    private void Awake()
    {
        firePoint = transform.Find("FirePoint");
        if(firePoint ==null)
        {
            Debug.LogError("No FirePoint?");
        }
    }

    private void Start()
    {
        //caching
        audioManager = AudioManager.instance;
        
        if (audioManager == null)
        {
            Debug.LogError("FREAK OUT! No AudioManager found in the scene.");
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();  
            }
        }
        else
        {
            if(Input.GetButton("Fire1") && Time.time > timeToFire)
            {

                timeToFire = Time.time + 1 / fireRate;
                Shoot(); 
            }
        }
	}
    void Shoot()
    {
        GetWorldPositionOnPlane(gameObject.transform.position, 0f);
        
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        //Vector2 mousePosition2 = new Vector2(Camera.current.ScreenToWorldPoint(Input.mousePosition).x, Camera.current.ScreenToWorldPoint(Input.mousePosition).y);
        
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);
        
        if (Time.time >= timeToSpawnEffect)
        {
            Effect();
            timeToSpawnEffect = Time.time + 1 /effectSpawnRate;
        }
        //Debug.DrawLine(firePointPosition, (mousePosition-firePointPosition)*100, Color.cyan);
        if(hit.collider!=null)
        {
            //Debug.DrawLine(firePointPosition, hit.point, Color.red);
            //Debug.Log("We hit " + hit.collider.name + " and did " + Damage + " damage.");
        }
    }
    void Effect()
    {
        audioManager.PlaySound(shotSoundName);
        Instantiate(projectile, firePoint.position, firePoint.rotation);
        Transform clone = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
        clone.parent = firePoint;
        float size = Random.Range(0.6f, 0.9f);
        clone.localScale = new Vector3(size, size, size);
        Destroy(clone.gameObject, .02f);
    }

    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}
