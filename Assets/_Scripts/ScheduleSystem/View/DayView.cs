using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _Scripts.ScheduleSystem.View
{
    public class DayView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _dayNameText;
        [SerializeField] private List<LearnView> _learns;
    }
}
