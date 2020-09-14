// NULLcode Studio © 2016
// null-code.ru

using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	[Header("Трансформ персонажа:")]
	public Transform player;

	[Header("Настройки высоты:")]
	public float maxHeight = 20;
	public float minHeight = 10;

	[Header("Настройки вращения:")]
	public KeyCode rotateA = KeyCode.Q;
	public KeyCode rotateB = KeyCode.E;
	public float maxRotate = 60;
	public float minRotate = -60;

	[Header("Общий параметр сглаживания:")]
	public float smooth = 2;

	private float height, rotIndex;

	void Awake()
	{
		height = maxHeight;
		transform.position = player.position + new Vector3(0, height, 0);
	}

	void LateUpdate()
	{
		DoTransform(); // следование за игроком
		DoRotation(); // поворот камеры
	}

	void DoRotation()
	{
		if(Input.GetKey(rotateA)) rotIndex += 1;
		else if(Input.GetKey(rotateB)) rotIndex -= 1;

		rotIndex = Mathf.Clamp(rotIndex, minRotate, maxRotate);
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, rotIndex, 0), smooth * Time.deltaTime);
	}

	void DoTransform()
	{
		if(!player) return;

		if(Input.GetAxis("Mouse ScrollWheel") > 0) height -= 1;
		else if(Input.GetAxis("Mouse ScrollWheel") < 0) height += 1;

		height = Mathf.Clamp(height, minHeight, maxHeight);

		// берем текущее положение курсора
		Vector3 mouse = Input.mousePosition;

		// находим позицию персонажа в пространстве экрана
		Vector3 playerPos = Camera.main.WorldToScreenPoint(player.position);

		// создаем окно, уменьшившую в два раза копию текущего, и закрепляем его за позицией персонажа
		Rect rect = new Rect(playerPos.x - (Screen.width/2)/2, playerPos.y - (Screen.height/2)/2, Screen.width/2, Screen.height/2);

		// удерживание позиции курсора в рамках окна
		mouse = Vector2.Max(mouse, rect.min);
		mouse = Vector2.Min(mouse, rect.max);

		// переносим позицию курсора в мировые координаты
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, transform.position.y));

		// находим центр, между персонажем и измененной позиций курсора
		Vector3 camLook = (player.position + mousePos)/2;

		// финальная позиция камеры
		transform.position = Vector3.Lerp(transform.position, new Vector3(camLook.x, player.position.y + height, camLook.z), smooth * Time.deltaTime);
	}
}
