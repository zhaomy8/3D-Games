using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myGame;

public class Ruler : MonoBehaviour
{
    public int[] score;

    public Ruler()
    {
        score = new int[10];
    }

    public void init()
    {
        for (int i = 0; i < 10; i++)
            score[i] = 0;
    }

    public void updateScore(int roundIndex)
    {
        score[roundIndex] += 1;
    }

    public float setInterval(int round)
    {
        return (float)(2 - 0.2 * round);
    }

    public int getTargetThisRound(int round)
    {
        if (round != -1)
        {
            return 5 + round > 10 ? 10 : 5 + round;
        }
        return 0;
    }

    public bool enterNextRound(int round)
    {
        if (round != -1 && this.score[round - 1] >= (5 + round > 10 ? 10 : 5 + round))
        {
            return true;
        }
        return false;
    }
    public Vector3 setScale(int round)
    {
        float x = Random.Range((float)(1 - 0.1 * round), (float)(2 - 0.1 * round));
        float y = Random.Range((float)(1 - 0.1 * round), (float)(2 - 0.1 * round));
        float z = Random.Range((float)(1 - 0.1 * round), (float)(2 - 0.1 * round));
        return new Vector3(x, y, z);
    }

    public float setPower(int round)
    {
        if (round == 1)
        {
            return 2;
        }
        return 3 * round;
    }
}
