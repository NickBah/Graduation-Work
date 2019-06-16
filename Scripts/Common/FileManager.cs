using System;
using UnityEngine;
using System.Collections;
using System.IO;

public static class FileManager
{	

	#if UNITY_ANDROID && !UNITY_EDITOR
		//private static AndroidJavaObject file_manager = FileManagerActivity.activityObj.Get<AndroidJavaObject>("manager");
		private static AndroidJavaObject file_manager;
	#else
		private static AndroidJavaObject file_manager;
	#endif

	// Запись в конец файла
	public static void AppendFile(string fileName, string Data){
		Debug.Log("Запись в конец файла [" + fileName +"]");
		// Запись файлов на Android
        if (Application.platform == RuntimePlatform.Android){
				file_manager.Call("AppendFile",fileName,Data);
		} else {
			// Запись файлов в редакторе Unity
			if (Application.platform == RuntimePlatform.WindowsEditor){
				
				string path = fileName.Substring(0,fileName.LastIndexOf('/'));
				
				bool exists = System.IO.Directory.Exists(path);

				if(!exists){
					System.IO.Directory.CreateDirectory(path);
				}
			
				StreamWriter outFile = new StreamWriter(new FileStream(fileName, FileMode.Append));		
				outFile.Write(Data);
				outFile.Close();	
			}
		}
	}
	
	public static void AppendFile(string fileName, array_element[] Data){
			// Запись файлов в редакторе Unity
			if (Application.platform == RuntimePlatform.WindowsEditor){
				string path = fileName.Substring(0,fileName.LastIndexOf('/'));
				
				bool exists = System.IO.Directory.Exists(path);

				if(!exists){
					System.IO.Directory.CreateDirectory(path);
				}
				
				StreamWriter outFile = new StreamWriter(new FileStream(fileName, FileMode.Append));
				for (int i=0;i<Data.Length;i++){				
					outFile.Write (Data[i].frequency + "\n");
				}
				outFile.Write ("###\n");
				outFile.Close();		
			}
		
	}	
	
	// Перезапись файла
	public static void WriteFile(string fileName, string Data){
		// Debug.Log("Запись в файл [" + fileName +"]");
		// Запись файлов на Android
        if (Application.platform == RuntimePlatform.Android){
            file_manager.Call("WriteFile",fileName,Data);
		} else {
			// Запись файлов в редакторе Unity
			if (Application.platform == RuntimePlatform.WindowsEditor){
				
				string path = fileName.Substring(0,fileName.LastIndexOf('/'));
				
				bool exists = System.IO.Directory.Exists(path);

				if(!exists){
					System.IO.Directory.CreateDirectory(path);
				}
			
				StreamWriter outFile = new StreamWriter(new FileStream(fileName, FileMode.Create));		
				outFile.Write (Data);
				outFile.Close();	
			}
		}
	}
	
