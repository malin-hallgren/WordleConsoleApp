using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using WordleConsoleApp.User;

namespace WordleConsoleApp.Utilities
{
    internal static class JsonHelper
    {
        private static readonly JsonSerializerOptions Options;


        static JsonHelper()
        {
            var resolver = new DefaultJsonTypeInfoResolver();


            //Adds modifiers to the resolver based on types
            //if the type of data is BasicUser, add Polymorphism options 
            //with a type discriminator for the dervided types
            //if more types of BasicUser are added, remember to add them here
            resolver.Modifiers.Add ((JsonTypeInfo ti) =>
            {
                if(ti.Type == typeof(BasicUser))
                {
                    ti.PolymorphismOptions = new JsonPolymorphismOptions
                    {
                        TypeDiscriminatorPropertyName = $"type",
                        IgnoreUnrecognizedTypeDiscriminators = false,
                        DerivedTypes =
                        {
                            new JsonDerivedType(typeof(Manager), nameof(Manager)),
                            new JsonDerivedType(typeof(Player), nameof(Player))
                        }
                    };
                }
            });

            Options = new JsonSerializerOptions
            {
                WriteIndented = true,
                TypeInfoResolver = resolver
            };
        }

        /// <summary>
        /// Loads a file into a list. If the file doesn't exist or cannot be read, creates a new, empty list
        /// </summary>
        /// /// <remarks>If the file cannot be read due to an error, the method will overwrite the file with
        /// an empty dictionary.</remarks>
        /// <typeparam name="T">The type of data in the list</typeparam>
        /// <param name="path">file path to load from, if file doesn't exist, it will be created</param>
        /// <returns> <see cref= List{T}">"/>The loaded list, which may be a newly created empty list, to be assigned to list used by program</returns>
        public static List<T> LoadList<T>(string path)
        {
            List<T> listToLoadTo = new List<T>();
            try
            {
                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    if (listToLoadTo is List<BasicUser>)
                    {
                        listToLoadTo = JsonSerializer.Deserialize<List<T>>(json, Options) ?? new List<T>();
                    }
                    else
                    { 
                        listToLoadTo = JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
                    }
                }
                else
                {
                    Console.WriteLine($"File {path} not found. Creating {path}...");
                    SaveList(path, listToLoadTo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"File {path} cannot be read: {ex.Message}\nOverwriting {path}...");
                SaveList(path, listToLoadTo);
            }

            return listToLoadTo;
        }

        /// <summary>
        /// Loads a dictionary from a JSON file at the specified path. If the file does not exist or cannot be read, a
        /// new empty dictionary is created and saved to the specified path.
        /// </summary>
        /// <remarks>If the file cannot be read due to an error, the method will overwrite the file with
        /// an empty dictionary.</remarks>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="path">The file path from which to load the dictionary. If the file does not exist, it will be created.</param>
        /// <returns>A <see cref="Dictionary{TKey, TValue}"/> containing the deserialized data from the file, or an empty
        /// dictionary if the file does not exist or cannot be read.</returns>
        public static Dictionary<TKey, TValue> LoadDict<TKey, TValue>(string path)
        {
            Dictionary<TKey, TValue> dictToLoadTo = new Dictionary<TKey, TValue>();
            try
            {
                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    dictToLoadTo = JsonSerializer.Deserialize<Dictionary<TKey, TValue>>(json) ?? new Dictionary<TKey, TValue>();
                }
                else
                {
                    Console.WriteLine($"File {path} not found. Creating {path}...");
                    SaveDict(path, dictToLoadTo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"File {path} cannot be read: {ex.Message}\nOverwriting {path}...");
                SaveDict(path, dictToLoadTo);
            }

            return dictToLoadTo;
        }

        /// <summary>
        /// Saves the passed List to a file
        /// </summary>
        /// <typeparam name="T">The type of data in the List</typeparam>
        /// <param name="path">The file path to which data will be saved</param>
        /// <param name="listToSave">The List which will be saved</param>
        public static void SaveList<T>(string path, List<T> listToSave)
        {
            try
            {
                string json;

                if (listToSave is List<BasicUser>)
                {
                    json = JsonSerializer.Serialize(listToSave, Options);
                }
                else
                {
                    json = JsonSerializer.Serialize(listToSave);
                }

                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file {path}: {ex.Message}");
            }
        }

        /// <summary>
        /// Saves the passed Dictionary to a file
        /// </summary>
        /// <typeparam name="TKey">The type of the key of the Dictionary</typeparam>
        /// <typeparam name="TValue">The type of the value of the Dictionary</typeparam>
        /// <param name="path">The path which to save the Dictionary to</param>
        /// <param name="dictToSave">The Dictionary to save</param>
        public static void SaveDict<TKey, TValue>(string path, Dictionary<TKey, TValue> dictToSave)
        {
            try
            {
                string json = JsonSerializer.Serialize(dictToSave);
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file {path}: {ex.Message}");
            }
        }
    }
}
