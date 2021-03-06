﻿using Nova.Bytecode.IO;
using Nova.Members;
using Nova.Types;
using Nova.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.IO
{
    public class ClassesContainer : IEnumerable<KeyValuePair<string, Class>>
    {
        public int Count
        {
            get
            {
                return Elements.Count;
            }
        }

        private Dictionary<string, Class> Elements
        {
            get;
            set;
        }
        private Dictionary<string, int> Relator
        {
            get;
            set;
        }
        private int RelatorId
        {
            get;
            set;
        }
        public TypeManager TypeManager
        {
            get;
            set;
        }
        public ClassesContainer()
        {
            this.Elements = new Dictionary<string, Class>();
            this.Relator = new Dictionary<string, int>();
            this.RelatorId = 0;
            this.TypeManager = new TypeManager();
        }
        public int GetClassId(Class @class)
        {
            return GetClassId(@class.ClassName);
        }
        public int GetClassId(string className)
        {
            return Relator[className];
        }

        public void Add(Class element)
        {
            Elements.Add(element.ClassName, element);
            Relator.Add(element.ClassName, RelatorId++);
        }
        public void AddRange(Dictionary<string, Class> elements)
        {
            foreach (var element in elements)
            {
                Add(element.Value);
            }
        }
        public void AddRange(IEnumerable<Class> elements)
        {
            foreach (var element in elements)
            {
                Add(element);
            }
        }
        public bool ContainsClass(string @class)
        {
            return Elements.ContainsKey(@class);
        }
        public void Concat(ClassesContainer container)
        {
            AddRange(container.Elements);
        }

        public Method ComputeEntryPoint()
        {
            Method result = null;

            foreach (var @class in GetClasses())
            {
                foreach (var method in @class.Methods)
                {
                    if (method.Value.IsMainPointEntry())
                    {
                        if (result != null)
                        {
                            return null;
                        }

                        result = method.Value;
                    }
                }
            }
          
            return result;
        }

        public IEnumerable<Class> GetClasses()
        {
            return Elements.Values;
        }
        public Class TryGetClass(string key)
        {
            Class result = null;
            Elements.TryGetValue(key, out result);
            return result;
        }
        public IEnumerator<KeyValuePair<string, Class>> GetEnumerator()
        {
            return Elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Elements.GetEnumerator();
        }

        public Class this[string className]
        {
            get
            {
                return Elements[className];
            }
        }

    }
}
