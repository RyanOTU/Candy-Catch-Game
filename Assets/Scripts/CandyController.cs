using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using TMPro;

public class CandyController : MonoBehaviour
{
    //Candy Variables
    [SerializeField] private GameObject[] candyList;
    [SerializeField] private List<GameObject> candies;
    [SerializeField] private int candyAmount;
    [SerializeField] private float candyFallSpeed;
    private int currentCandy;
    private Vector3 startingPos = new Vector3(0, 10, 0);
    
    //Timer Variables
    [SerializeField] private int timerInterval = 3000;
    private float timeRemaining;
    
    //IsFalling bool
    private bool isFalling = false;

    //Game Over text
    [SerializeField] private TextMeshProUGUI gameOverBox;

    //New score manager
    //ScoreManager scoreManager = new ScoreManager();

    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (candies != null)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = timerInterval;
                isFalling = true;
                GetRandomStartPosition();
            }
            if (isFalling)
            {
                transform.Translate(new Vector3(0, -candyFallSpeed, 0));
            }
        }
        else
        {

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collided with player! Added " + candies[currentCandy].GetComponent<Candy>().GetPointValue() + " points to score!");
            GetComponent<ScoreManager>().UpdateScore(candies[currentCandy].GetComponent<Candy>().GetPointValue());
            GetRandomStartPosition();
            timeRemaining = timerInterval;
        }
    }
    private GameObject RandomizeCandyType()
    {
        Debug.Log("Chosing candy...");
        int percentage = Random.Range(0, 101);
        Debug.Log("Candy range chosen: " + percentage);
        if (percentage > 0 && percentage <= 50) return candyList[0];
        if (percentage > 50 && percentage <= 75) return candyList[1];
        if (percentage > 75 && percentage <= 90) return candyList[2];
        if (percentage > 90 && percentage <= 95) return candyList[3];
        if (percentage > 95 && percentage <= 99) return candyList[4];
        if (percentage == 100) return candyList[5];
        else return candyList[0];
    }
    private void GetRandomStartPosition()
    {
        if (candies.Count > 0)
        {
            candies.Remove(candies[currentCandy]);
            candies[currentCandy].transform.position = startingPos; //Unity kept throwing an error when I tried to destroy, so instead I just moved it :/
            Instantiate(candies[currentCandy], this.transform);
            transform.position = new Vector3(Random.Range(-8, 9), startingPos.y, 0);
        }
        else if (candies.Count == 0)
        {
            Reset();
            if (GetComponent<ScoreManager>().GetScore() < 15)
            {
                gameOverBox.text = "Game Over! Result: :(";
            }
            if (GetComponent<ScoreManager>().GetScore() > 15 && GetComponent<ScoreManager>().GetScore() <= 35)
            {
                gameOverBox.text = "Game Over! Result: Sugar Rush!";
            }
            if (GetComponent<ScoreManager>().GetScore() > 35 && GetComponent<ScoreManager>().GetScore() <= 50)
            {
                gameOverBox.text = "Game Over! Result: Halloween!";
            }
            if (GetComponent<ScoreManager>().GetScore() > 50)
            {
                gameOverBox.text = "Game Over! Result: Candy Craze!";
            }
        }
    }
        
    private void Reset()
    {
        candies = new List<GameObject>();
        for (int i = 0; i < candyAmount; i++)
        {
            candies.Add(RandomizeCandyType());
        }
        currentCandy = 0;
        timeRemaining = timerInterval;
        transform.position = startingPos;
        Instantiate(candies[currentCandy], this.transform);
    }
}
