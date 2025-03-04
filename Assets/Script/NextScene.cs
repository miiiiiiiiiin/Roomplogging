
using UnityEngine;
using TMPro;

public class NextScene : MonoBehaviour
{

    public TMP_InputField nickname;
    private string getname;

    public void StartGame()
    {
        if(nickname.text != null){
            getname = nickname.text;
            PlayerPrefs.SetString("name", getname);
        }else{
            getname = "홍길동";
            PlayerPrefs.SetString("name", getname);
        }
        

        FindAnyObjectByType<Faderscript>().StartFade("MainScene");
        
        
    }
    public void Endding(){
        FindAnyObjectByType<Faderscript>().StartFade("EndingScene");
    }

    public void ReStart(){
        FindAnyObjectByType<Faderscript>().StartFade("GameScene");
    }

    public void Home(){
        GameManager.instance.HeartCount = 0;
        GameManager.instance.FirstSlimeCount = 10;
        GameManager.instance.LevelCount =1;
        FindAnyObjectByType<Faderscript>().goHome("TitleScene");
    } 


}
