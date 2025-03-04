using UnityEngine;

public class Bear : MonoBehaviour
{
    public ParticleSystem particle;

    [SerializeField]
    private AudioSource Kill;
    
    public void GetDestroy(){
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
        Kill.Play();
        particle.Play();


    }
}
