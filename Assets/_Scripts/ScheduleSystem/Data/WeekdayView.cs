using _Scripts.Core;
using UnityEngine;

namespace _Scripts.ScheduleSystem.Data
{
    public class WeekdayView : MonoBehaviour
    {
        [SerializeField] private SerializableDictionary<View.WeekDayName, View.DayView> _days;
    }
}