	// Перезапись файла - массив строк
	public static void WriteFile(string fileName, string[] Data){
		//Debug.Log("Запись в файл [" + fileName +"]");
		
		// Запись файлов на Android
        if (Application.platform == RuntimePlatform.Android){
			string res = System.String.Empty;
			for (int i=0;i<Data.Length;i++){
				res += Data[i] + "\n";
			}
            file_manager.Call("WriteFile",fileName,res);
		} else {
			// Запись файлов в редакторе Unity
			if (Application.platform == RuntimePlatform.WindowsEditor){
				string path = fileName.Substring(0,fileName.LastIndexOf('/'));
				
				bool exists = System.IO.Directory.Exists(path);

				if(!exists){
					System.IO.Directory.CreateDirectory(path);
				}
				
				StreamWriter outFile = new StreamWriter(new FileStream(fileName, FileMode.Create));
				for (int i=0;i<Data.Length;i++){				
					outFile.Write (Data[i] + "\n");
				}
				outFile.Close();		
			}
		}
	}
	/*
	// Перезапись файла
	public static void WriteFile(string fileName, Complex[] Data){
		//Debug.Log("Запись в файл [" + fileName +"]");
		
		// Запись файлов на Android
        if (Application.platform == RuntimePlatform.Android){
			string res = System.String.Empty;
			for (int i=0;i<Data.Length;i++){
				res += Data[i] + "\n";
			}
            file_manager.Call("WriteFile",fileName,res);
		} else {
			// Запись файлов в редакторе Unity
			if (Application.platform == RuntimePlatform.WindowsEditor){
				string path = fileName.Substring(0,fileName.LastIndexOf('/'));
				
				bool exists = System.IO.Directory.Exists(path);

				if(!exists){
					System.IO.Directory.CreateDirectory(path);
				}
				
				StreamWriter outFile = new StreamWriter(new FileStream(fileName, FileMode.Create));
				for (int i=0;i<Data.Length;i++){				
					outFile.Write (Data[i].Imag + "\n");
				}
				outFile.Close();		
			}
		}
	}	
*/
	// Перезапись файла
	public static void WriteFile(string fileName, float[] Data){
		//Debug.Log("Запись в файл [" + fileName +"]");
		
		// Запись файлов на Android
        if (Application.platform == RuntimePlatform.Android){
			string res = System.String.Empty;
			for (int i=0;i<Data.Length;i++){
				res += Data[i] + "\n";
			}
            file_manager.Call("WriteFile",fileName,res);
		} else {
			// Запись файлов в редакторе Unity
			if (Application.platform == RuntimePlatform.WindowsEditor){
				string path = fileName.Substring(0,fileName.LastIndexOf('/'));
				
				bool exists = System.IO.Directory.Exists(path);

				if(!exists){
					System.IO.Directory.CreateDirectory(path);
				}
				
				StreamWriter outFile = new StreamWriter(new FileStream(fileName, FileMode.Create));
				for (int i=0;i<Data.Length;i++){				
					outFile.Write (Data[i] + "\n");
				}
				outFile.Close();		
			}
		}
	}
	// Перезапись файла
	public static void WriteFile(string fileName, double[] Data){
		//Debug.Log("Запись в файл [" + fileName +"]");
		
		// Запись файлов на Android
        if (Application.platform == RuntimePlatform.Android){
			string res = System.String.Empty;
			for (int i=0;i<Data.Length;i++){
				res += Data[i] + "\n";
			}
            file_manager.Call("WriteFile",fileName,res);
		} else {
			// Запись файлов в редакторе Unity
			if (Application.platform == RuntimePlatform.WindowsEditor){
				string path = fileName.Substring(0,fileName.LastIndexOf('/'));
				
				bool exists = System.IO.Directory.Exists(path);

				if(!exists){
					System.IO.Directory.CreateDirectory(path);
				}
				
				StreamWriter outFile = new StreamWriter(new FileStream(fileName, FileMode.Create));
				for (int i=0;i<Data.Length;i++){				
					outFile.Write (String.Format("{0}",Data[i]));
				}
				outFile.Close();		
			}
		}
	}	
	
	// Перезапись файла
	public static void WriteFileComplex(string fileName, float[] Data){
		//Debug.Log("Запись в файл [" + fileName +"]");
		
		// Запись файлов на Android
        if (Application.platform == RuntimePlatform.Android){
			string res = System.String.Empty;
			for (int i=0;i<Data.Length;i++){
				res += Data[i] + "\n";
			}
            file_manager.Call("WriteFile",fileName,res);
		} else {
			// Запись файлов в редакторе Unity
			if (Application.platform == RuntimePlatform.WindowsEditor){
				string path = fileName.Substring(0,fileName.LastIndexOf('/'));
				
				bool exists = System.IO.Directory.Exists(path);

				if(!exists){
					System.IO.Directory.CreateDirectory(path);
				}
				
				StreamWriter outFile = new StreamWriter(new FileStream(fileName, FileMode.Create));
				for (int i=0;i<Data.Length;i++){				
					outFile.Write (String.Format("{0} + 0i\n",Data[i]));
				}
				outFile.Close();		
			}
		}
	}	
	
	// Перезапись файла
	public static void WriteFile(string fileName, string fileNameFreq, array_element[] Data){
		//Debug.Log("Запись в файл [" + fileName +"]");
		
		// Запись файлов на Android
        if (Application.platform == RuntimePlatform.Android){
			string res = System.String.Empty;
			for (int i=0;i<Data.Length;i++){
				res += Data[i] + "\n";
			}
            file_manager.Call("WriteFile",fileName,res);
		} else {
			// Запись файлов в редакторе Unity
			if (Application.platform == RuntimePlatform.WindowsEditor){
				int last = fileName.LastIndexOf('/');
				
				if (last == -1){
					last = fileName.LastIndexOf('\\');
				}
				
				string path = fileName.Substring(0,last);
				
				bool exists = System.IO.Directory.Exists(path);

				if(!exists){
					System.IO.Directory.CreateDirectory(path);
				}
				
				StreamWriter outFile = new StreamWriter(new FileStream(fileName, FileMode.Create));
				StreamWriter outFile2 = new StreamWriter(new FileStream(fileNameFreq, FileMode.Create));
				//StreamWriter outFile3 = new StreamWriter(new FileStream(fileNameImp, FileMode.Create));
				for (int i=0;i<Data.Length;i++){				
					outFile.Write (String.Format("{0} \n",Data[i].value));
					outFile2.Write (String.Format("{0} \n",Data[i].frequency));
					//outFile3.Write (String.Format("{0} \n",Data[i].importance));
				}
				outFile.Close();	
				outFile2.Close();	
				//outFile3.Close();				
			}
		}
	}	

