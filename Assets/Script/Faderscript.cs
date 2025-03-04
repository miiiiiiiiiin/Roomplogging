using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Faderscript : MonoBehaviour
{
    public float FadeSpeed = 5f;

    [SerializeField]
    private Image Fade;
    [SerializeField]
    private Image WhiteFade;
    public GameObject OldLight;
    public GameObject NewLight;
    //public GameObject CleanLight;
    public Animator animator;
    public GameObject Bear;
    public GameObject BadCleanRoom;
    public GameObject GoodCleanRoom;

    public GameObject DirtyRoom;

    public void StartFade(string name){
        StartCoroutine(FadeOut(name));
    }
    IEnumerator FadeOut(string name){
         
        Debug.Log("됏냐고");
        //yield return new WaitForSeconds(0.5f); 
        float alpha = 0f;
        Fade.raycastTarget = true;
            while(alpha < 1){
                GetComponent<AudioSource>().volume -= FadeSpeed * Time.deltaTime;
                alpha += FadeSpeed * Time.deltaTime;
                Fade.color = new Color(0, 0, 0, alpha);
                yield return new WaitForSeconds(0.01f);
            }
        SceneManager.LoadScene(name);
    }

    IEnumerator FadeOut2(string name){
         
        Debug.Log("됏냐고");
        //yield return new WaitForSeconds(0.5f); 
        float alpha = 0f;
        Fade.raycastTarget = true;
            while(alpha < 1){
                NewLight.GetComponent<AudioSource>().volume -= FadeSpeed * Time.deltaTime;
                alpha += FadeSpeed * Time.deltaTime;
                Fade.color = new Color(0, 0, 0, alpha);
                yield return new WaitForSeconds(0.01f);
            }
        SceneManager.LoadScene(name);
    }

    public void goHome(string name){
        StartCoroutine(FadeOut2(name));
    }

    public void Whitefade(){
        StartCoroutine(FadeInOut());
    }
    IEnumerator FadeInOut(){
        GetComponent<AudioSource>().Play();
       // Debug.Log("됏냐고");
        //yield return new WaitForSeconds(0.5f); 
        float alpha = 0f;
        //WhiteFade.raycastTarget = true;
            while(alpha < 1){
                
                alpha += FadeSpeed * Time.deltaTime;
                WhiteFade.color = new Color(0.7f, 0.7f, 0.7f, alpha);
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(1.5f);
            OldLight.SetActive(false);
            NewLight.SetActive(true);
            
            animator.SetTrigger("isChange");
            NewLight.GetComponent<AudioSource>().Play();
            while(alpha > 0){
                alpha -= FadeSpeed * Time.deltaTime;
                WhiteFade.color = new Color(0.7f, 0.7f, 0.7f, alpha);
                yield return new WaitForSeconds(0.01f);
            }
            FindAnyObjectByType<NewMonoBehaviourScript>().DisSlime();
                yield return new WaitForSeconds(1f);

       
    }

    

    public void NormalFadeStart(){
        StartCoroutine(NormalFade());
    }

    IEnumerator NormalFade(){
        yield return new WaitForSeconds(0);
        float alpha = 0f;
        while(alpha < 1){
                
                alpha += FadeSpeed * Time.deltaTime;
                WhiteFade.color = new Color(0, 0, 0, alpha);
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(1.5f);
            DirtyRoom.SetActive(false);
            BadCleanRoom.SetActive(true);
            OldLight.SetActive(false);//불끔끔
            NewLight.SetActive(true);//해들어오는 불빛빛
            animator.SetTrigger("isBadClean");// 플레이어 위치 변경경
            Bear.SetActive(false);//곰없앰앰
            NewLight.GetComponent<AudioSource>().Play();//오디오 소스 시작작
            while(alpha > 0){
                alpha -= FadeSpeed * Time.deltaTime;
                WhiteFade.color = new Color(0,0,0, alpha);
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(2f);

    }

    public void GoodFadeStart(){
        StartCoroutine(GoodFadeEnding());
    }

    IEnumerator GoodFadeEnding(){
        yield return new WaitForSeconds(0);
        float alpha = 0f;
        while(alpha < 1){
                
                alpha += FadeSpeed * Time.deltaTime;
                WhiteFade.color = new Color(0, 0, 0, alpha);
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(1.5f);
            DirtyRoom.SetActive(false);
            GoodCleanRoom.SetActive(true);
            OldLight.SetActive(false);//불끔끔
            NewLight.SetActive(true);//해들어오는 불빛빛
            animator.SetTrigger("isHeart");// 플레이어 위치 변경경
            Bear.SetActive(false);//곰없앰앰
            GoodCleanRoom.GetComponent<AudioSource>().Play();//오디오 소스 시작작
            NewLight.GetComponent<AudioSource>().Play();//오디오 소스 시작작

            while(alpha > 0){
                alpha -= FadeSpeed * Time.deltaTime;
                WhiteFade.color = new Color(0,0,0, alpha);
                yield return new WaitForSeconds(0.01f);
            }

            yield return new WaitForSeconds(2f);


    }

    
   
}
