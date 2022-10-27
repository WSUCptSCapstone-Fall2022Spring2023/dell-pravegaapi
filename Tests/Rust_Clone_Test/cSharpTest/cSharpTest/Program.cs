
 namespace My.Company
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Serializable]
    public class InnerTestingClass
    {
        public int attribute1;
        public string attribute2;

        public InnerTestingClass()
        {
            this.attribute1 = 2;
            this.attribute2 = "I am on the inside of a class";
        }

        public int Attribute1
        {
            get { return this.attribute1; }
            set { this.attribute1 = value; }
        }

        public string Attribute2
        {
            get { return this.attribute2; }
            set { this.attribute2 = value; }
        }
    }

    [Serializable]
    public class TestingClass
    {
        public string attribute1;
        public int attribute2;
        public double attribute3;
        public InnerTestingClass attribute4;

        public TestingClass()
        {
            this.attribute1 = "I am the outer class";
            this.attribute2 = 3;
            this.attribute3 = 1.0;
            this.attribute4 = new InnerTestingClass();
        }

        public string Attribute1
        {
            get { return this.attribute1;}
            set { this.attribute1 = value; }
        }
        public int Attribute2
        {
            get { return this.attribute2;}
            set { this.attribute2 = value; }
        }
        public double Attribute3
        {
            get { return this.attribute3;}
            set { this.attribute3 = value; }
        }
        public InnerTestingClass Attribute4
        {
            get { return this.attribute4;}
            set { this.attribute4 = value; }
        }
    }

    /// <summary>
    ///  Tests deep clone feature
    /// </summary>
    public static class Program
    {
        static void Main()
        {
            TestingClass objectOne = new TestingClass();
            objectOne.attribute1 = "I am different from the default";
            objectOne.attribute2 = 1000;
            objectOne.attribute3 = 1000.0;
            objectOne.attribute4.attribute1 = 1000;
            objectOne.attribute4.attribute2 = "I am different from the inner class default";
            TestingClass? objectTwo = DeepClone.Clone(objectOne);

            if (objectTwo != null)
            {
                Console.Write("Test deep clone:" + Environment.NewLine +
                "Attribute 1 transfer: Values are equal ->" + (objectOne.attribute1 == objectTwo.attribute1).ToString() + Environment.NewLine +
                "Attribute 2 transfer: Values are equal ->" + (objectOne.attribute2 == objectTwo.attribute2).ToString() + Environment.NewLine +
                "Attribute 3 transfer: Values are equal ->" + (objectOne.attribute3 == objectTwo.attribute3).ToString() + Environment.NewLine +
                "Attribute 4 transfer: Value 1 are equal ->" + (objectOne.attribute4.attribute1 == objectTwo.attribute4.attribute1).ToString() + " , Objects aren't equal ->" + (!objectOne.attribute4.Equals(objectTwo.attribute4)).ToString() + Environment.NewLine);

                Console.Write(Environment.NewLine + "Output values:" + Environment.NewLine);
                Console.Write("Object One:" + Environment.NewLine
                    + "Attr 1  : " + objectOne.attribute1 + Environment.NewLine +
                    "Attr 2  : " + objectOne.attribute2.ToString() + Environment.NewLine +
                    "Attr 3  : " + objectOne.attribute3.ToString() + Environment.NewLine +
                    "Attr 4.1: " + objectOne.attribute4.attribute1.ToString() + Environment.NewLine +
                    "Attr 4.2: " + objectOne.attribute4.attribute2 + Environment.NewLine);
                Console.Write("Object Two:" + Environment.NewLine
                    + "Attr 1  : " + objectTwo.attribute1 + Environment.NewLine +
                    "Attr 2  : " + objectTwo.attribute2.ToString() + Environment.NewLine +
                    "Attr 3  : " + objectTwo.attribute3.ToString() + Environment.NewLine +
                    "Attr 4.1: " + objectTwo.attribute4.attribute1.ToString() + Environment.NewLine +
                    "Attr 4.2: " + objectTwo.attribute4.attribute2 + Environment.NewLine);
            }          
        }
    }
}