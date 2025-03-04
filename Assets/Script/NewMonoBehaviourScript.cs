using System.Collections;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private Transform playerTransform;
    private Rigidbody2D rigid;
    [SerializeField]
    private float MoveSpeed = 2f;

    public ParticleSystem particle;
    private bool isDead= false;
    private bool isHit = false;
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate(){
        if(!isDead && !isHit){
            Vector3 moveto = (playerTransform.position - transform.position).normalized; 
            transform.position += moveto * MoveSpeed * Time.deltaTime;
            rigid.linearVelocity = Vector2.zero;

        } 
    }

    public void DisSlime(){
        StartCoroutine(DestroySlime());
    }

    IEnumerator DestroySlime(){
        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0f);
        gameObject.GetComponent<AudioSource>().Play();
        particle.Play();
        yield return new WaitForSeconds(0.5f);
        

        Destroy(gameObject);
        
    }
}
