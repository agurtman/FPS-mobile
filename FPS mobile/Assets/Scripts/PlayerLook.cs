using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    float mouseSense = 0.5f;

    void Update()
    {
        foreach (var touch in Input.touches)
        {
            if (touch.position.x >= Screen.width / 2)
            {
                float rotateX = touch.deltaPosition.x * mouseSense;
                float rotateY = touch.deltaPosition.y * mouseSense;

                Vector3 rotPlayer = transform.rotation.eulerAngles;

                rotPlayer.x -= rotateY;
                rotPlayer.z = 0;
                rotPlayer.y += rotateX;

                transform.rotation = Quaternion.Euler(rotPlayer);
            }
        }
    }
}