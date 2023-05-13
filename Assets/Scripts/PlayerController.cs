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
    public GroundSpawner groundSpawner;
    public static bool isDead = true;


    Vector3 yon = Vector3.left;
    public float hızlanmaZorlugu;
    float score = 0f;
    float artisMiktari = 1f;
    int bestScore = 0;




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
                PlayerPrefs.SetInt("BestScore", bestScore);
            }
            restartPanel.SetActive(true);
            Destroy(this.gameObject, 3f);
        }
    }

    private void FixedUpdate()
    {
        if (isDead)
            return;
        Vector3 hareket = yon * speed * Time.deltaTime;//objemizin hareket değeri
        speed += Time.deltaTime * hızlanmaZorlugu;
        transform.position += hareket;//hareket değerini sürekli pozisyonuma ekle

        score += artisMiktari * speed * Time.deltaTime;
        scoreText.text ="Score: "+ ((int) score).ToString();
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

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Coin"))
        {
            Debug.Log("coin yokoldu");
            Destroy(Coin);
        }
    }

    public void PlayGame()
    {
        isDead = false;
        playGamePanel.SetActive(false);
    }
}//class
