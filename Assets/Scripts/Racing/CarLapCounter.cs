using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class CarLapCounter : MonoBehaviour
{
    [SerializeField]
    private float maxScore = 300f;
    [SerializeField]
    private float scoreDifferential = 50f;

    private int passedCheckpointNumber = 0;
    private float timeAtLastPassedCheckPoint = 0f;
    private int carPosition;
    public int lapsCompleted = 0;
    public int lapsToComplete = 3;
    public bool isRaceCompleted = false;

    public int numberOfPassedCheckPoints = 0;

    public event Action<CarLapCounter> OnPassCheckPoint;

    public UnityEvent<string> OnCarPositionChanged;
    public UnityEvent<float> OnPlayerFinishedRace;

    private void Start()
    {
        PositionHandler positionHander = FindObjectOfType<PositionHandler>();

        positionHander.carLapCounters.Add(this);
        OnPassCheckPoint += positionHander.OnPassCheckPoint;
    }

    public void SetCarPosition(int position)
    {
        carPosition = position;

        Debug.Log("position: " + carPosition);

        switch(carPosition)
        {
            case 1:
                OnCarPositionChanged?.Invoke("1st");
                break;
            case 2:
                OnCarPositionChanged?.Invoke("2nd");
                break;
            case 3:
                OnCarPositionChanged?.Invoke("3rd");
                break;
            default:
                OnCarPositionChanged?.Invoke(carPosition.ToString() + "th");
                break;
        }
    }

    public int GetNumberOfCheckPointsPassed()
    {
        return numberOfPassedCheckPoints;
    }

    public float GetTimeAtLastCheckPoint()
    {
        return timeAtLastPassedCheckPoint;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("collision with: " + collision.gameObject.tag);
        if (collision.CompareTag("CheckPoint"))
        {
            CheckPoint checkPoint = collision.GetComponent<CheckPoint>();

            //Debug.Log("passedCheckpointNumber + 1: " + (passedCheckpointNumber + 1) + " == " + checkPoint.checkPointNumber + " :checkPoint.checkPointNumber");

            if (passedCheckpointNumber + 1 == checkPoint.checkPointNumber)
            {
                passedCheckpointNumber = checkPoint.checkPointNumber;

                Debug.Log("passedCheckPoint+: " + passedCheckpointNumber);

                numberOfPassedCheckPoints++;

                timeAtLastPassedCheckPoint = Time.time;

                if(checkPoint.isFinishLine)
                {
                    Debug.Log("finish_line");
                    passedCheckpointNumber = 0;
                    lapsCompleted += 1;

                    if(lapsCompleted >= lapsToComplete)
                    {
                        isRaceCompleted = true;

                        float points = maxScore - ((carPosition - 1) * scoreDifferential);

                        OnPlayerFinishedRace?.Invoke(points);
                    }
                }

                Debug.Log("before on_pass_check_point");

                OnPassCheckPoint?.Invoke(this);
            }
        }
    }
}
