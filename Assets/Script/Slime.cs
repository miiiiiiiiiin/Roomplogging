using System.Collections;
using UnityEngine;

public class Slime : MonoBehaviour
{

    private Transform playerTransform;
    private Rigidbody2D rigid;
    [SerializeField]
    private float MoveSpeed = 3f;
    public float HP = 10;
    public float AttackDamage = 2;
    private float colisionDamageTime = 1f;
    private float lastDamagedTime = 0f;
    private float colisionAttackTime = 0.3f;
    private float lastAttackTime = 0f;

    public ParticleSystem particle;
    private bool isDead= false;
    private bool isHit = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if(other.collider is CapsuleCollider2D && other.gameObject.tag == "Player"){//플레이어 무방비상태로 닿았을 때
            if(Time.time - lastDamagedTime >= colisionDamageTime){
                GameManager.instance.PlayerLife -=1;
                GameManager.instance.isOn = true;
                Debug.Log("dlrjs");
                lastDamagedTime = Time.time;
            }
            
        }
    }
    private void OnTriggerEnter2D(Collider2D other) // 슬라임이 데미지 입었을 때
    {
        if(other is CircleCollider2D){
            if(Time.time - lastAttackTime >= colisionAttackTime){
                lastAttackTime = Time.time;
                StartCoroutine(Attack());
            }
        }
    }

     IEnumerator Attack()
    {
        GetComponent<AudioSource>().Play();
        isHit = true;
        yield return new WaitForSeconds(0);
        HP -= AttackDamage;
        Debug.Log("마법봉 닿음");

        // 넉백
        Vector2 knockBack = (transform.position - playerTransform.position).normalized * 3f;
        rigid.AddForce(knockBack, ForceMode2D.Impulse);

        GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0.4f, 1f);
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
        isHit = false;

        if (HP <= 0) // HP <= 0로 변경
        {
            
            isDead = true;
            StartCoroutine(DestroySlime());
        }
    }

    IEnumerator DestroySlime(){
        GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        GameManager.instance.FirstSlimeCount--;
        
        
        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0f);
        particle.Play();
        if(GameManager.instance.FirstSlimeCount == 0){
            GameManager.instance.ResetSlimeCount();
        }
        yield return new WaitForSeconds(0.5f);
        

        Destroy(gameObject);
        
    }
}
