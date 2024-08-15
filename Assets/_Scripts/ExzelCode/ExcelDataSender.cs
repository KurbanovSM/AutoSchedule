using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace _Scripts.ExzelCode
{
    // Данные для записи

    public class ExcelDataSender : MonoBehaviour
    {
        [SerializeField] private List<Person> _persons;

        private void Start()
        {
            Debug.Log(Path.Combine(Application.streamingAssetsPath, "Example.xlsx").Replace("\\", "/"));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SendDataToExcel();
            }
        }

        public void SendDataToExcel()
        {
            // Путь к файлу Excel
            string filePath = Path.Combine(Application.streamingAssetsPath, "Example.xlsx").Replace("\\", "/");;
        
            string consoleProgramExzelHandler = Path.Combine(Application.streamingAssetsPath, "ExzelHandler/ExzelHandler.exe");

            string argument = JsonConvert.SerializeObject(_persons);
        
            argument = argument.Replace("\"", "\\\"");
        
            // Запускаем .NET приложение
            Process process = new Process();
            process.StartInfo.FileName = consoleProgramExzelHandler; 
        
            // Замените "path/to/your/ExcelWriter.exe" на фактический путь к вашему .NET приложению

            // Передаем параметры
            process.StartInfo.Arguments = $"\"{filePath}\" " + $"\"{argument}\" ";
            //process.StartInfo.UseShellExecute = false;
            //process.StartInfo.CreateNoWindow = true;
            // Запускаем процесс
            process.Start();

            // Ожидаем завершения процесса
            process.WaitForExit();

            Debug.Log("Данные отправлены в Excel!");
        }
    }
}