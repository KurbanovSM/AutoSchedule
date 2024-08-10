using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _Scripts.ClassSystem
{
    public class Day : MonoBehaviour
    {
        [SerializeField] private TMP_Text _dayNameText;
        [SerializeField] private List<Learn> _learns;
    }
}
