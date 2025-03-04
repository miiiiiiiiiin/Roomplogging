using System.Collections;

using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float movespeed = 10f;
    private Animator animator;
    private bool isWalking = false; 
    public bool isAttack = true;
    private Rigidbody2D rigid;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 moveTo = new Vector3(horizontalInput, verticalInput, 0f).normalized;
        rigid.linearVelocity = Vector2.zero;
        if(moveTo.x != 0 || moveTo.y != 0){
            transform.position += moveTo * movespeed* Time.deltaTime;
            isWalking = true;
            animator.SetBool("isWalking", true);
            animator.SetFloat("DirX", moveTo.x);
            animator.SetFloat("DirY", moveTo.y);
        }
        else if(moveTo.x == 0 || moveTo.y == 0){
            animator.SetBool("isWalking", false);
        }
        // 공격 클릭
        //if(Input.GetMouseButtonDown(0) && isAttack){
        if(Input.GetKey(KeyCode.Space) && isAttack){
                StartCoroutine(Attacked());
        }else{
            if(isAttack){
                animator.SetBool("isAttack",false);
            }
        }
         if(GameManager.instance.isOn){
            StartCoroutine(isOn());            
         }   
    }
       IEnumerator isOn(){
        GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0.4f, 1f);
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    IEnumerator Attacked(){
        //GetComponent<AudioSource>().Play();
        isAttack = false;
        animator.SetBool("isAttack",true);
        yield return new WaitForSeconds(0.05f);
        animator.SetBool("isAttack",false);
        isAttack = true;
    }
    
}
