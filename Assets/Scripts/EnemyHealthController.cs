using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int currentHelth = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    public void DamgeEnemy(int damageAmount)
    {
        currentHelth -= damageAmount;

        if (currentHelth <= 0)
        {
            Destroy(gameObject);

            AudioManager.instance.PlaySFX(3);
        }
    }
}
