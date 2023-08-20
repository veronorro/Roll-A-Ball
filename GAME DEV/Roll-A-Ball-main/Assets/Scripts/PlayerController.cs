using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody rb;
    private int pickupCount;
    private Timer timer;
    private bool gameOver;
    GameObject resetPoint;
    bool resetting = false;
    Color originalColour;

    //UI AND HEADERS
    [Header("UI")]
    public GameObject inGamePanel;
    public GameObject gameOverPanel;
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public TMP_Text winTimeText;

    //CONTROLLERS
    GameController gameController;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Get number of pickups in our scene
        pickupCount = GameObject.FindGameObjectsWithTag("PickUp").Length;
        //Run pickups function
        SetcountText();
        //Get the timer object and start the timer
        timer = FindObjectOfType<Timer>();
        timer.StartTimer();
        //Turn on in game panel
        inGamePanel.SetActive(true);
        //Turn off win panel
        gameOverPanel.SetActive(false);
        resetPoint = GameObject.Find("Reset Point");
        originalColour = GetComponent<Renderer>().material.color;
        //GAME CONTROLLER: Finding the GameController in start function
        gameController = FindObjectOfType<GameController>();
    }

    private void Update()
    {
        timerText.text = "Time: " + timer.GetTime().ToString("F2");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (resetting)
            return;
     
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.AddForce(movement * speed);

        if (gameController.controlType == ControlType.WorldTilt)
            return; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PickUp")
        {
            Destroy(other.gameObject);
            //Decrement the pickup count
            pickupCount -= 1;
            //Run the check pickups function
            SetcountText();
        } 
    }

    void SetcountText()
    {
        //Display the amount of pickups left in scene
        scoreText.text = "Score: " + pickupCount;

        if (pickupCount == 0)
        {
            WinGame();
        }
    }


    void WinGame()
    {
        //set game over to true
        gameOver = true;
        //Stop the timer
        timer.StopTimer();
        //Turn on Win panel
        gameOverPanel.SetActive(true);
        //Turn off In game panel
        inGamePanel.SetActive(false);
        //display timer on the win time text
        winTimeText.text = "Your Time Was: " + timer.GetTime().ToString("F2");

        //Set velocity of rigidbody to 0
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    //Code to restart the game once finished
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene
            (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

    }

    //Code to quit the game application when built
    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            StartCoroutine(ResetPlayer());
        }
    }

    public IEnumerator ResetPlayer()
    {
        resetting = true;
        GetComponent<Renderer>().material.color = Color.black;
        rb.velocity = Vector3.zero;
        Vector3 startpos = transform.position;
        float resetSpeed = 2f;
        var i = 0.0f;
        var rate = 1.0f / resetSpeed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(startpos, resetPoint.transform.position, i);
            yield return null;
        }
        GetComponent<Renderer>().material.color = originalColour;
        resetting = false;
    }
}
