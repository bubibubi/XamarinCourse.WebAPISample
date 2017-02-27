using System;

namespace XamarinWebAPISample
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", Id, Name);
        }
    }
}
