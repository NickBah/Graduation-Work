using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class read_file {
	
	public static string loadFileContent(string path)
    {
		string res = System.String.Empty;

		WWW reader = new WWW(path);  

		while (!reader.isDone) {}
			
		if (reader.error != null){		
			if (reader.error == "Unknown Error"){
				Debug.LogError("Не удается прочитать файл [" + path + "]");
			}
		}
		
		res = reader.text;
		reader.Dispose();

		return res;
	}
	
	public static byte[] loadFileContentBytes(string path)
    {
		byte[] res = new byte[0];

		WWW reader = new WWW(path);  

		while (!reader.isDone) {}
			
		if (reader.error != null){		
			//if (reader.error == "Unknown Error"){
				Debug.LogError(reader.error);
				Debug.LogError("Не удается прочитать файл [" + path + "]");
			//}
				return new byte[0];
		}
		
		res = reader.bytes;
		reader.Dispose();

		return res;
	}	
}
