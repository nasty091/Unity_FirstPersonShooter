using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int damage = 25;

    public bool damageEnemy, damagePlayer;

    private void OnTriggerEnter(Collider other)
    {
        //Damage Enemy
        if (other.gameObject.tag == "Enemy" && damageEnemy)
        {
            //Destroy(other.gameObject);
            other.gameObject.GetComponent<EnemyHealthController>().DamgeEnemy(damage);
        }

        //Damage Player
        if (other.tag == "Player" && damagePlayer)
        {
            //Debug.Log("Hit Player " + transform.position);
            PlayerHealthController.instance.DamgePlayer(damage);
        }
    }
}
