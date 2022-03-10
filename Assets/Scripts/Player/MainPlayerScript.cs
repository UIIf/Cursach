using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainPlayerScript :MonoBehaviour, Humanoid
{
    [SerializeField] private float _HP;
    [SerializeField] private GameObject hand;

    private CamScript camScript;
    private Animator animator;

    rayWeapon[] holded_guns = { null, null };

    void Awake()
    {
        camScript = GetComponent<CamScript>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (holded_guns[0] != null)
            {
                holded_guns[0].Shoot(camScript.targetPoint);
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (holded_guns[0] != null)
            {
                animator.SetBool("nowOneHanded", false);
                animator.SetBool("nowTwoHanded", false);
                holded_guns[0].DropGun();
                holded_guns[0] = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(0);
    }

    private void Death()
    {
        print("You're dead");
    }

    public void GetDamage(float dmg)
    {
        _HP -= dmg;
        if (_HP <= 0)
        {
            Death();
        }
    }

    public float GetHP()
    {
        return _HP;
    }

    private void PlaceGun(GameObject gun)
    {
        Transform newGunTrans = gun.transform;
        newGunTrans.parent = hand.transform;
        newGunTrans.localPosition = Vector3.zero;
        newGunTrans.localRotation = Quaternion.Euler(180, 90, 90);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Weapon" || other.gameObject.GetComponent<dropWeaponScript>()) return;

        GameObject newGun = other.gameObject;
        if (newGun.GetComponent<Rigidbody>())
        {
            Destroy(newGun.GetComponent<Rigidbody>());
            Destroy(newGun.GetComponent<dropWeaponScript>());
        }
        BoxCollider[] boxcol = newGun.GetComponents<BoxCollider>();
        for (int i = 0; i < boxcol.Length; i++)
        {
            boxcol[i].enabled = false;
        }

        animator.SetBool("nowTwoHanded", true);
        PlaceGun(newGun);
        holded_guns[0] = newGun.GetComponent<rayWeapon>();
    }
}
