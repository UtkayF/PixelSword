using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{

    public Animator animator;
    public Rigidbody2D rg2d;
    public SpriteRenderer spriteRenderer;
    public bool isAttacking = false;

    [SerializeField]
    GameObject attackHitBoxFIRST;

    public GameObject spellHere;
    public GameObject fireBallOBJ;
    public GameObject fireBall_GO;

    public float speed;
    public bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public float jumpForce;
    public LayerMask whatIsGround;

    private Vector2 startPos;
    public int pixelDistToDetect;
    private bool fingerDown;

    public BoxCollider2D player;
    public GameObject isBlockLayer;
    public bool isBlock = false;

    private int forRandomAttack;

    public GameObject fakeGroundOBJ;
    public bool fakeGround = true;
    public bool justOneUpAttack = true;
    public bool redToWhite = false;

    public ACH playerACH;

    [Header("Controller Number of layer")]
    public int controllerNumber;
    public Button fireball_Swipe;
    public Button fireball_Buttons;

    [Header("Controller Buttons")]
    public Button jumpBTN;
    public Button attackBTN;
    public Button blockBTN;
    public Button downBTN;


    private void Start()
    {
        controllerNumber = PlayerPrefs.GetInt("controller", 1);

        if (controllerNumber == 1)
        {
            jumpBTN.gameObject.SetActive(false);
            attackBTN.gameObject.SetActive(false);
            blockBTN.gameObject.SetActive(false);
            downBTN.gameObject.SetActive(false);
            fireball_Swipe.gameObject.SetActive(true);
            fireball_Buttons.gameObject.SetActive(false);
        }
        else
        {
            jumpBTN.gameObject.SetActive(true);
            attackBTN.gameObject.SetActive(true);
            blockBTN.gameObject.SetActive(true);
            downBTN.gameObject.SetActive(true);

            fireball_Swipe.gameObject.SetActive(false);
            fireball_Buttons.gameObject.SetActive(true);

            jumpBTN.onClick.AddListener(jumpMethod);
            attackBTN.onClick.AddListener(attackMethod);
            blockBTN.onClick.AddListener(blockMethod);
            downBTN.onClick.AddListener(downMethod);
            fireball_Buttons.onClick.AddListener(playerACH.m_Fireball);

        }

        animator = GetComponent<Animator>();
        rg2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        attackHitBoxFIRST.SetActive(true);
    }

    private void Update()
    {
        controllerNumber = PlayerPrefs.GetInt("controller", 1);

        if (redToWhite == true) { 
            StartCoroutine(redToWhiteMethod());
        }

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        //FOR MOBILE
        //SWIPE
        if (controllerNumber == 1)
        {

            if (fingerDown == false && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                startPos = Input.touches[0].position;
                fingerDown = true;
            }
            if (fingerDown)
            {

                if (Input.touches[0].position.y >= startPos.y + pixelDistToDetect)
                {
                    fingerDown = false;
                    print("SwipeUp");
                    if (isGrounded)
                    {
                        player.size = new Vector2(1.266667f, 1.933333f);
                        player.offset = new Vector2(0f, 0.9666666f);
                        isBlockLayer.SetActive(false);
                        rg2d.velocity = new Vector2(0, 1f) * jumpForce;
                        animator.Play("Player_jump");
                        isBlock = false;
                    }
                } //UP
                else if (Input.touches[0].position.x <= startPos.x - pixelDistToDetect)
                {
                    fingerDown = false;
                    print("Swipe LEFT");
                    if (isGrounded && !isAttacking)
                    {
                        animator.Play("Player_block");
                        isBlock = true;
                        if (isBlock)
                        {
                            player.size = new Vector2(0.04f, 1.933333f);
                            player.offset = new Vector2(0f, 0.9666666f);
                            isBlockLayer.SetActive(true);
                        }
                    }

                } //LEFT OK BUTTON
                else if (Input.touches[0].position.x >= startPos.x + pixelDistToDetect)
                {
                    fingerDown = false;
                    print("RIGHT");
                    if (!isAttacking && isGrounded)
                    {
                        isAttacking = true;
                        isBlock = false;
                        isBlockLayer.SetActive(false);
                        player.size = new Vector2(1.266667f, 1.933333f);
                        player.offset = new Vector2(0f, 0.9666666f);
                        //ATTACK ANIMATION
                        forRandomAttack = Random.Range(1, 3);

                        if (forRandomAttack == 1)
                            animator.Play("Player_attack");
                        else if (forRandomAttack == 2)
                            animator.Play("Player_attack2");
                        else
                            animator.Play("Player_attack2");

                        StartCoroutine(DoAttack());
                    }

                } //RIGHT OK BUTTON
                else if (Input.touches[0].position.y <= startPos.y - pixelDistToDetect)
                {
                    fingerDown = false;
                    print("DOWN");

                    if (isGrounded == true)
                    {
                        isBlock = false;
                        isBlockLayer.SetActive(false);
                        animator.Play("Player_crouch");
                        player.offset = new Vector2(0f, 0.7f);
                        player.size = new Vector2(1.266667f, 1.3f);
                    }

                    if (isGrounded == false && fakeGround == false && justOneUpAttack == true)
                    {
                        isAttacking = true;
                        isBlock = false;
                        isBlockLayer.SetActive(false);
                        int forUpperAttackRandom = Random.Range(1, 3);
                        if (forUpperAttackRandom == 1)
                            animator.Play("Player_attack3");
                        else if (forUpperAttackRandom == 2)
                            animator.Play("Player_attack4");
                        else
                            animator.Play("Player_attack4");

                        StartCoroutine(UpperAttack());
                    }



                } //DOWN OK BUTTON


            }
            if (fingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
            {
                fingerDown = false;
            }

        }

    }


    void attackMethod()
    {
        print("RIGHT");
        if (!isAttacking && isGrounded)
        {
            isAttacking = true;
            isBlock = false;
            isBlockLayer.SetActive(false);
            player.size = new Vector2(1.266667f, 1.933333f);
            player.offset = new Vector2(0f, 0.9666666f);
            //ATTACK ANIMATION
            forRandomAttack = Random.Range(1, 3);

            if (forRandomAttack == 1)
                animator.Play("Player_attack");
            else if (forRandomAttack == 2)
                animator.Play("Player_attack2");
            else
                animator.Play("Player_attack2");

            StartCoroutine(DoAttack());
        }

        if (isGrounded == false && fakeGround == false && justOneUpAttack == true)
        {
            isAttacking = true;
            isBlock = false;
            isBlockLayer.SetActive(false);
            int forUpperAttackRandom = Random.Range(1, 3);
            if (forUpperAttackRandom == 1)
                animator.Play("Player_attack3");
            else if (forUpperAttackRandom == 2)
                animator.Play("Player_attack4");
            else
                animator.Play("Player_attack4");

            StartCoroutine(UpperAttack());
        }

    }

    void downMethod()
    {
        print("DOWN");

        if (isGrounded == true)
        {
            isBlock = false;
            isBlockLayer.SetActive(false);
            animator.Play("Player_crouch");
            player.offset = new Vector2(0f, 0.7f);
            player.size = new Vector2(1.266667f, 1.3f);
        }

    }

    void blockMethod()
    {
        print("Swipe LEFT");
        if (isGrounded && !isAttacking)
        {
            animator.Play("Player_block");
            isBlock = true;
            if (isBlock)
            {
                player.size = new Vector2(0.04f, 1.933333f);
                player.offset = new Vector2(0f, 0.9666666f);
                isBlockLayer.SetActive(true);
            }
        }
    }

    void jumpMethod()
    {
        print("SwipeUp");
        if (isGrounded)
        {
            player.size = new Vector2(1.266667f, 1.933333f);
            player.offset = new Vector2(0f, 0.9666666f);
            isBlockLayer.SetActive(false);
            rg2d.velocity = new Vector2(0, 1f) * jumpForce;
            animator.Play("Player_jump");
            isBlock = false;
        }
    }



    IEnumerator redToWhiteMethod()
    {
        yield return new WaitForSeconds(1f);
        this.GetComponent<SpriteRenderer>().color = Color.white;
        redToWhite = false;
        
    }

    IEnumerator DoAttack()
    {


       yield return new WaitForSeconds(.3f);
       attackHitBoxFIRST.SetActive(false);
       yield return new WaitForSeconds(.3f);
       attackHitBoxFIRST.SetActive(true);

        if (forRandomAttack == 1){
            yield return new WaitForSeconds(.3f);
            attackHitBoxFIRST.SetActive(false);
            yield return new WaitForSeconds(.3f);
            attackHitBoxFIRST.SetActive(true);
           
        }
        else
        {
            
        }

        isBlock = false;
        isAttacking = false;

    }

    IEnumerator UpperAttack()
    {
        justOneUpAttack = false;
        yield return new WaitForSeconds(.3f);
        attackHitBoxFIRST.SetActive(false);
        yield return new WaitForSeconds(.3f);
        attackHitBoxFIRST.SetActive(true);
        yield return new WaitForSeconds(.1f);

        justOneUpAttack = true;
        isBlock = false;
        isAttacking = false;
    }

}
