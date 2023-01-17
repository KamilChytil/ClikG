/*
 * using UnityEngine;
using System.Security.Cryptography;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System;
using TMPro;


public class SimpleAES
{

    private const string initVector = "14rukl893fhjl9r5";
    private const int keysize = 256;


    public string EncryptString(string plaiText, string passPhrase)
    {
        byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
        byte[] plaiTextBytes = Encoding.UTF8.GetBytes(plaiText);
        PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
        byte[] keyBytes = password.GetBytes(keysize / 8);
        RijndaelManaged symetricKey = new RijndaelManaged();
        symetricKey.Mode = CipherMode.CBC;
        ICryptoTransform encryptor = symetricKey.CreateEncryptor(keyBytes, initVectorBytes);
        MemoryStream memoryStream = new MemoryStream();
        CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        cryptoStream.Write(plaiTextBytes, 0, plaiTextBytes.Length);
        cryptoStream.FlushFinalBlock();
        byte[] cipherTextBytes = memoryStream.ToArray();
        memoryStream.Close();
        cryptoStream.Close();
        return Convert.ToBase64String(cipherTextBytes);
    }

    public string DecryptString(string cipherText, string passPhrase)
    {
        byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
        byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
        PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
        byte[] keyBytes = password.GetBytes(keysize / 8);
        RijndaelManaged symetricKey = new RijndaelManaged();
        symetricKey.Mode = CipherMode.CBC;
        ICryptoTransform decryptor = symetricKey.CreateEncryptor(keyBytes, initVectorBytes);
        MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
        CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        byte[] plainTextBytes = new byte[cipherTextBytes.Length];
        int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
        memoryStream.Close();
        cryptoStream.Close();
        return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);


    }

}
    public class SaveSystem : MonoBehaviour
    {
        public TMP_InputField importValue;
        public TMP_InputField exportValue;


        public static string json = "";


        protected static string encryptKey = "2xhhr44dj1sc5g6h6f0pvq4g";
        protected static string savePath = "/idletut.save";
        protected static string savePathBackUP = "/idletutBackUp.save";

        public static int backUpCount = 0;

        public static void SavePlayer(PlayerData data)
        {
            var saveTo = backUpCount == 4 ? savePathBackUP : savePath;
            using (StreamWriter writer = new StreamWriter(Application.persistentDataPath + saveTo))
            {
                json = JsonUtility.ToJson(data);
                ConvertStringToBase64(writer, json);
                writer.Close();

                /*
                PlayerPrefs.SetString("OfflineTime", System.DateTime.Now.ToBinary().ToString());
                data.OfflineProgressCheck = true;

            */
            /* 
           * 
           * 
           * }

            backUpCount = (backUpCount + 1) % 5;
        }


        public static string ConvertStringToBase64(StreamWriter writer,string x )
        {
            SimpleAES aes = new SimpleAES();
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(x);
            string stringTemp = Convert.ToBase64String(plainTextBytes);
            writer.WriteLine(aes.EncryptString(stringTemp, encryptKey));
            return stringTemp;


        }
        public static bool LoadSaveFile(ref PlayerData data, string path)
        {
            var success = false;
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {

                    json = ConvertBase64ToString(reader);
                    data = JsonUtility.FromJson<PlayerData>(json);
                    reader.Close();
                    success = true;

                }
            }
            catch(Exception ex)
            {
                Debug.Log("Load Save Failed");
                CreateFile();
            }
            return success;
        }


        public static void CreateFile()
        {
            if(!File.Exists(Application.persistentDataPath + savePathBackUP))
            {
                File.CreateText(Application.persistentDataPath + savePathBackUP);
            }

            if (!File.Exists(Application.persistentDataPath + savePath))
            {
                File.CreateText(Application.persistentDataPath + savePath);
            }


        }
        public static void LoadPlayer(ref PlayerData data)
        {

            CreateFile();
            if(!LoadSaveFile(ref data, Application.persistentDataPath + savePath))
            {
                LoadSaveFile(ref data, Application.persistentDataPath + savePathBackUP);
            }

        }

        
        public static string ConvertBase64ToString(StreamReader reader)
        {
            SimpleAES aes = new SimpleAES();
            string stringConvert = reader.ReadLine();
            var base64EncodeBytes = Convert.FromBase64String(aes.DecryptString(stringConvert, encryptKey));
            return System.Text.Encoding.UTF8.GetString(base64EncodeBytes);

        }
        
    public void ImportPlayer2(GameManager playerData)
    {
        using (StreamWriter writer = new StreamWriter(Application.persistentDataPath + savePath))
        {
            Debug.Log(importValue.text);
            writer.WriteLine(importValue.text);
            writer.Close();
            using (StreamReader reader = new StreamReader(Application.persistentDataPath + savePath))
            {
                json = ConvertBase64ToString(reader);
                playerData.data = JsonUtility.FromJson<PlayerData>(json);
                reader.Close();

            }

        }
        
    }

    public void ExportPlayer2()
    {
        using (StreamReader reader = new StreamReader(Application.persistentDataPath + savePath))
        {
            SimpleAES aes = new SimpleAES();
            string outputData = reader.ReadLine();
            reader.Close();
            exportValue.text = outputData;
            Debug.Log(aes.DecryptString(outputData, encryptKey));
            Debug.Log(outputData);


        }

    }
    

    public void ClearFields()
    {
        exportValue.text = "";
        importValue.text = "";

    }



}
*/