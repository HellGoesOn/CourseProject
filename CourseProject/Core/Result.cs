namespace CourseProject.Core
{
    public struct Result<T> where T : class
    {
        public readonly T Data;

        public readonly bool Success;

        public readonly int Id;

        public Result(T data, bool success = true, int id = -1)
        {
            Data = data;
            Success = success;
            Id = id;
        }
    }
}
