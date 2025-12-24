using Microsoft.Win32;
using SportsClub_Buisness;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp11
{
    internal class clsGlobal
    {
        public static clsUser CurrentUser;

        public static bool RememberUsernameAndPassword(string Username, string Password)
        {

            try
            {
                //this will get the current project directory folder.
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();


                // Define the path to the text file where you want to save the data
                string filePath = currentDirectory + "\\data.txtFile";

                //incase the username is empty, delete the file
                if (Username == "" && File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;

                }

                // concatonate username and passwrod withe seperator.
                string dataToSave = Username + "#//#" + Password;

                // Create a StreamWriter to write to the file
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write the data to the file
                    writer.WriteLine(dataToSave);

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            //this will get the stored username and password and will return true if found and false if not found.
            try
            {
                //gets the current project's directory
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();

                // Path for the file that contains the credential.
                string filePath = currentDirectory + "\\data.txt";

                // Check if the file exists before attempting to read it
                if (File.Exists(filePath))
                {
                    // Create a StreamReader to read from the file
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        // Read data line by line until the end of the file
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(line); // Output each line of data to the console
                            string[] result = line.Split(new string[] { "#//#" }, StringSplitOptions.None);

                            Username = result[0];
                            Password = result[1];
                        }
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public static bool RememberMeUserNameAndPassword(string Username, string Password)
        {


            string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";


            Registry.SetValue(KeyPath, "UserName", Username, RegistryValueKind.String);
            Registry.SetValue(KeyPath, "Password", Password, RegistryValueKind.String);

            return true;

        }

        public static void DeleteUserNameAndPassword()
        {
            string keyPath = @"SOFTWARE\DVLD";
            string valueName = "UserName";
            string valuePassword = "Password";


            try
            {
                // Open the registry key in read/write mode with explicit registry view
                using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                {
                    using (RegistryKey key = baseKey.OpenSubKey(keyPath, true))
                    {
                        if (key != null)
                        {
                            // Delete the specified value
                            key.DeleteValue(valueName);
                            key.DeleteValue(valuePassword);


                            Console.WriteLine($"Successfully deleted value '{valueName}' from registry key '{keyPath}'");
                        }
                        else
                        {
                            Console.WriteLine($"Registry key '{keyPath}' not found");
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("UnauthorizedAccessException: Run the program with administrative privileges.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        //public static bool GetUserNameAndPassword(ref string Username, ref string Password)
        //{
        //    string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";

        //    Username = Registry.GetValue(KeyPath, "UserName", null) as string;
        //    Password = Registry.GetValue(KeyPath, "Password", null) as string;

        //    if (Username == null && Password == null)
        //        return false;
        //    else
        //        return true;
        //}
    }
}
