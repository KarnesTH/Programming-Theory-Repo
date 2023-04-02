using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reciepes : IngredientContainer
{
    public string Name { get; set; }

    public Reciepes(string name, float water, float coffee, float milk, float sugar) : base(water, coffee, milk, sugar)
    {
        Name = name;
    }
}
