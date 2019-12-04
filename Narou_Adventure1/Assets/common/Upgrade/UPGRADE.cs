using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UPGRADE : POPTEXT_PU
{
    ////LevelはSaveするよ
    //public virtual int level { get; set; }
    //protected int[][] upgradeId;
    //protected double initValue;
    //protected double initCost;
    //protected double plusValue;
    //public double bottom;
    //public double Aug1,AugM1;
    //public double Jem1,Jem3, WineAdd1,WineMul1;
    //public double Asc1, AscM1;
    //public double Ruby1;
    //public enum buyMode
    //{
    //    nothing,
    //    mode1,
    //    mode10,
    //    mode25,
    //    modeMax
    //}
    //Button thisButton;

    //public void startUpgrade(double initValue, double initCost, double plus,double bottom)
    //{
    //    this.initValue = initValue;
    //    this.initCost = initCost;
    //    this.bottom = bottom;
    //    plusValue = plus;
    //    thisButton = gameObject.GetComponent<Button>();
    //    gameObject.GetComponent<Button>().onClick.AddListener(upgrade);
    //}
    //virtual public void upgrade() { }
    //public double calcurateCurrentCost()
    //{
    //    switch (main.SR.buyMode) {

    //        case buyMode.mode1:
    //        return initCost * Math.Pow(CorrectBottom(), level);
    //        case buyMode.mode10:
    //            return calculateSumCost(level, 10);
    //        case buyMode.mode25:
    //            return calculateSumCost(level, 25);
    //        case buyMode.modeMax:
    //            return calculateMaxSumCost(level).x ==0? initCost * Math.Pow(bottom * Math.Pow(0.995, main.ascendList[11].level), level)
    //                : calculateMaxSumCost(level).x;
    //        case buyMode.nothing:
    //            return 0;
    //        default:
    //            return 0;
    //    }
    //}

    //public double CorrectBottom()
    //{
    //    return 1 + (bottom - 1) * Math.Pow(0.995, main.ascendList[11].level);
    //}

    //public double calculateCurrentCost(int level)
    //{
    //    return initCost * Math.Pow(CorrectBottom(), level);
    //}

    //public double calculateSumCost(int currentLevel, int plusLevel)
    //{
    //    int tempLevel = currentLevel;
    //    double sumCost=0;

    //    for(int i=0; i<plusLevel; i++)
    //    {
    //        sumCost += calculateCurrentCost(tempLevel);
    //        tempLevel++;
    //    }

    //    return sumCost;
    //}

    //public virtual (double x, int y) calculateMaxSumCost(int currentLevel)
    //{
    //    int tempLevel = currentLevel;
    //    double sumCost = 0;

    //    while (true)
    //    {
    //        if (main.SR.money < sumCost)
    //        {
    //            sumCost -= calculateCurrentCost(tempLevel-1);
    //            return (sumCost, tempLevel-1 - currentLevel);
    //        }
    //        else
    //        {
    //            sumCost += calculateCurrentCost(tempLevel);
    //        }

    //        tempLevel++;
    //    }
    //}
    //public void calcurate(ref double resources)
    //{
    //    // resources -= calcurateCurrentCost();
    //    // level++;
    //    double tempX=0;
    //    int tempY=0;

    //    switch (main.SR.buyMode)
    //    {

    //        case buyMode.mode1:
    //            resources -= calcurateCurrentCost();
    //            level++;
    //            break;
    //        case buyMode.mode10:
    //            resources -= calcurateCurrentCost();
    //            level+=10;
    //            break;
    //        case buyMode.mode25:
    //            resources -= calcurateCurrentCost();
    //            level += 25;
    //            break;
    //        case buyMode.modeMax:
    //            (tempX, tempY) = calculateMaxSumCost(level);
    //            resources -= tempX;
    //            level += tempY;
    //            break;
    //        case buyMode.nothing:
    //            break;
    //        default:
    //            break;
    //    }
    //}

    //public virtual double calculateCurrentValue()
    //{
    //    return (initValue + level * (plusValue + Aug1)+Asc1 + WineAdd1) * (1 + Jem1) * (1 + Jem3) * (1 + WineMul1)*(1+AugM1)*(1+AscM1) * (1 + Ruby1);
    //}

    //public virtual double calculateCurrentValue(int level)
    //{
    //    return (initValue + level * (plusValue + Aug1)+Asc1 + WineAdd1) * (1 + Jem1) * (1 + Jem3) * (1 + WineMul1) * (1 + AugM1) *(1+AscM1) *(1+Ruby1);
    //}

    //public double calculateNextValue()
    //{
    //  int tempLevel = level;
    //  switch (main.SR.buyMode)
    //    {

    //        case buyMode.mode1:
    //            tempLevel++;
    //            double ans = calculateCurrentValue(tempLevel);
    //            //level--;
    //            return ans;
    //        case buyMode.mode10:
    //            tempLevel+=10;
    //            double ans10 = calculateCurrentValue(tempLevel);
    //            //level-=10;
    //            return ans10;
    //        case buyMode.mode25:
    //            tempLevel+=25;
    //            double ans25 = calculateCurrentValue(tempLevel);
    //            //level-=25;
    //            return ans25;
    //        case buyMode.modeMax:
    //            tempLevel+=calculateMaxSumCost(level).y;
    //            double ansMax = calculateCurrentValue(tempLevel);
    //            //tempLevel-= calculateMaxSumCost(level).y;
    //            return ansMax;
    //        case buyMode.nothing:
    //            return 0;
    //        default:
    //            return 0;
    //    }

    //}
    //public double calculateNextSub()
    //{
    //    return calculateNextValue() - calculateCurrentValue();
    //}
    //public void checkButton(double Resources)
    //{
    //    if (main.SR.buyMode == buyMode.modeMax)
    //    {
    //        if (calculateMaxSumCost(level).x == 0)
    //        {
    //           thisButton.interactable = false;
    //        }
    //        else
    //        {
    //            thisButton.interactable = true;
    //        }
    //    }
    //    else
    //    {
    //        if (calcurateCurrentCost() > Resources)
    //        {
    //           thisButton.interactable = false;
    //        }
    //        else
    //        {
    //           thisButton.interactable = true;
    //        }
    //    }
    //}

}
