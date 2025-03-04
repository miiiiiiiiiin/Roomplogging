using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour
{
    public Image Fade;
    public float FadeSpeed = 5f;
    void Start()
    {
        StartCoroutine(Fader());
        
    }

    IEnumerator Fader(){
        float alpha = 1.0f;
        while(alpha > 0){
            alpha -= FadeSpeed * Time.deltaTime;
            Fade.color = new Color(0, 0, 0, alpha);
            //Debug.Log(alpha);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
