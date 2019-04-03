using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LongClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{	public Button button;
    public int attackDamage = 1;               // The amount of health taken away per attack.
    float timer;
     public float timeBetweenAttacks = 1.5f;     // The time in seconds between each attack.
    AudioSource playerAudio;                                    // Reference to the AudioSource component.

    public Rigidbody rb;
    GameObject Enemy;  
    public float fwdforce = 25f;
    public float swdforce = 25f;
    public Quaternion originalRotationValue;
    public float rotation_speed = 80f;
 private bool walkUp;
    private bool walkLeft;
    private bool walkRight;
    private bool walkDown;
    Animator anim;
    Vector3 ip;
	private bool pointerDown;
	private float pointerDownTimer;

	[SerializeField]
	private float requiredHoldTime;

	public UnityEvent onLongClick;

	[SerializeField]
	private Image fillImage;
void Start () {
		 anim = GetComponent<Animator>();
        Enemy = GameObject.FindGameObjectWithTag ("Enemy");
        ip = transform.position;
        originalRotationValue = transform.rotation; // sa
	}
	public void OnPointerDown(PointerEventData eventData)
	{
		pointerDown = true;

		Debug.Log("OnPointerDown");
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		Reset();
		Debug.Log("OnPointerUp");
	}

	private void Update()
	{
		if (pointerDown)
		{
			pointerDownTimer += Time.deltaTime;
			if (pointerDownTimer >= requiredHoldTime)
			{
				if (onLongClick != null)
					onLongClick.Invoke();

				Reset();
			}
			fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
		}
	}

	private void Reset()
	{
		pointerDown = false;
		pointerDownTimer = 0;
		fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
	}

}