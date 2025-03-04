using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using DG.Tweening;
using System.Linq;



public class EndingScript : MonoBehaviour
{

    public List<string> textlist;
    public List<string> badtextlist;
    public List<string> goodtextlist;

    
    [SerializeField]
    private GameObject chat;
    
    [SerializeField]
    private GameObject next;
    public TMP_Text chattext;
    public TMP_Text chatname;
    private string nickname;
    public GameObject Player;
    public GameObject Bear; // 곰 터지는 소리 
    public AudioSource BearMagic;
    private Vector2 beartransform;
    [SerializeField]
    private AudioSource StandUp;
    private int num = 3;
    public GameObject SpacebarBTN;

    private  float FadeSpeed = 1f;
    public Image Fade;

    public GameObject GoodEndingImage;
    public GameObject BadEndingImage;

    public GameObject Heart;
    public List<GameObject> lifeList = new();

    public AudioSource EndingSong;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        beartransform = new Vector2(-0.7f,0.4f);
         GoodEndingImage.SetActive(false);
         BadEndingImage.SetActive(false);
        StartCoroutine(FadeOut());
        nickname = PlayerPrefs.GetString("name");
        chattext.text = nickname + ": ";
        
    }


    IEnumerator FadeOut(){
        float alpha = 1.0f;
        while(alpha > 0){
            alpha -= FadeSpeed * Time.deltaTime;
            Fade.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSeconds(0.01f);
        }
            StartCoroutine(StartEnding());
           
        }
    IEnumerator StartEnding(){

        yield return new WaitForSeconds(0.5f);
        GetComponent<AudioSource>().pitch = 1.2f;
        chattext.text = nickname + ": ";
        chat.SetActive(true);        
        // 하아하아...
        for(int i =0; i< textlist[0].Length; i++){
            yield return new WaitForSeconds(0.1f);
            GetComponent<AudioSource>().Play();
            chattext.text += textlist[0][i];
            yield return null;
        }
        while(!Input.GetMouseButtonDown(0)){//클릭을 기다림
            next.SetActive(true);
            yield return null;
        }

        chat.SetActive(false);
        next.SetActive(false);

        chattext.text = null;
        yield return new WaitForSeconds(1f);

        chattext.text = nickname + ": ";

        chat.SetActive(true);        
        //이제 끝난건가가
        for(int i =0; i< textlist[1].Length; i++){
            yield return new WaitForSeconds(0.1f);
            GetComponent<AudioSource>().Play();
            chattext.text += textlist[1][i];
            yield return null;
        }
        while(!Input.GetMouseButtonDown(0)){//클릭을 기다림
            next.SetActive(true);
            yield return null;
        }

        chat.SetActive(false);
        next.SetActive(false);

        chattext.text = null;
        yield return new WaitForSeconds(1f);

        StandUp.Play();//곰 일어나는 소리
        Bear.GetComponent<Animator>().SetTrigger("StandUp");//곰 애니메이션 일어남 settrigger StandUp
        yield return new WaitForSeconds(1.5f);
chattext.text = nickname + ": ";
        chat.SetActive(true);        
        //플레이어 대사 !
        for(int i =0; i< textlist[2].Length; i++){
            yield return new WaitForSeconds(0.1f);
            GetComponent<AudioSource>().Play();
            chattext.text += textlist[2][i];
            yield return null;
        }
        while(!Input.GetMouseButtonDown(0)){//클릭을 기다림
            next.SetActive(true);
            yield return null;
        }

        chat.SetActive(false);
        next.SetActive(false);

        chattext.text = null;
        yield return new WaitForSeconds(0.3f);

        Player.GetComponent<Animator>().SetTrigger("isStanding");//플레이어 뒤돌아봄
        StartCoroutine(ChatRound());
        // 여기서부터는 새로운 코루틴틴
        //플레이어 대사 : ... 거짓말.
        //플레이어 대사: 너도 내가 처리해야 되는거... 아니지?
        //안돼... 그럴리가 없어.
        //넌 나와 정말 셀수도 없이 오랜 시간을 보낸.. 친구잖아.

        //곰도리가 손을 올린다. 애니메이션 Attack
        
        

        


    }
    IEnumerator ChatRound(){
        if(num == 6){
            Bear.GetComponent<Animator>().SetTrigger("WalkBear");
            yield return new WaitForSeconds(3f);
        }
        
        yield return new WaitForSeconds(1f);
        chattext.text = nickname + ": ";
        chat.SetActive(true);        
        for(int i =0; i< textlist[num].Length; i++){
            yield return new WaitForSeconds(0.1f);
            GetComponent<AudioSource>().Play();
            chattext.text += textlist[num][i];
            yield return null;
        }
        while(!Input.GetMouseButtonDown(0)){//클릭을 기다림
            next.SetActive(true);
            yield return null;
        }

        chat.SetActive(false);
        next.SetActive(false);
        num ++;
        chattext.text = null;
        yield return new WaitForSeconds(1f);
        if(num <= 6){
            StartCoroutine(ChatRound());
        }else{
            Bear.GetComponent<Animator>().SetTrigger("Attack");
            yield return new WaitForSeconds(1.5f);

            if(GameManager.instance.HeartCount >= 5){
                StartCoroutine(GoodEnding());
                
                // 엔딩 분기점 굿엔딩딩
            }else{
                StartCoroutine(BadEnding());
                //찝찝엔딩딩
            }
        }
        
    }

    // IEnumerator GoodEnding(){
    //     yield return new WaitForSeconds(0);


        

    // }

    IEnumerator GoodEnding(){
        Heart.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        //곰도리가 흔들림
        Bear.GetComponent<Animator>().SetTrigger("HeartAttack");
        yield return new WaitForSeconds(1f);

        chattext.text = nickname + ": ";

            chat.SetActive(true); //!    
            
            for(int i =0; i< goodtextlist[0].Length; i++){
                yield return new WaitForSeconds(0.1f);
                GetComponent<AudioSource>().Play();
                chattext.text += goodtextlist[0][i];
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



                // 💖 새로운 하트를 생성하여 리스트에 추가
        for (int i = 0; i < GameManager.instance.HeartCount; i++)
        {
            GameObject newHeart = Instantiate(Heart, Heart.transform.parent); // 부모 유지
            newHeart.SetActive(true); // 애니메이션 실행 전에 활성화
            lifeList.Add(newHeart);
        }
        BearMagic.Play();    
        // ❤️ 활성화된 하트만 애니메이션 실행
        foreach (var heart in lifeList.Where(obj => obj.activeSelf))
        {
            heart.transform.DOMove(beartransform, 1f).SetEase(Ease.InOutQuad);
            heart.transform.DOScale(Vector3.zero, 1f).SetEase(Ease.InBack)
                .OnComplete(() => heart.SetActive(false)); // 애니메이션 종료 후 비활성화

            yield return new WaitForSeconds(0.1f); // 다음 하트 애니메이션 간격
        }

        // ❌ Heart.SetActive(false); 제거
       // yield return new WaitForSeconds(5f);
        yield return new WaitForSeconds(5f);
        Bear.GetComponent<Animator>().SetTrigger("getClean");//쌔 곰돌이가 됨
        Heart.SetActive(false);
        yield return new WaitForSeconds(2f);
chattext.text = nickname + ": ";
            chat.SetActive(true); //!    
             //우와....!!! 너...!! 
            for(int i =0; i< goodtextlist[1].Length; i++){
                yield return new WaitForSeconds(0.1f);
                GetComponent<AudioSource>().Play();
                chattext.text += goodtextlist[1][i];
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
            EndingSong.Play();

            chattext.text = nickname + ": ";//지금까지의 노력이 널 위해서였구나... 그리고 나도.
            chat.SetActive(true); //!    
            
            for(int i =0; i< goodtextlist[2].Length; i++){
                yield return new WaitForSeconds(0.1f);
                GetComponent<AudioSource>().Play();
                chattext.text += goodtextlist[2][i];
                yield return null;
            }
            while(!Input.GetMouseButtonDown(0)){//클릭을 기다림
                next.SetActive(true);
                yield return null;
            }

            chat.SetActive(false);
            next.SetActive(false);

            chattext.text = null;
            yield return new WaitForSeconds(1.5f);
            while(EndingSong.volume > 0){
                EndingSong.volume -= FadeSpeed * Time.deltaTime;
                yield return new WaitForSeconds(0.01f);
                
            }

            FindAnyObjectByType<Faderscript>().GoodFadeStart();//페이드~~ 곰도리가 잇는 깨긋한 방에 서잇음음

            //창문 열린것처럼 빛 바꾸기
                    //새가 지저귀는 소리

            yield return new WaitForSeconds(4f);
chattext.text = nickname + ": ";//이야... 그래도 죽을뻔 한 대가가 있네. 뭔가 뿌듯하다. 마음도 개운하고.
            chat.SetActive(true); //!    
            
            for(int i =0; i< goodtextlist[3].Length; i++){
                yield return new WaitForSeconds(0.1f);
                GetComponent<AudioSource>().Play();
                chattext.text += goodtextlist[3][i];
                yield return null;
            }
            while(!Input.GetMouseButtonDown(0)){//클릭을 기다림
                next.SetActive(true);
                yield return null;
            }

            chat.SetActive(false);
            next.SetActive(false);

            chattext.text = null;
            yield return new WaitForSeconds(1.5f);

chattext.text = nickname + ": ";//운동해서 그런가? (하하)
            chat.SetActive(true); //!    
            
            for(int i =0; i< goodtextlist[4].Length; i++){
                yield return new WaitForSeconds(0.1f);
                GetComponent<AudioSource>().Play();
                chattext.text += goodtextlist[4][i];
                yield return null;
            }
            while(!Input.GetMouseButtonDown(0)){//클릭을 기다림
                next.SetActive(true);
                yield return null;
            }

            chat.SetActive(false);
            next.SetActive(false);

            chattext.text = null;
            yield return new WaitForSeconds(1.5f);

            Player.GetComponent<Animator>().SetTrigger("Stare");

            yield return new WaitForSeconds(1f);

chattext.text = nickname + ": ";// 앞으로도 잘 부탁한다.
            chat.SetActive(true); //!    
            
            for(int i =0; i< goodtextlist[5].Length; i++){
                yield return new WaitForSeconds(0.1f);
                GetComponent<AudioSource>().Play();
                chattext.text += goodtextlist[5][i];
                yield return null;
            }
            while(!Input.GetMouseButtonDown(0)){//클릭을 기다림
                next.SetActive(true);
                yield return null;
            }

            chat.SetActive(false);
            next.SetActive(false);

            chattext.text = null;
            yield return new WaitForSeconds(1.5f);

            float alpha = 0;// 엔딩 화면 띄우기기
                while(alpha < 1){
                alpha += FadeSpeed * Time.deltaTime;
                Fade.color = new Color(0, 0, 0, alpha);
                yield return new WaitForSeconds(0.01f);
                }
                GoodEndingImage.SetActive(true);
                while(alpha > 0){
                    alpha -= FadeSpeed * Time.deltaTime;
                    Fade.color = new Color(0, 0, 0, alpha);
                    yield return new WaitForSeconds(0.01f);

                 }

        //그때 모았던 하트가 두근.두근 하고 곰도리로 하트가 들어감
        
       
        
        
       
         
        
        //곰돌이에게 다가가는 애니메이션
        
        // 페이드~~~
        //페이드 씬 바궈서 해피엔딩  

    }

    IEnumerator BadEnding(){
        yield return new WaitForSeconds(0);
        SpacebarBTN.SetActive(true);

        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null; // 한 프레임 대기
        }
        // 스페이스를 눌렀을 떄떄
            SpacebarBTN.SetActive(false);
chattext.text = nickname + ": ";
            chat.SetActive(true); //이럴순없는겨겨       
            // 하아하아...
            for(int i =0; i< badtextlist[0].Length; i++){
                yield return new WaitForSeconds(0.1f);
                GetComponent<AudioSource>().Play();
                chattext.text += badtextlist[0][i];
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

            SpacebarBTN.SetActive(true);

            while (!Input.GetKeyDown(KeyCode.Space))
            {
                yield return null; // 한 프레임 대기
            }
                SpacebarBTN.SetActive(false);

                Player.GetComponent<Animator>().SetTrigger("isBear");//베어를 벰
                Player.GetComponent<AudioSource>().Play();//슈슉슉
                yield return new WaitForSeconds(2f);
                FindAnyObjectByType<Bear>().GetDestroy();//곰 메소드에 곰 터지는 함수를 넣어서 find로 끌어다 쓰자.

                yield return new WaitForSeconds(2f);
chattext.text = nickname + ": ";
                chat.SetActive(true);       
                // "..."
                for(int i =0; i< badtextlist[1].Length; i++){
                    yield return new WaitForSeconds(0.1f);
                    GetComponent<AudioSource>().Play();
                    chattext.text += badtextlist[1][i];
                    yield return null;
                }
                while(!Input.GetMouseButtonDown(0)){//클릭을 기다림
                    next.SetActive(true);
                    yield return null;
                }

                chat.SetActive(false);
                next.SetActive(false);

                chattext.text = null;
                yield return new WaitForSeconds(1.5f);
                

                FindAnyObjectByType<Faderscript>().NormalFadeStart();

                yield return new WaitForSeconds(4f);
chattext.text = nickname + ": ";
                chat.SetActive(true);       
                // "이러려고 목숨 걸엇나 자괴감들어"
                for(int i =0; i< badtextlist[2].Length; i++){
                    yield return new WaitForSeconds(0.1f);
                    GetComponent<AudioSource>().Play();
                    chattext.text += badtextlist[2][i];
                    yield return null;
                }
                while(!Input.GetMouseButtonDown(0)){//클릭을 기다림
                    next.SetActive(true);
                    yield return null;
                }

                chat.SetActive(false);
                next.SetActive(false);

                chattext.text = null;
                yield return new WaitForSeconds(1f);

chattext.text = nickname + ": ";
                chat.SetActive(true);       
                // "..."
                for(int i =0; i< badtextlist[3].Length; i++){
                    yield return new WaitForSeconds(0.1f);
                    GetComponent<AudioSource>().Play();
                    chattext.text += badtextlist[3][i];
                    yield return null;
                }
                while(!Input.GetMouseButtonDown(0)){//클릭을 기다림
                    next.SetActive(true);
                    yield return null;
                }

                chat.SetActive(false);
                next.SetActive(false);

                chattext.text = null;
                yield return new WaitForSeconds(1.5f);

                Player.GetComponent<Animator>().SetTrigger("BadWalk");

                yield return new WaitForSeconds(3.2f);

                Player.GetComponent<Animator>().SetTrigger("GoBed");

                yield return new WaitForSeconds(1f);

                float alpha = 0;// 엔딩 화면 띄우기기
                while(alpha < 1){
                alpha += FadeSpeed * Time.deltaTime;
                Fade.color = new Color(0, 0, 0, alpha);
                yield return new WaitForSeconds(0.01f);
                }
                BadEndingImage.SetActive(true);
                while(alpha > 0){
                    alpha -= FadeSpeed * Time.deltaTime;
                    Fade.color = new Color(0, 0, 0, alpha);
                    yield return new WaitForSeconds(0.01f);

                 }


            

        

            
        
        //스페이스바를 누르세요 버튼
        //스페이스 누르면 ... 대사 뜸뜸
        //다시 스페이스바를 누르세요 뜸뜸
        //플레이어 공격 모션 + 곰 터지는 소리와 함께 곰 destroy
        //플레이어 그거 없이 "..."

        //페이드~~~~~~
        
        //창문 열린것처럼 바꾸기
        //새가 지저귀는 소리리
        //누군지는 모르겠지만 방 치워준건 고마운데... 내 애착인형까지 없애야 했냐?
        //침대에 들어감
        // 페이더 씬 바꿔서 배드엔딩
    }
}

