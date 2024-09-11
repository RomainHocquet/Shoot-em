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

}
