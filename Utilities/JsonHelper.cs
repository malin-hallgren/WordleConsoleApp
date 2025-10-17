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

        public static List<T> LoadListFromPath<T>(string path)
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
                    SaveListToPath(path, listToLoadTo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"File {path} cannot be read: {ex.Message}\nOverwriting {path}...");
                SaveListToPath(path, listToLoadTo);
            }

            return listToLoadTo;
        }

        public static Dictionary<TKey, TValue> LoadDictFromPath<TKey, TValue>(string path)
        {
            Dictionary<TKey, TValue> dictToLoadTo = new Dictionary<TKey, TValue>();
            try
            {
                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    dictToLoadTo = System.Text.Json.JsonSerializer.Deserialize<Dictionary<TKey, TValue>>(json) ?? new Dictionary<TKey, TValue>();
                }
                else
                {
                    Console.WriteLine($"File {path} not found. Creating {path}...");
                    SaveDictToPath(path, dictToLoadTo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"File {path} cannot be read: {ex.Message}\nOverwriting {path}...");
                SaveDictToPath(path, dictToLoadTo);
            }

            return dictToLoadTo;
        }

        public static void SaveListToPath<T>(string path, List<T> listToSave)
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

        public static void SaveDictToPath<TKey, TValue>(string path, Dictionary<TKey, TValue> dictToSave)
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(dictToSave);
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file {path}: {ex.Message}");
            }
        }
    }
}
