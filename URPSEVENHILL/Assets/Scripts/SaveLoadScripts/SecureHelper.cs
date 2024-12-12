using System.Collections;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;
using System;

public static class SecureHelper 
{
    public static string Hash(string data)
    {
        byte[] textToBytes = Encoding.UTF8.GetBytes(data);
        SHA256Managed mySha256 = new SHA256Managed();

        byte[] hashValue = mySha256.ComputeHash(textToBytes);

        return GetHexStringFromHash(hashValue); 
    }

    private static string GetHexStringFromHash(byte[] hash)
    {
        string hexString = String.Empty;

        foreach(byte b in hash)
            hexString += b.ToString("x2");

        return hexString;    
    }

    public static string EncryptDecrypt(string data, int key)
    {
        StringBuilder input = new StringBuilder(data);
        StringBuilder output = new StringBuilder(data.Length);

        char character;

        for(int i=0; i<data.Length; i++)
        {
            character=input[i];
            character = (char)(character^key);
            output.Append(character);
        }

        return output.ToString();
    }
    
}
