namespace WordAnalysis.ConsoleApplication
{
    public class Counter<T>
    {
        public T Key { get; set; }
        public int Count { get; set; }
        public string AsString { get; set; }

        public Counter(T key, int cnt)
        {
            Key = key;
            Count = cnt;
            AsString = key.ToString();
        }
    }
}