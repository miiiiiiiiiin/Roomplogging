using System.Collections;
using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{

    public GameObject Slime;
    private int SlimeCount=0;
    private bool isSpawnning = false;
    [SerializeField]
    private float Slimeinterval = 1f;
    [SerializeField]
    private int MaxSlime = 4;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //StartCoroutine(SlimeSpawn());
        
    }

    private void StartSlimeSpawner(){
        if(SlimeCount<=MaxSlime){
            isSpawnning = true;
            SlimeCount++;
            StartCoroutine(SlimeSpawn());
        }
    }

    IEnumerator SlimeSpawn(){
        yield return new WaitForSeconds(0.06f);
        
        Instantiate(Slime, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(Slimeinterval);
        isSpawnning = false;
        
    }
    // Update is called once per frame
    void Update()
    {
        if(!isSpawnning){
            
            StartSlimeSpawner();
        }
        
        
    }
}
