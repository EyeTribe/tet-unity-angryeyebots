using UnityEngine;
using System.Collections;
using TETCSharpClient;
using TETCSharpClient.Data;
using Assets.Scripts;

public class GazeAngryBotsWrap : MonoBehaviour, IGazeListener
{
    private GazeDataValidator gazeUtils;

	void Start () 
    {
        gazeUtils = new GazeDataValidator(15);

        //activate C# TET client
        GazeManager.Instance.Activate
            (
                GazeManager.ApiVersion.VERSION_1_0,
                GazeManager.ClientMode.Push
            );

        //register for gaze updates
        GazeManager.Instance.AddGazeListener(this);
	}

    public void OnGazeUpdate(GazeData gazeData) 
    {
        //Add frame to GazeData cache handler
        gazeUtils.Update(gazeData);
    }

    public void OnCalibrationStateChanged(bool isCalibrated)
    {
    }

    public void OnScreenIndexChanged(int screenIndex) 
    {
    }

    void Update()
    {
        //handle keypress
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void OnGUI()
    {
        int padding = 10;
        int btnWidth = 100;
        int btnHeight = 30;
        int y = padding;

        if (GUI.Button(new Rect(padding, y, btnWidth, btnHeight), "Press to Exit"))
        {
            Application.Quit();
        }

        if (!GazeManager.Instance.IsConnected)
        {
            y += btnHeight + padding;
            GUI.TextArea(new Rect(padding, y, 170, 20), "EyeTribe Server not running!");
        }
        else
        if (!GazeManager.Instance.IsCalibrated)
        {
            y += btnHeight + padding;
            GUI.TextArea(new Rect(padding, y, 190, 20), "EyeTribe Server not calibrated!");
        }
    }

    void OnApplicationQuit()
    {
        GazeManager.Instance.RemoveGazeListener(this);
        GazeManager.Instance.Deactivate();
    }

    public Vector3 GetGazeScreenPosition() 
    {
        Point2D gp = gazeUtils.GetLastValidSmoothedGazeCoordinates();

        if (null != gp)
        {
            Point2D sp = UnityGazeUtils.getGazeCoordsToUnityWindowCoords(gp);
            return new Vector3((float)sp.X, (float)sp.Y, 0f);
        }
        else
            return Vector3.zero;

    }
}
