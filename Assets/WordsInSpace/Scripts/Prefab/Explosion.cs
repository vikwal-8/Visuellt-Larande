using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
   public ParticleSystem hitExplosion, destroyExplosion;

    public void BeenHit(Transform go){

        if (this.gameObject.GetComponent<EnemyMovment>().GetRecievedHits() < 1)
        {
            Instantiate(hitExplosion, go.position, go.rotation);
            this.gameObject.GetComponent<EnemyMovment>().UpdateRecievedHits();
        }
        else
        {
            Instantiate(destroyExplosion, go.position, go.rotation);
            Destroy(this.gameObject);
        }

    }

}
