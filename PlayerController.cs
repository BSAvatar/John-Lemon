using UnityEngine;
using UnityEngine.UI; // Add this to access the UI slider

public class PlayerController : MonoBehaviour
{
    public float normalSpeed = 3.0f;
    public float boostSpeed = 6.0f;
    public float boostDuration = 2.0f;
    public float rechargeTime = 5.0f;
    public KeyCode boostKey = KeyCode.LeftShift;
    public GameObject boostIcon;
    public Slider boostCooldownBar; // Add reference to the Slider

    private bool isBoosting = false;
    private bool canBoost = true;
    private float boostTimer = 0f;
    private float rechargeTimer = 0f;

    void Update()
    {
        HandleBoost();

        float speed = isBoosting ? boostSpeed : normalSpeed;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(h, 0, v) * speed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        UpdateCooldownBar();
    }

    void HandleBoost()
    {
        if (Input.GetKey(boostKey) && canBoost)
        {
            isBoosting = true;
            boostTimer += Time.deltaTime;
            if (boostTimer >= boostDuration)
            {
                isBoosting = false;
                canBoost = false;
                boostTimer = 0f;
            }
        }
        else if (isBoosting)
        {
            isBoosting = false;
            canBoost = false;
            boostTimer = 0f;
        }

        if (!canBoost)
        {
            rechargeTimer += Time.deltaTime;
            if (rechargeTimer >= rechargeTime)
            {
                canBoost = true;
                rechargeTimer = 0f;
            }
        }

        if (boostIcon != null)
        {
            boostIcon.SetActive(isBoosting);
        }
    }

    void UpdateCooldownBar()
    {
        if (!canBoost)
        {
            // Calculate the cooldown progress (time left until boost can be used again)
            float progress = rechargeTimer / rechargeTime;
            boostCooldownBar.value = progress; // Update slider value
        }
    }
}
