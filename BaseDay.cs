using System;
using System.IO;

namespace AOC2021 {

    class BaseDay {

        public virtual bool Debug { get; protected set; }

        public virtual void Solve() { }

        protected void Log(string s) {
            Console.WriteLine(string.Format("{0}{1} - {2}", Debug ? "[DEBUG]" : "", GetDay(), s));
        }

        protected string[] GetInput() {
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string text = Path.Combine(path, GetDay(), Debug ? "example.txt" : "input.txt");
            return File.ReadAllLines(text);
        }

        private string GetDay() {
            return GetType().Name;
        }
    }

}
