using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float moveSpeed, lifeTime;

    public Rigidbody theRB;

    public GameObject impactEffect;

    public int damage = 1;

    public bool damageEnemy, damagePlayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = transform.forward * moveSpeed;
        lifeTime -= Time.deltaTime;
        
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Damage Enemy
        if(other.gameObject.tag == "Enemy" && damageEnemy)
        {
            //Destroy(other.gameObject);
            other.gameObject.GetComponent<EnemyHealthController>().DamgeEnemy(damage);
        }

        //Damage HeadShot Enemy
        if(other.gameObject.tag == "HeadShot" && damageEnemy)
        {
            other.transform.parent.gameObject.GetComponent<EnemyHealthController>().DamgeEnemy(damage * 5);
        }

        //Damage Player
        if(other.tag == "Player" && damagePlayer)
        {
            //Debug.Log("Hit Player " + transform.position);
            PlayerHealthController.instance.DamgePlayer(damage);
        }

        Destroy(gameObject);
        Instantiate(impactEffect, transform.position + (transform.forward * (-moveSpeed * Time.deltaTime)), transform.rotation);

    }
}
