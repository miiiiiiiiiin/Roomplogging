using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private Image Fade;
    [SerializeField]
    private GameObject next;
    public GameObject chat;
    public TMP_Text chattext;
    public List<string> textlist;
    [SerializeField]
    private float FadeSpeed = 2.0f;

    private bool isCliked = false;
    private string nickname;
    public GameObject TrashCan;//쓰레기통
    public GameObject ChairPlayer; // 의자 플레이어
    public GameObject Player;// 일어난 플레이어
    public GameObject Slime;// 슬라임 
    private Animator animator;
    public AudioSource ChairSound;
    public AudioSource NoiseKnown;


    void Start()
    {
        
        //list = new List<string>();
        StartCoroutine(FadeOut());
        nickname = PlayerPrefs.GetString("name");
        animator = Player.GetComponent<Animator>();

    }

    IEnumerator FadeOut(){
        float alpha = 1.0f;
        while(alpha > 0){
            alpha -= FadeSpeed * Time.deltaTime;
            Fade.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSeconds(0.01f);
        }
        
        StartCoroutine(ShowMessege(0));
    }
    IEnumerator ShowMessege(int num)
    {
        yield return new WaitForSeconds(1.8f);
        chattext.text = "엄마: ";
        chat.SetActive(true);
        
        string dialoge = string.Format(textlist[num], nickname);
        for(int i = 0; i<dialoge.Length; i++)
        {
            

            yield return new WaitForSeconds(0.1f);
            isCliked = true;
            GetComponent<AudioSource>().Play();
            //chattext.text += textlist[num][i];
            chattext.text += dialoge[i];
            yield return null;
            // if(Input.GetMouseButtonDown(0)){
            //     Debug.Log("눌러짐");
            //     chattext.text = textlist[num];
            //     break;
            // }            
        }
        
        while(!Input.GetMouseButtonDown(0)){//클릭을 기다림
            next.SetActive(true);
            yield return null;
        }

        chat.SetActive(false);
        next.SetActive(false);

        chattext.text = null;
        yield return new WaitForSeconds(2f);

        if (num + 1 < 3) {// 🔹 인덱스 초과 방지

            StartCoroutine(ShowMessege(num + 1));
        }else{
            StartCoroutine(GetUpSlime());
        }

    }

    IEnumerator GetUpSlime(){

        yield return new WaitForSeconds(0);
        TrashCan.GetComponent<AudioSource>().Play();
        TrashCan.GetComponent<Animator>().SetTrigger("istrash");//쓰레기통 흔들림림
        yield return new WaitForSeconds(1f);
        ChairPlayer.GetComponent<AudioSource>().Stop();//타자소리 멈춤춤
        ChairPlayer.GetComponent<Animator>().SetTrigger("isSound");// 플레이어 타자 멈춤춤
        yield return new WaitForSeconds(1f);
        //FindAnyObjectByType<BubbleCaht>().BubbleChat();// 대화창 [?]

        chat.SetActive(true);        
        chattext.text = nickname+": ";// 이게무슨소리지? 
        for(int i =0; i< textlist[3].Length; i++){
            yield return new WaitForSeconds(0.1f);
            GetComponent<AudioSource>().pitch = 1.2f;
            GetComponent<AudioSource>().Play();
            //chattext.text += textlist[num][i];
            chattext.text += textlist[3][i];
            yield return null;
        }
        while(!Input.GetMouseButtonDown(0)){//클릭을 기다림
            next.SetActive(true);
            yield return null;
        }

        chat.SetActive(false);
        next.SetActive(false);

        chattext.text = null;

        
        yield return new WaitForSeconds(2f);
        //의자에서 내려서 중앙에 섬
        ChairSound.Play();
        ChairPlayer.SetActive(false);
        
        Player.SetActive(true);
        yield return new WaitForSeconds(1f);
        Player.GetComponent<AudioSource>().Play();
        animator.SetBool("isWalking", true);  
        yield return new WaitForSeconds(2f);

        chat.SetActive(true);        
        chattext.text = nickname+": ";// 여기서 이상한소리가,
        for(int i =0; i< textlist[4].Length; i++){
            yield return new WaitForSeconds(0.1f);
            //GetComponent<AudioSource>().pitch = 1f;
            GetComponent<AudioSource>().Play();

            //chattext.text += textlist[num][i];
            chattext.text += textlist[4][i];
            yield return null;
        }
        while(!Input.GetMouseButtonDown(0)){//클릭을 기다림
            next.SetActive(true);
            yield return null;
        }

        chat.SetActive(false);
        next.SetActive(false);

        chattext.text = null;

        yield return new WaitForSeconds(3f);

        
        //Slime.GetComponent<AudioSource>().Play(); 
        Slime.SetActive(true);//슬라임이 튀어나옴
        Slime.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.7f);

        animator.SetTrigger("isSliming");//개놀람
        //FindAnyObjectByType<BubbleCaht>().BubbleChat2();//개놀람대화창

        chat.SetActive(true);        
        chattext.text = nickname+": ";// 으악이게뭐야야
        for(int i =0; i< textlist[5].Length; i++){
            yield return new WaitForSeconds(0.1f);
            //GetComponent<AudioSource>().pitch = 1f;
            GetComponent<AudioSource>().Play();

            //chattext.text += textlist[num][i];
            chattext.text += textlist[5][i];
            yield return null;
        }
        while(!Input.GetMouseButtonDown(0)){//클릭을 기다림
            next.SetActive(true);
            yield return null;
        }

        chat.SetActive(false);
        next.SetActive(false);

        chattext.text = null;

        yield return new WaitForSeconds(0.5f);
        FindAnyObjectByType<Faderscript>().Whitefade();//갑자기 빛이 꺼졋다가 커짐 (라이트 투디 써클 래디어스 축소->확대)
        yield return new WaitForSeconds(3f);
        chattext.text = nickname+": ";
        chat.SetActive(true);        
        // 뭐야 이 마법봉은! 
        for(int i =0; i< textlist[6].Length; i++){
            yield return new WaitForSeconds(0.1f);
            GetComponent<AudioSource>().Play();
            chattext.text += textlist[6][i];
            yield return null;
        }
        while(!Input.GetMouseButtonDown(0)){//클릭을 기다림
            next.SetActive(true);
            yield return null;
        }

        chat.SetActive(false);
        next.SetActive(false);

        chattext.text = null;
        //StartCoroutine(Chatbox(5));//뭐야이마법봉은은  
