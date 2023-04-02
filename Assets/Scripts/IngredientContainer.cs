using System.Collections;
using System.Collections.Generic;

public class IngredientContainer : Container, IService
{
    public float Water { get; set; }
    public float Coffee { get; set; }
    public float Milk { get; set; }
    public float Sugar { get; set; }

    public IngredientContainer(float water, float coffee, float milk, float sugar)
    {
        Water = water;
        Coffee = coffee;
        Milk = milk;
        Sugar = sugar;
    }

    public void Service()
    {
        Water = 1.0f;
        Coffee = 1.0f;
        Milk = 1.0f;
        Sugar = 1.0f;
    }
}
