using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunContainerSC : MonoBehaviour
{

    public GameObject myGun;
    [HideInInspector]
    public GunStats myGunStats;
    // Start is called before the first frame update
    void Start()
    {
        myGunStats = myGun.GetComponent<GunStats>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReplaceGun(GameObject newGun)
    {
        Destroy(myGun);
        myGun = Instantiate(newGun, transform);
        myGunStats = myGun.GetComponent<GunStats>();
    }


    public void Shoot(PlayerStats shootingPlayer)
    {

        myGunStats.audioSource.Play();

        // Apply random spread to the original rotation
        foreach (GameObject muzzle in myGunStats.muzzles)
        {

            // Calculate random spread rotation
            Quaternion randomSpread = Quaternion.Euler(
                0, // X axis
                Random.Range(-myGunStats.spreadAngle, myGunStats.spreadAngle), // Random angle for Y axis
                0  // Z axis 
            );
            Quaternion bulletRotation = muzzle.transform.rotation * randomSpread;

            BulletSC shootedBullet = Instantiate(myGunStats.bullet, muzzle.transform.position, bulletRotation).GetComponent<BulletSC>();
            shootedBullet.owner = shootingPlayer;
            shootedBullet.damage *= myGunStats.damageMultipler;
            shootedBullet.speed *= myGunStats.speedMultipler;
        }

    }

}