	// Чтение файла одной строкой
	public static string ReadFileInOneLine(string fileName){
		string res = System.String.Empty;
		// Чтение файла на Android
        if (Application.platform == RuntimePlatform.Android){
			res = file_manager.Call<string>("ReadFile",fileName);
		} else {
			// Чтение файла в редакторе Unity
			if (Application.platform == RuntimePlatform.WindowsEditor){
				StreamReader inFile = new StreamReader(new FileStream(fileName, FileMode.Open));
				res = inFile.ReadToEnd();
				inFile.Close();
			}
			
		}
		return res;
	}
	

	
	// Чтение файла
	public static string[] ReadFile(string fileName){
		string[] res = new string[0];
		// Чтение файла на Android
        if (Application.platform == RuntimePlatform.Android){
			string tmp = file_manager.Call<string>("ReadFile",fileName);
			res = tmp.Split('\n');
		} else {
			// Чтение файла в редакторе Unity
			if (Application.platform == RuntimePlatform.WindowsEditor){
				StreamReader inFile = new StreamReader(new FileStream(fileName, FileMode.Open));
				res = inFile.ReadToEnd().Split('\n');
				inFile.Close();
			}
			
		}
		return res;
	}
	
	
	// Проверка на существование файла
    public static bool FileExists(string path)
    {	
		bool res = false;
        if (Application.platform == RuntimePlatform.Android){
            //res = file_manager.Call<bool>("FileExists",path);
			
			// Пока заглушка
			res = true;
		} else {
			if (Application.platform == RuntimePlatform.WindowsEditor){
				res = File.Exists(path);
			}
		}
		return res;
    }
	
	// Создание, если нет
    public static void createIfNotExists(string path)
    {	
		if (Application.platform == RuntimePlatform.Android){
			file_manager.Call("CreateIfNotExists",path);
		} else {
			if (Application.platform == RuntimePlatform.WindowsEditor){			
				bool exists = System.IO.Directory.Exists(path);

				if (exists == false){
					System.IO.Directory.CreateDirectory(path);
				}
			}
		
		}
    }
	
	// Удалить файл
	public static void delete(string path){
		if (Application.platform == RuntimePlatform.Android){
			file_manager.Call("DeleteFile",path);
		} else {
			if (Application.platform == RuntimePlatform.WindowsEditor){
				if(File.Exists(path))
				{
					File.Delete(path);
				}		
			}
		}
	}
	
	// Получить список папок в каталоге 
	public static string[] GetDirsList(string dir){
		if (Application.platform == RuntimePlatform.WindowsEditor){
				if (Directory.Exists(dir)){
					string[] folders = Directory.GetDirectories(dir);
					for (int i=0;i<folders.Length;i++){
						string folder = folders[i];
						int start = folder.LastIndexOf('/') + 1;
						int len = folder.Length - start;
						folders[i] = folder.Substring(start,len);
					}
					return folders;
				}
		}		
		return new string[0];
	}
	
	// Получить список файлов в каталоге. Фильтр по расширению необязателен
	public static string[] GetFilesList(string dir, string extension = ""){
		if (Application.platform == RuntimePlatform.WindowsEditor){
				if (Directory.Exists(dir)){
				
					string[] files = Directory.GetFiles(dir);
					string[] res = new string[0];
					
					for (int i=0;i<files.Length;i++){
						string file = files[i];
						int start = file.LastIndexOf('\\') + 1;
						int len = file.Length - start;
						files[i] = file.Substring(start,len);
					}					
					
					if (extension == ""){
						return files;
					}
						
					for (int i=0;i<files.Length;i++){
						if (Path.GetExtension(files[i]).ToUpper() == extension.ToUpper()){
							Array.Resize(ref res, res.Length + 1);
							res[res.Length - 1] = files[i];
						}
					}

					for (int i=0;i<res.Length;i++){
						string file = res[i];
						int len = file.LastIndexOf('.');
						res[i] = file.Substring(0,len);
					}

					return res;
				}
		}
		return new string[0];
	}
	
}
