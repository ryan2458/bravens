namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Class1 c1 = new();
            Class1 c2 = new();

            c1.Name = "1";
            c2.Name = "2";
            List<Class1> classes = [c1, c2];

            classes.Remove(c2);

        }
    }


    public class Class1
    {
       public string Name { get; set; }
    }
}
