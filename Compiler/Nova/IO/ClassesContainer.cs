using Nova.Members;
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

        public ClassesContainer()
        {
            this.Elements = new Dictionary<string, Class>();
        }

        public void Add(Class element)
        {
            Elements.Add(element.ClassName, element);
        }
        public void AddRange(Dictionary<string, Class> elements)
        {
            foreach (var element in elements)
            {
                Elements.Add(element.Key, element.Value);
            }
        }
        public void AddRange(IEnumerable<Class> elements)
        {
            foreach (var element in elements)
            {
                Elements.Add(element.ClassName, element);
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