string dialoge = string.Format(textlist[7], nickname);
        chattext.text = "???: ";

        NoiseKnown.GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().pitch = 0.7f;
        chat.SetActive(true);        
        // 뭐야 이 마법봉은! 
        for(int i =0; i< dialoge.Length; i++){
            yield return new WaitForSeconds(0.1f);
            GetComponent<AudioSource>().Play();
            chattext.text += dialoge[i];
            yield return null;
        }
        while(!Input.GetMouseButtonDown(0)){//클릭을 기다림
            next.SetActive(true);
            yield return null;
        }

        chat.SetActive(false);
        next.SetActive(false);

        chattext.text = null;
        //chat.GetComponent<AudioSource>().playOnAwake = false;  
        //StartCoroutine(Chatbox(6));//넌이제부터어쩌구
        yield return new WaitForSeconds(1f);


        GetComponent<AudioSource>().pitch = 1.2f;
        chattext.text = nickname+": ";
        chat.SetActive(true);        
        // 뭐야 이 마법봉은! 
        for(int i =0; i< textlist[8].Length; i++){
            yield return new WaitForSeconds(0.1f);
            GetComponent<AudioSource>().Play();
            chattext.text += textlist[8][i];
            yield return null;
        }
        while(!Input.GetMouseButtonDown(0)){//클릭을 기다림
            next.SetActive(true);
            yield return null;
        }

        chat.SetActive(false);
        next.SetActive(false);

        chattext.text = null;
        //StartCoroutine(Chatbox(7));//너누기야
        yield return new WaitForSeconds(1f);


        chattext.text = "???: ";
        //chat.GetComponent<AudioSource>().playOnAwake = true;  
        GetComponent<AudioSource>().pitch = 0.7f;
        chat.SetActive(true);        
        // 뭐야 이 마법봉은! 
        for(int i =0; i< textlist[9].Length; i++){
            yield return new WaitForSeconds(0.1f);
            GetComponent<AudioSource>().Play();
            chattext.text += textlist[9][i];
            yield return null;
        }
        while(!Input.GetMouseButtonDown(0)){//클릭을 기다림
            next.SetActive(true);
            yield return null;
        }

        chat.SetActive(false);
        next.SetActive(false);

        chattext.text = null;
        //chat.GetComponent<AudioSource>().playOnAwake = false;  
        //StartCoroutine(Chatbox(8));//만약다못하면사망합니다.
        yield return new WaitForSeconds(1f);


        // GetComponent<AudioSource>().pitch = 1.2f;
        // chattext.text = nickname+": ";
        chattext.text = "???: ";
        chat.SetActive(true);        
        // 뭐야 이 마법봉은! 
        for(int i =0; i< textlist[10].Length; i++){
            yield return new WaitForSeconds(0.1f);
            GetComponent<AudioSource>().Play();
            chattext.text += textlist[10][i];
            yield return null;
        }
        while(!Input.GetMouseButtonDown(0)){//클릭을 기다림
            next.SetActive(true);
            yield return null;
        }

        chat.SetActive(false);
        next.SetActive(false);

        chattext.text = null;
        //StartCoroutine(Chatbox(9));//너무극단적인거아니에요
        yield return new WaitForSeconds(1f);


        chattext.text = "???: ";
        //chat.GetComponent<AudioSource>().playOnAwake = true;  
        GetComponent<AudioSource>().pitch = 0.7f;
        chat.SetActive(true);        
        // 뭐야 이 마법봉은! 
        for(int i =0; i< textlist[11].Length; i++){
            yield return new WaitForSeconds(0.1f);
            GetComponent<AudioSource>().Play();
            chattext.text += textlist[11][i];
            yield return null;
        }
        while(!Input.GetMouseButtonDown(0)){//클릭을 기다림
            next.SetActive(true);
            yield return null;
        }

        chat.SetActive(false);
        next.SetActive(false);

        chattext.text = null;
        NoiseKnown.GetComponent<AudioSource>().Stop();
        //chat.GetComponent<AudioSource>().playOnAwake = false;  
        //StartCoroutine(Chatbox(10));//구럭
        yield return new WaitForSeconds(0.5f);


        GetComponent<AudioSource>().pitch = 1.2f;
        chattext.text = nickname+": ";
        chat.SetActive(true);        
        // 뭐야 이 마법봉은! 
        for(int i =0; i< textlist[12].Length; i++){
            yield return new WaitForSeconds(0.1f);
            GetComponent<AudioSource>().Play();
            chattext.text += textlist[12][i];
            yield return null;
        }
        while(!Input.GetMouseButtonDown(0)){//클릭을 기다림
            next.SetActive(true);
            yield return null;
        }

        chat.SetActive(false);
        next.SetActive(false);

        chattext.text = null;
        //StartCoroutine(Chatbox(11));//얌마
        yield return new WaitForSeconds(1f);
        FindAnyObjectByType<NextScene>().ReStart();

    }             
}
