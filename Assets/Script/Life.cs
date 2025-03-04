using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using DG.Tweening;
public class Life : MonoBehaviour
{
    // [SerializeField]
    // private Image life1;
    // [SerializeField]
    // private Image life2;
    // [SerializeField]
    // private Image life3;
    [SerializeField]
    private TMP_Text SlimeCount;
    [SerializeField]
    private TMP_Text HeartCount;
    private int index =1;

    public List<GameObject> lifeList = new List<GameObject>();
    //private List<Vector3> lifeTransform = new List<Vector3>();
    private int count = 10;
    private int listIndex = 0;
    public GameObject Player;
    public GameObject NextPanel;
    public GameObject Next2Panel;
    public GameObject Next3Panel;
    public GameObject PocketTransform;

    Dictionary<GameObject, Vector3> heartOriginalPositions = new Dictionary<GameObject, Vector3>();

    void Start()
    {
        
        //index = GameManager.instance.HeartCount;
        foreach (var heart in lifeList)
        {
            heartOriginalPositions[heart] = heart.transform.position; // 원래 위치 저장
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.isOn){
            if(listIndex <3){
                GameManager.instance.isOn = false;
                StartCoroutine(isOn());
            }else{
                GameManager.instance.isOn = false;
            }   
        }

        if(GameManager.instance.FirstSlimeCount != count && GameManager.instance.LevelCount == 1){
            SlimeCount.text = GameManager.instance.FirstSlimeCount.ToString() + "/10";
            count = GameManager.instance.FirstSlimeCount;
        }else if(GameManager.instance.FirstSlimeCount != count && GameManager.instance.LevelCount == 2){
            SlimeCount.text = GameManager.instance.FirstSlimeCount.ToString() + "/15";
            count = GameManager.instance.FirstSlimeCount;
        }else if(GameManager.instance.FirstSlimeCount != count && GameManager.instance.LevelCount == 3){
            SlimeCount.text = GameManager.instance.FirstSlimeCount.ToString() + "/20";
            count = GameManager.instance.FirstSlimeCount;
        }
        // if(GameManager.instance.FirstSlimeCount == 0){
        //     StartCoroutine(NextLevel());
        //    
        // }

    }

    IEnumerator isOn(){

        lifeList[listIndex].SetActive(false);
        GetComponent<AudioSource>().Play();
        
        Player.GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0.4f, 1f);
        yield return new WaitForSeconds(0.1f);
        Player.GetComponent<SpriteRenderer>().color = Color.white;

        listIndex ++;
        int remainingHearts = lifeList.Count(obj => obj.activeSelf);
        if(remainingHearts == 0){
            GameManager.instance.GameOver();
        }
        
    }

    public void StartPanel(){
        StartCoroutine(NextLevel());
        PocketTransform.SetActive(true);
        NextPanel.SetActive(true);
        GameManager.instance.LevelCount = 2;

    }
    public void StartPanelLevel2(){
        StartCoroutine(NextLevel());
        Next2Panel.SetActive(true);
        GameManager.instance.LevelCount = 3;
        PocketTransform.SetActive(true);
    }
    public void StartPanelLevel3(){
        StartCoroutine(NextLevel());
        Next3Panel.SetActive(true);
        //GameManager.instance.LevelCount = 3;
        PocketTransform.SetActive(true);
    }

    IEnumerator NextLevel(){
        yield return new WaitForSeconds(0.5f);
        int remainingHearts = lifeList.Count(obj => obj.activeSelf);
        Debug.Log("남은 하투 개수: "+ remainingHearts);

        foreach (var heart in lifeList.Where(obj => obj.activeSelf))
        {
            HeartCount.text = index.ToString();
            index ++;
            heart.transform.DOMove(PocketTransform.transform.position, 1f).SetEase(Ease.InOutQuad);
            heart.transform.DOScale(Vector3.zero, 0.8f).SetEase(Ease.InBack)
                .OnComplete(() => heart.SetActive(false));
        }
        GameManager.instance.HeartCount = index - 1;
        
    }
    public void ResetHearts()
    {
        foreach (var heart in lifeList)
        {
            if (heartOriginalPositions.ContainsKey(heart))
            {
                heart.transform.position = heartOriginalPositions[heart]; // 원래 위치로 복구
                heart.transform.localScale = new Vector3(0.9f,0.8f);
                listIndex =0;
                heart.SetActive(true); // 다시 보이게 만들기
            }
        }
    }
}
