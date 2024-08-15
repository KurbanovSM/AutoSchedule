using _Scripts.Core;
using UnityEngine;

namespace _Scripts.ScheduleSystem.View
{
    public class WeekdayView : MonoBehaviour
    {
        [SerializeField] private SerializableDictionary<WeekDayName, DayView> _days;
    }
}
