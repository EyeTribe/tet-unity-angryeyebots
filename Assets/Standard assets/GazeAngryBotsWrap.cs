/*
 * Copyright (c) 2013-present, The Eye Tribe. 
 * All rights reserved.
 *
 * This source code is licensed under the BSD-style license found in the LICENSE file in the root directory of this source tree. 
 *
 */

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
        gazeUtils = new GazeDataValidator(30);

        //activate C# TET client, default port
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
        int btnHeight = 40;
        int y = padding;

        if (GUI.Button(new Rect(padding, y, btnWidth, btnHeight), "Press to Exit"))
        {
            Application.Quit();
        }

        if (!GazeManager.Instance.IsActivated)
        {
            y += btnHeight + padding;

            if (GUI.Button(new Rect(padding, y, 200, btnHeight), "Connect To Server"))
            {
                //activate C# TET client, default port
                GazeManager.Instance.Activate
                (
                    GazeManager.ApiVersion.VERSION_1_0,
                    GazeManager.ClientMode.Push
                );
            }
        }
        else
        if (!GazeManager.Instance.IsCalibrated)
        {
            y += btnHeight + padding;
            GUI.TextArea(new Rect(padding, y, 190, 20), "EyeTribe Server not calibrated!");
        }
        else
        {
            y += btnHeight + padding;

            string calibText;
            int rating;
            CalibrationResult result = GazeManager.Instance.LastCalibrationResult;
            CalibrationRatingFunction(result, out rating, out calibText);
            GUI.TextArea(new Rect(padding, y, 220, 20), "Calibration Result: " + calibText);
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

    /// <summary>
    /// Simple rating of a given calibration.
    /// </summary>
    /// <param name="result">Any given CalibrationResult</param>
    /// <param name="rating">A number between 1 - 5 where 5 is the best othervise -1.</param>
    /// <param name="strRating">A string with a rating name othervise ERROR.</param>
    public void CalibrationRatingFunction(CalibrationResult result, out int rating, out string strRating)
    {
        if (result == null)
        {
            rating = -1;
            strRating = "ERROR";
            return;
        }
        if (result.AverageErrorDegree < 0.5)
        {
            rating = 5;
            strRating = "PERFECT";
            return;
        }
        if (result.AverageErrorDegree < 0.7)
        {
            rating = 4;
            strRating = "GOOD";
            return;
        }
        if (result.AverageErrorDegree < 1)
        {
            rating = 3;
            strRating = "MODERATE";
            return;
        }
        if (result.AverageErrorDegree < 1.5)
        {
            rating = 2;
            strRating = "POOR";
            return;
        }
        rating = 1;
        strRating = "REDO";
    }
}
