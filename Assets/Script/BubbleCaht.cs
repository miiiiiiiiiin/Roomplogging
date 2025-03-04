using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BubbleCaht : MonoBehaviour
{
    private float fadeSpeed=1;
    public GameObject ChatBubble;
    public TMP_Text chattext;
    public Image Fadechat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void BubbleChat(){
        StartCoroutine(BubbleChatStart());
        //Fadechat = ChatBubble.GetComponent<Image>();

    }

    IEnumerator BubbleChatStart(){
        
        float alpha = 0;
        while(alpha < 1){
            alpha += fadeSpeed * Time.deltaTime;
            Fadechat.color = new Color(1,1,1,alpha);
            yield return new WaitForSeconds(0.01f);
        }
            yield return new WaitForSeconds(1f);

        while(alpha > 0){
            alpha -= fadeSpeed * Time.deltaTime;
            Fadechat.color = new Color(1,1,1,alpha);
            yield return new WaitForSeconds(0.01f);
        }

    }
    public void BubbleChat2(){
        StartCoroutine(BubbleChatSecond());
    }

    IEnumerator BubbleChatSecond(){
        chattext.text = "!!!";
        ChatBubble.transform.position = new Vector3(-10f, 63f,0);
        float alpha = 0;
        while(alpha < 1){
            alpha += fadeSpeed * Time.deltaTime;
            Fadechat.color = new Color(1,1,1,alpha);
            yield return new WaitForSeconds(0.01f);
        }
            yield return new WaitForSeconds(1f);

        while(alpha > 0){
            alpha -= fadeSpeed * Time.deltaTime;
            Fadechat.color = new Color(1,1,1,alpha);
            yield return new WaitForSeconds(0.01f);
        }

    }
}
