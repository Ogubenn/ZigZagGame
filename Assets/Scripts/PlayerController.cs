using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Out Component")]
    [SerializeField] float speed;
    [SerializeField] Text scoreText, bestScoreText;
    [SerializeField] GameObject restartPanel, playGamePanel;
    [SerializeField] GameObject Coin;
    [SerializeField] Animator Anim;
    [SerializeField] AudioSource CoinSes;
    [SerializeField] AudioSource BestScoreSes;

    public GroundSpawner groundSpawner;
    public static bool isDead = true;


    Vector3 yon = Vector3.left;
    public float hızlanmaZorlugu;
    int score = 0;
    int bestScore = 0;


    private void Awake() 
    {
    }

    private void Start()
    {
        if (RestartGame.isRestart)
        {
            isDead = false;
            playGamePanel.SetActive(false);
        }
        bestScore = PlayerPrefs.GetInt("BestScore");
        bestScoreText.text = "Best : " + bestScore.ToString();
    }

    private void Update()
    {
        if (isDead)
            return;

        if(Input.GetMouseButtonDown(0))
        {
            if (yon.x == 0)//z ekseninde hareket ediyor.
                yon = Vector3.left;
            else
                yon = Vector3.back;
        }

        if (transform.position.y < 0.4f)
        {
            Debug.Log("Öldü");
            isDead = true;
            if (bestScore < score)
            {
                bestScore = (int)score;
                BestScoreSes.Play();
                PlayerPrefs.SetInt("BestScore", bestScore);
            }
            restartPanel.SetActive(true);
            Destroy(this.gameObject, 3f);
            Anim.SetTrigger("GameOver");

        }
    }

    private void FixedUpdate()
    {
        if (isDead)
            return;
        Vector3 hareket = yon * speed * Time.deltaTime;//objemizin hareket değeri
        speed += Time.deltaTime * hızlanmaZorlugu;
        transform.position += hareket;//hareket değerini sürekli pozisyonuma ekle

        
        //score += artisMiktari * speed * Time.deltaTime;
        //scoreText.text ="Score: "+ ((int) score).ToString();
    }
        

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Zemin"))
        {
            StartCoroutine(Yoket(collision.gameObject));
            groundSpawner.ZeminOluştur();
        }
    }

    IEnumerator Yoket(GameObject zemin)
    {
        yield return new WaitForSeconds(0.2f);
        zemin.AddComponent<Rigidbody>();

        yield return new WaitForSeconds(0.4f);
        Destroy(zemin);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            
            score += 10;
            if(score  > 100)
                speed = speed + 0;
            if(score  > 200)
                speed = speed + 0;
            if(score  > 500)
                speed = speed + 1;
            scoreText.text ="Score: "+ ((int) score).ToString();
            StartCoroutine(Yoket(other.gameObject));
            CoinSes.Play();
        }
    }

    IEnumerator Yoket()
    {
        yield return new WaitForSeconds(0f);
        Destroy(gameObject);
    }


    public void PlayGame()
    {
        isDead = false;
        playGamePanel.SetActive(false);
    }
}//class
