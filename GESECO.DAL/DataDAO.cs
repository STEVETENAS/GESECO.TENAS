﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESECO.DAL
{
    public class DataDAO<T>
    {
        private static List<T> datas;
        private readonly string FILE_NAME = $@"{typeof(T).Name}.json";
        private readonly string dbFolder;
        private readonly FileInfo file;


        public DataDAO(string dbFolder)
        {
            this.dbFolder = dbFolder;
            file = new FileInfo(Path.Combine(this.dbFolder, FILE_NAME));
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }

            if (!file.Exists)
            {
                file.Create().Close();
                file.Refresh();
            }

            if (file.Length > 0)
            {
                using (StreamReader sr = new StreamReader(file.FullName))
                {
                    string json = sr.ReadToEnd();
                    datas = JsonConvert.DeserializeObject<List<T>>(json);
                }
            }
            if (datas == null)
            {
                datas = new List<T>();
            }

        }

        public void Set(T oldData, T newData)
        {
            var oldIndex = datas.IndexOf(oldData);
            var newIndex = datas.IndexOf(newData);

            if (oldIndex < 0)
                throw new KeyNotFoundException($"{typeof(T).Name} reference doesn't exists !");

            if (newIndex > 0 && newIndex != oldIndex)
                throw new DuplicateNameException($"this {typeof(T).Name} already exists !");

            datas[oldIndex] = newData;
            Save();
        }

        public void Add(T data)
        {
            var index = datas.IndexOf(data);
            if (index >= 0)
                throw new DuplicateNameException($"{typeof(T).Name} reference already exist !");

            datas.Add(data);
            Save();
        }
        public void Remove(T data)
        {
            datas.Remove(data);
            Save();
        }

        public void Save()
        {
            using (StreamWriter sw = new StreamWriter(file.FullName, false))
            {
                string json = JsonConvert.SerializeObject(datas);
                sw.WriteLine("\n\n" + json);
            }
        }
        public void Save2()
        {
            using (StreamWriter sw = new StreamWriter($"{file.FullName} history", false))
            {
                string json = JsonConvert.SerializeObject(datas);
                sw.WriteLine("\n\n" + json);
            }
        }
        public void RemoveSave(T data)
        {
            datas.Add(data);
            Save2();
        }

        public int Count()
        {
            return datas.Count();
        }

        public IEnumerable<T> Find()
        {
            return new List<T>(datas);
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return new List<T>(datas.Where(predicate).ToArray());
        }
    }
}
