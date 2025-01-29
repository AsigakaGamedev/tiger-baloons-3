using System.Collections;
using System.Collections.Generic;
using Blobcreate.ProjectileToolkit;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Joystick aimJoystick;
    [SerializeField] private Transform playerBody;
    [SerializeField] private float rotateSpeed = 3;

    [Space]
    [SerializeField] private float attackDelay = 3;
    [SerializeField] private float attackForce = 25;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private TrajectoryPredictor trajectoryPredictor;

    [Space]
    [SerializeField] private Button attack1Btn;
    [SerializeField] private Bullet defaultBulletPrefab;
    [SerializeField] private int bullets1 = 1;
    [SerializeField] private TextMeshProUGUI bullets1Text;

    [Space]
    [SerializeField] private Button attack2Btn;
    [SerializeField] private Bullet bullet2Prefab;
    [SerializeField] private int bullets2 = 1;
    [SerializeField] private TextMeshProUGUI bullets2Text;

    [Space]
    [SerializeField] private Button attack3Btn;
    [SerializeField] private Bullet bullet3Prefab;
    [SerializeField] private int bullets3 = 0;
    [SerializeField] private TextMeshProUGUI bullets3Text;

    [Space]
    [SerializeField] private Bullet currentBullet;

    private bool canAttack;
    private bool bulletShooted;

    [Inject] private UIEkran uiEkran;

    private void Start()
    {
        bullets1Text.text = bullets1.ToString();
        bullets2Text.text = bullets2.ToString();
        bullets3Text.text = bullets3.ToString();

        canAttack = true;
        bulletShooted = false;

        attack1Btn.onClick.AddListener(() =>
        {
            Shot(0);
        });

        attack2Btn.onClick.AddListener(() =>
        {
            Shot(1);
        });

        attack3Btn.onClick.AddListener(() =>
        {
            Shot(2);
        });
    }

    [SerializeField] private LayerMask aimLayers;

    private void Update()
    {
        if (bulletShooted)
        {
            if (!currentBullet)
            {
                bulletShooted = false;
                virtualCamera.Follow = playerBody;
                Time.timeScale = 1;
                uiEkran.ChangeScreen("hud");
                //trajectoryPredictor.gameObject.SetActive(true);
            }
        }
        else
        {
            if (Physics.Raycast(attackPoint.position, attackPoint.forward, out RaycastHit hit))
            {
                float dst = Vector3.Distance(attackPoint.position, hit.point);
                //trajectoryPredictor.Render(attackPoint.position, attackPoint.forward * attackForce, dst);
            }
            playerBody.Rotate(-aimJoystick.Vertical * Time.deltaTime * rotateSpeed, aimJoystick.Horizontal * Time.deltaTime * rotateSpeed, 0, Space.Self);
        }
    }

    private void ResetCanAttack()
    {
        canAttack = true;
    }

    public void Shot(int type)
    {
        if (!canAttack) return;

        Bullet bullet = null;
        if (type == 0)
        {
            if (bullets1 <= 0) return;

            bullet = defaultBulletPrefab;
            bullets1--;
            bullets1Text.text = bullets1.ToString();
        }
        else if (type == 1)
        {
            if (bullets2 <= 0) return;

            bullet = bullet2Prefab;
            bullets2--;
            bullets2Text.text = bullets2.ToString();
        }
        else if (type == 2)
        {
            if (bullets3 <= 0) return;

            bullet = bullet3Prefab;
            bullets3--;
            bullets3Text.text = bullets3.ToString();
        }

        if (bullets3 <= 0 && bullets2 <= 0 && bullets1 <= 0)
        {
            GameManager.Instance.SetEmpty();
        }

            currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.LookRotation(attackPoint.forward));
        currentBullet.SetType(type);

        if (currentBullet.TryGetComponent(out Rigidbody rb))
        {
            rb.AddForce(attackPoint.forward * attackForce, ForceMode.Impulse);
        }

        canAttack = false;
        bulletShooted = true;
        virtualCamera.Follow = currentBullet.transform;
        uiEkran.ChangeScreen("aim");
        Invoke(nameof(ResetCanAttack), attackDelay);
    }
}
