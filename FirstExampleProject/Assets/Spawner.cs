using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    // Start is called before the first frame update

    public GameObject spawnablePrefab;
    public float spawnTime;
    void Start()
    {
        if(spawnTime <= 0){
            spawnTime = .1f;
        }
        StartCoroutine(doSpawnBehaviour());
    }

    IEnumerator doSpawnBehaviour(){

        while(true){

            yield return new WaitForSeconds(spawnTime);
            GameObject obj = Instantiate<GameObject>(spawnablePrefab);
            obj.transform.SetParent(this.transform);
            obj.transform.position = this.transform.position;
            obj.transform.rotation = this.transform.rotation;
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
