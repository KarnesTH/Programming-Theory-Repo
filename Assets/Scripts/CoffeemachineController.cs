using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class CoffeemachineController : MonoBehaviour
{  
    IngredientContainer zb = new IngredientContainer(1.0f, 1.0f, 1.0f, 1.0f);
    GarbageContainer gc = new GarbageContainer();
    Reciepes[] reciepes =
    {
        new Reciepes("Coffee", 0.2f, 0.02f, 0, 0),
        new Reciepes("Coffee + Milk", 0.2f, 0.02f, 0.02f, 0),
        new Reciepes("Coffee + M. + S.", 0.2f, 0.02f, 0.02f, 0.02f)
    };

    // public variables
    // text
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI waterValueText;
    public TextMeshProUGUI coffeeValueText;
    public TextMeshProUGUI milkValueText;
    public TextMeshProUGUI sugarValueText;
    public TextMeshProUGUI icValueText;
    public TextMeshProUGUI gcValueText;

    // gameObjects
    public GameObject statusBox;
    public GameObject coffeeParticle;
    
    // private variables
    private float currentWater;
    private float currentCoffee;
    private float currentMilk;
    private float currentSugar;
    private int currentIdxNum;
    private bool isCoffeeStarting;
    private bool showStatus;
    private bool isGcFull;
    private bool isWaterEmpty;

    public CoffeemachineController()
    {
        currentWater = zb.Water;
        currentCoffee = zb.Coffee;
        currentMilk = zb.Milk;
        currentSugar = zb.Sugar;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentIdxNum = 0;
        isCoffeeStarting = false;
        showStatus = false;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < reciepes.Length; i++)
        {
            if (reciepes[i].Name == reciepes[currentIdxNum].Name)
            {
                infoText.text = reciepes[i].Name;
            }
        }
        if (isCoffeeStarting)
        {
            coffeeParticle.SetActive(true);
        }
        else
        {
            coffeeParticle.SetActive(false);
        }
        if (isGcFull)
        {
            infoText.text = "Clear GC";
        }
        if (isWaterEmpty)
        {
            infoText.text = "Fill IC";
        }
    }

    public void EndCoffeemachine()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void StartCoffeemachine()
    {
        if (gc.MaxGarbageContains == 1.0f)
        {
            isGcFull = true;
        }
        else if (zb.Water == 0.2f)
        {
            isWaterEmpty = true;
        }
        else
        {
            isCoffeeStarting = true;

            if (reciepes[currentIdxNum].Water == +0.2f)
            {
                currentWater = zb.Water - reciepes[currentIdxNum].Water;
                zb.Water = currentWater;
            }

            if (reciepes[currentIdxNum].Coffee == +0.02f)
            {
                currentCoffee = zb.Coffee - reciepes[currentIdxNum].Coffee;
                zb.Coffee = currentCoffee;
            }

            if (reciepes[currentIdxNum].Milk == +0.02f)
            {
                currentMilk = zb.Milk - reciepes[currentIdxNum].Milk;
                zb.Milk = currentMilk;
            }

            if (reciepes[currentIdxNum].Sugar == +0.02f)
            {
                currentSugar = zb.Sugar - reciepes[currentIdxNum].Sugar;
                zb.Sugar = currentSugar;
            }

            if (reciepes[currentIdxNum].Coffee == +0.02f)
            {
                float garbage = 0.2f;
                float fill = gc.MaxGarbageContains + garbage;
                gc.MaxGarbageContains = fill;
            }

            StartCoroutine(MakeCoffee());
        }
    }

    public void PrevReciep()
    {
        if (currentIdxNum > 0)
        {
            currentIdxNum--;
        }
        else
        {
            currentIdxNum = reciepes.Length - 1;
        }
    }

    public void NextReciep()
    {
        if (currentIdxNum < reciepes.Length - 1)
        {
            currentIdxNum++;
        }
        else
        {
            currentIdxNum = 0;
        }
    }

    public void ShowStatus()
    {
        if (!showStatus)
        {
            showStatus = true;
            statusBox.SetActive(true);
            waterValueText.text = $"{Math.Round(zb.Water, 3)}L";
            coffeeValueText.text = $"{Math.Round(zb.Coffee, 3)}L";
            milkValueText.text = $"{Math.Round(zb.Milk, 3)}L";
            sugarValueText.text = $"{Math.Round(zb.Sugar, 3)}L";
            icValueText.text = $"{Math.Round((zb.Water + zb.Coffee + zb.Milk + zb.Sugar), 3)}L";
            gcValueText.text = $"{Math.Round(gc.MaxGarbageContains, 3)}L";
        }
        else
        {
            statusBox.SetActive(false);
            showStatus = false;
        }
    }

    public void ClearGarbageContainer()
    {
        gc.Service();
    }

    public void FillIngredientContainer()
    {
        zb.Service();
    }

    IEnumerator MakeCoffee()
    {
        yield return new WaitForSeconds(5.0f);
        isCoffeeStarting = false;
    }
}
