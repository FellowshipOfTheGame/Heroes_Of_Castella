using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using System;
using System.Reflection;


public static class EnumAttribute
{
    public static string GetDescription<T>(this T enumerationValue)
    where T : struct
    {
        Type type = enumerationValue.GetType();
        if (!type.IsEnum)
        {
            throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
        }

        //Tries to find a DescriptionAttribute for a potential friendly name
        //for the enum
        MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
        if (memberInfo != null && memberInfo.Length > 0)
        {
            object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attrs != null && attrs.Length > 0)
            {
                //Pull out the description value
                return ((DescriptionAttribute)attrs[0]).Description;
            }
        }
        //If we have no description attribute, just return the ToString of the enum
        return enumerationValue.ToString();
    }
}

[System.Serializable]
public class Character
{
    public enum EquipmentType
    {
        [Description("Armor")]
        ARMOR,
        [Description("Helmet")]
        HELMET,
        [Description("Legs")]
        LEGS,
        [Description("Armor")]
        SHIELD,
        COUNT
    }

    

    [System.Serializable]
    public struct Attributes
    {
        int strength;
        int agility;
        int vitality;
    }

    [System.Serializable]
    public struct Personality
    {
        int agressivity;
        int empathy;
    }

    [System.Serializable]
    public struct CharacterInfo
    {
        public string name;
        public Attributes attributes;
        public Personality personality;

    }


    public CharacterInfo characterInfo;
    public string name;
    public Attributes attributes;
    public Personality personality;
    [NamedArrayAttribute (new string[] {"Armor", "Helmet", "Legs", "Shield"})]
    public int[] equipment;
    public Skill[] skills;

    public void IterationOnStruct()
    {
        foreach (var field in typeof(CharacterInfo).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
        {
            Console.WriteLine("{0} = {1}", field.Name, field.GetValue(characterInfo));
        }
    }
}
