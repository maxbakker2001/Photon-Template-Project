using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;


public class PlayerController : NetworkBehaviour, IDamageable
{
    [SerializeField] private Image healthBarImage;
    [SerializeField] private GameObject ui;
    
    [SerializeField] GameObject cameraHolder;
    
    [SerializeField] float mouseSen, sprintSpeed, walkSpeed, jumpForce, smoothTime;

    [SerializeField] private Item[] items;

    private int itemIndex;
    private int previousItemIndex = -1;
    float verticalLookRotation;
    bool isGrounded;
    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;
    
    
    Rigidbody rb;
    
    const float maxHealth = 100f;
    float currentHealth = maxHealth;

    PlayerManager playermanager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        if (isLocalPlayer)
        {
            EquipItem(0);
        }
        else
        {
           Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);
            Destroy(ui);
        }
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;
        
       Look();
       Movement();
       Jump();
       SwitchGuns();

       if (transform.position.y <= -10f)
           Die();
    }

    void SwitchGuns()
    {
        for(int i = 0; i < items.Length; i++)
        {
            if(Input.GetKeyDown((i + 1).ToString()))
            {
                EquipItem(i);
                break;
            }
        }

        if(Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            if(itemIndex >= items.Length - 1)
            {
                EquipItem(0);
            }
            else
            {
                EquipItem(itemIndex + 1);
            }
        }
        else if(Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            if(itemIndex <= 0)
            {
                EquipItem(items.Length - 1);
            }
            else
            {
                EquipItem(itemIndex - 1);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            items[itemIndex].Use();
        }
    }

    void Movement()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
        
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }

    void EquipItem(int _index)
    {

        if (_index == previousItemIndex) 
            return;
            
        itemIndex = _index;

        items[itemIndex].ItemGameObject.SetActive(true);

        if (previousItemIndex != -1)
        {
            items[previousItemIndex].ItemGameObject.SetActive(false);;
        }

        previousItemIndex = itemIndex;

        if(isLocalPlayer)
        {
            Hashtable hash = new Hashtable();
            hash.Add("itemIndex", itemIndex);
        }
    }


    void Look()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSen);
        
        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSen;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;

    }

    public void SetGroundedState(bool _grounded)
    {
        isGrounded = _grounded;
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;
        
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }

    [ClientRpc]
    public void TakeDamage(float Damage)
    {
        if (!isLocalPlayer) {
            currentHealth -= Damage;
            healthBarImage.fillAmount = currentHealth / maxHealth;

            if (currentHealth <= 0) {
                Die();
            }
        }
    }


    void RPC_TakeDamage(float Damage)
    {

    }

    void Die()
    {
        playermanager.Die();
    }
}
