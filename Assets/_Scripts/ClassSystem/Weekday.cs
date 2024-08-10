using System.Collections.Generic;
using _Scripts.Core;
using UnityEngine;

namespace _Scripts.ClassSystem
{
    public class Weekday : MonoBehaviour
    {
        [SerializeField] private SerializableDictionary<WeekDayName, Day> _days;
    }
}
