using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.ExzelCode.Test2
{
    public class SaveTest : MonoBehaviour 
    {

        [SerializeField] private Button _copyFileButton;

        private void Start()
        {
            _copyFileButton.onClick.AddListener(CopyFile);
        }

        public void CopyFile()
        {
            OpenFileData ofn = new OpenFileData();
            ofn.structSize = Marshal.SizeOf(ofn);
            ofn.filter = "DefaultExcel(*.xlsx)\0*.xlsx";;
            ofn.file = new string(new char[256]);
            ofn.maxFile = ofn.file.Length;
            ofn.fileTitle = new string(new char[64]);
            ofn.maxFileTitle = ofn.fileTitle.Length;
            ofn.initialDir = UnityEngine.Application.dataPath; // путь по умолчанию
            ofn.title = "Save Excel xlsx";
            ofn.defExt = "xlsx";
            ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;
        
            if (SaveDll.GetSaveFileName(ofn))
            {
                StartCoroutine(WaitSaveExcelXLSX(ofn.file));
            }
        }

        IEnumerator WaitSaveExcelXLSX(string fileName)
        {
            FileInfo fi = new FileInfo(Path.Combine(Application.streamingAssetsPath, "Example.xlsx"));

            var newFl = fi.CopyTo(fileName, true);

            yield return fi;
        }
 
    }
}