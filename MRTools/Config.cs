using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Text.RegularExpressions;

namespace MRTools
{
    public class Config
    {
        public string inipath;
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string ShortcutKey, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string ShortcutKey, string def, StringBuilder retVal, int size, string filePath);

        public Config(string INIPath)
        {
            inipath = INIPath;
        }
        public void iniWriteValue(string Section, string Key, string Value)
        {
            try
            {
                WritePrivateProfileString(Section, Key, Value, this.inipath);
            }
            catch (IOException ex)
            {
                utility.logWriter.writeErrorLog(ex);
            }
        }
        public string iniReadValue(string Section, string Key)
        {
            try
            {
                StringBuilder temp = new StringBuilder(500);
                int i = GetPrivateProfileString(Section, Key, "", temp, 500, this.inipath);
                return temp.ToString();
            }
            catch (IOException ex)
            {
                utility.logWriter.writeErrorLog(ex);
                return "";
            }
        }
        public void deleteKey(string Section, string Ident)
        {
            WritePrivateProfileString(Section, Ident, null, this.inipath);
        }
        public void deleteSection(string section)
        {
            WritePrivateProfileString(section, null, null, this.inipath);
        }
        public void updateKeyValue_AppendSubStr(string section, string key, string value,string splitSymbol)
        {
            string temp = iniReadValue(section, key);
            temp = updateSTR_AppendSubStr(temp, splitSymbol, value);
            iniWriteValue(section, key, temp);
        }
        public void updateKeyValue_ReplaceSubStr(string section, string key, string pattern, string substitution, string splitSymbol)
        {
            string temp = iniReadValue(section, key);
            temp = updateSTR_ReplaceSubStr(temp, splitSymbol, pattern, substitution);
            iniWriteValue(section, key, temp);
        }
        public bool ExistINIFile()
        {
            return File.Exists(inipath);
        }
        public string updateSTR_ReplaceSubStr(string str, string splitSymbol, string pattern, string substitution)
        {
            try
            {
                string[] contents = Regex.Split(str, splitSymbol);
                string temp = "";
                int mark = 0;
                if (contents[0] == pattern)
                {
                    temp = substitution;
                }
                else
                {
                    temp = contents[0];
                }
                for (int i = 1; i < contents.Length; i++)
                {
                    if (contents[i] == pattern)
                    {
                        if (substitution != "")
                        {
                            temp += splitSymbol + substitution;
                        }
                        mark = i + 1;
                        break;
                    }
                    else
                    {
                        if (temp != "")
                        {
                            temp += splitSymbol + contents[i];
                        }
                        else
                        {
                            temp = contents[i];
                        }
                    }
                }
                if (mark != 0)
                {
                    for (int i = mark; i < contents.Length; i++)
                    {
                        temp += splitSymbol + contents[i];
                    }
                }
                return temp;
            }
            catch (Exception ex)
            {
                utility.logWriter.writeErrorLog(ex);
                return str;
            }
        }
        public string updateSTR_AppendSubStr(string str, string splitSymbol, string subStr)
        {
            if (subStr != "")
            {
                if (str == "")
                {
                    str = subStr;
                }
                else
                {
                    str += splitSymbol + subStr;
                }
            }
            return str;
        }
    }
}
