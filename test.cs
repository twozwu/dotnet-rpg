namespace dotnet_rpg
{
    public class test
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();

            list.Add(5);
            list.Add(4);
            list.Add(3);
            list.Add(2);
            list.Add(1);

            var list_where = list.Where(x => x == 1 || x == 2);
        }
    }
}
