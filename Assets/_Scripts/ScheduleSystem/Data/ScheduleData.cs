using System;using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.ScheduleSystem.Data
{
    public class ScheduleData : MonoBehaviour
    {
        [SerializeField] private List<string> _listSchoolClasses;
        [SerializeField] private List<string> _listClassrooms;
        [SerializeField] private List<string> _listTextbooks;
        [SerializeField] private List<string> _listTeachers;
    }
}

public struct ClassNumber
{
    public List<string> Number;
}
