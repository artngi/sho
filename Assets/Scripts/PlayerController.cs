using UnityEngine;
using Terresquall;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public bool isGrounded = true;
    public SpriteRenderer SpriteRenderer;
    public bool isAlive = true;
    public int health = 10;
    public GameObject GameOverScene;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI scoreText;
    bool isInvincibleTime;
    public EnemyController enemyController;
    private AudioSource audioSource;
    public AudioClip Damaged;
    void Start()
    {
        GameOverScene.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        scoreText.text = "";
    }
    void Update()
    {
        healthText.text = $"Health: {health} / 10";
        float x = Input.GetAxis("Horizontal") + VirtualJoystick.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical") + VirtualJoystick.GetAxis("Vertical");
        
        transform.Translate(x * speed * Time.deltaTime, 0, z * speed * Time.deltaTime);
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W)) && isGrounded)
        {
            Debug.Log($"isGrounded: {isGrounded}");
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    public void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else
        {
            Debug.Log(collision.gameObject.tag);
        }
    }
    public void OnCollisionExit2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        else
        {
            Debug.Log(collision.gameObject.tag);
        }
    }

    public void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (!isInvincibleTime)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Destroy(collision.gameObject);
                audioSource.PlayOneShot(Damaged);
                isInvincibleTime = true;
                health--;

                Invoke(nameof(InvincibleTime), 0.1f);
            }

        }
        if (health <= 0)
        {
            Debug.Log("Game Over");
            scoreText.text = "Score: " + enemyController.difficulity;
            healthText.text = "Game Over";
            gameObject.SetActive(false);
            GameOverScene.SetActive(true);
        }
    }
    private void InvincibleTime()
    {
        isInvincibleTime = false;
    }
}
