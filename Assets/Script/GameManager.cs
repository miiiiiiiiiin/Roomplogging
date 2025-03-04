using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int FirstSlimeCount = 10;
    //public int SecondSlimeCount = 10;

    public int PlayerLife = 3;
    public bool isOn = false;

    public int LevelCount =1;
     public static GameManager instance = null;
     public PlayerPrefs Nicname;
     public int HeartCount = 0;

    
    public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindAnyObjectByType<GameManager>();
                    if (instance == null)
                    {
                        Debug.LogError("게임 매니저가 없습니다.");
                    }
                }
                return instance;
            }
        }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);  
        }
    }

    public void ResetSlimeCount(){// 넥스트 레벨 버튼 눌럿을때 실행
        if(LevelCount == 1){
            FirstSlimeCount = 15;
            FindAnyObjectByType<Life>().StartPanel();
        }else if(LevelCount == 2){
            FirstSlimeCount = 20;
            FindAnyObjectByType<Life>().StartPanelLevel2();
        }else if(LevelCount == 3){
            FindAnyObjectByType<Life>().StartPanelLevel3();
            //FindAnyObjectByType<Faderscript>().StartFade("EndingScene");
            // 라이프 개수 세어서 페이드
            //라이프 개수 세어서 HEartCount에 넣음 
        }
        
    }

    public void GameOver(){
        HeartCount = 0;
        FirstSlimeCount = 10;
        LevelCount =1;
        FindAnyObjectByType<Faderscript>().StartFade("GameOver");
    }
   
}
