using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Сreature : MonoBehaviour
{
    [SerializeField] float HP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDamage(float dmg)
    {
        HP -= dmg;
        if(HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
