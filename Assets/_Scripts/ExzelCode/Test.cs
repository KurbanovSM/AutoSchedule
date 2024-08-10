using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using OfficeOpenXml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class OpenFileName
{
    public int structSize = 0;
    public IntPtr dlgOwner = IntPtr.Zero;
    public IntPtr instance = IntPtr.Zero;
    public String filter = null;
    public String customFilter = null;
    public int maxCustFilter = 0;
    public int filterIndex = 0;
    public String file = null;
    public int maxFile = 0;
    public String fileTitle = null;
    public int maxFileTitle = 0;
    public String initialDir = null;
    public String title = null;
    public int flags = 0;
    public short fileOffset = 0;
    public short fileExtension = 0;
    public String defExt = null;
    public IntPtr custData = IntPtr.Zero;
    public IntPtr hook = IntPtr.Zero;
    public String templateName = null;
    public IntPtr reservedPtr = IntPtr.Zero;
    public int reservedInt = 0;
    public int flagsEx = 0;
}
public class LocalDialog
{
    //链接指定系统函数       打开文件对话框
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetOpenFileName([In, Out] OpenFileName ofn);
    public static bool GetOFN([In, Out] OpenFileName ofn)
    {
        return GetOpenFileName(ofn);
    }

    //链接指定系统函数        另存为对话框
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetSaveFileName([In, Out] OpenFileName ofn);
    public static bool GetSFN([In, Out] OpenFileName ofn)
    {
        return GetSaveFileName(ofn);
    }
}
public class Test : MonoBehaviour
{
    [SerializeField] private Button saveButton;

    [SerializeField] private TMP_Text _tmpText;
	// Use this for initialization
	void Start () {
        saveButton.onClick.AddListener(() =>
        {
            CreatExcel();
        });
		
        Debug.Log(Application.streamingAssetsPath);
        _tmpText.text = Application.streamingAssetsPath;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void CreatExcel()
    {
        OpenFileName openFileName = new OpenFileName();
        openFileName.structSize = Marshal.SizeOf(openFileName);
        openFileName.filter = "ExcelTest(*.xlsx)\0*.xlsx";
        openFileName.file = new string(new char[256]);
        openFileName.maxFile = openFileName.file.Length;
        openFileName.fileTitle = new string(new char[64]);
        openFileName.maxFileTitle = openFileName.fileTitle.Length;
        openFileName.initialDir = Application.streamingAssetsPath.Replace('/', '\\');//默认路径
        openFileName.title = "xlsx";
        openFileName.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;

        if (LocalDialog.GetOpenFileName(openFileName))
        {
            // Получаем путь к StreamingAssets
            string streamingAssetsPath = Application.streamingAssetsPath;
            // Путь к исходному файлу
            string sourceFilePath = Path.Combine(streamingAssetsPath, "MyExcelFile.xlsx"); // "Assets/Resources/Example.xlsx";

            // Путь к файлу-копии
            string destinationFilePath = "C:/path/to/your/copy.xlsx";

            // Дублирование файла
            File.Copy(sourceFilePath, destinationFilePath);

            string createPath = openFileName.file+".xlsx";
            FileInfo newFile = new FileInfo(createPath);
            if (newFile.Exists)
            {
                newFile.Delete();
                newFile = new FileInfo(createPath);
            }
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("table1");//创建worksheet
                worksheet.Column(1).Width = 30;
                worksheet.Cells[1, 1].Value = "我的欧文啊";//直接指定行列数进行赋值
                worksheet.Cells["A2"].Value = "名称";//直接指定单元格进行赋值

                ExcelWorksheet worksheet1 = package.Workbook.Worksheets.Add("table2");//创建worksheet
                worksheet1.Cells[1, 1].Value = "名称";//直接指定行列数进行赋值
                worksheet1.Cells["A2"].Value = "名称";//直接指定单元格进行赋值

                package.Save();//保存excel
            }
        }
        
    }
  
}
