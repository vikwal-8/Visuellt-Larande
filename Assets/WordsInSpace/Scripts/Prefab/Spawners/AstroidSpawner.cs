using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidSpawner : MonoBehaviour
{
  public int astroidFieldDimension = 10;
  public Astroid astroid1, astroid2, astroid3;
  private Astroid[] astroid = new Astroid[3];
  public int gridSpacing = 10;



  void Start(){
      astroid[0] = astroid1;
      astroid[1] = astroid2;
      astroid[2] = astroid3;
      PlaceAstroids();

    }

  void PlaceAstroids(){
      for(int x = -astroidFieldDimension; x < astroidFieldDimension; x += 1)
      {
          for(int y = -astroidFieldDimension; y < astroidFieldDimension; y+= 1)
          {
             for(int z = -astroidFieldDimension; z < astroidFieldDimension; z+= 1)
             {
                 InstantiateAstroid(x, y, z);
             }
          }
      }
  }

  void InstantiateAstroid(int x, int y, int z){
      Vector3 pos = new Vector3(transform.position.x + (x * gridSpacing) + AstroidOffset(),
                                transform.position.y + (y * gridSpacing) + AstroidOffset(), 
                                transform.position.z + (z * gridSpacing) + AstroidOffset());
      Instantiate(astroid[Random.Range(0,astroid.Length)], pos, Quaternion.identity, transform);

  }

  float AstroidOffset(){
      return Random.Range(-gridSpacing/2f, gridSpacing/2f);
  }
}
