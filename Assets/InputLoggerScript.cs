using UnityEngine;

public class InputLoggerScript : MonoBehaviour
{
    void Start()
    {
        
    }

    private string[] joystickAxes = { "Horizontal1", "Vertical1", "Shoot1", "Horizontal2", "Vertical2", "Shoot2" };

    private string[] detectedInputs = new string[100];

    private int next = 0;
    
    private void log(string message)
    {
        for (int i = 0; i < next; i++)
        {
            if (message == detectedInputs[i])
            {
                return;
            }
        }
        detectedInputs[next] = message;
        Debug.Log(message);
        next += 1;

    }
    
    void Update()
    {

        return;
        // Log pressed keys
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(key))
            {
                this.log("Key pressed: " + key);
            }
        }

        // Log mouse buttons
        for (int i = 0; i < 3; i++)
        {
            if (Input.GetMouseButton(i))
            {
                this.log("Mouse button pressed: " + i);
            }
        }
        
        // Log joystick axes
        foreach (string axis in joystickAxes)
        {
            float axisValue = Input.GetAxis(axis);
            if (Mathf.Abs(axisValue) > 0.1f) // Only log if the axis value is significant
            {
                this.log("Joystick axis " + axis );
            }
        }

        // Log joystick buttons
        for (int i = 0; i < 20; i++) // Assuming a maximum of 20 joystick buttons
        {
            if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), "JoystickButton" + i)))
            {
                this.log("Joystick button pressed: " + i);
            }
        }

        // Log pressed keys
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(key))
            {
                this.log("Key pressed: " + key);
            }
        }

        // Log mouse buttons
        for (int i = 0; i < 3; i++)
        {
            if (Input.GetMouseButton(i))
            {
                this.log("Mouse button pressed: " + i);
            }
        }

    }
}
