using CourseProject.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CourseProject.Core
{
    public class DataStorage<T> where T : Model
    {
        private readonly SqlConnection sqlConnection;

        private readonly List<T> data;

        public readonly Type expectedType;

        public DataStorage(SqlConnection cnn)
        {
            data = new List<T>();
            sqlConnection = cnn;
            expectedType = typeof(T);
        }

        public DataStorage(SqlConnection cnn, Type typeOf) : this(cnn)
        {
            expectedType = typeOf;
        }

        public Result<T> Add(T newData)
        {
            data.Add(newData);

            return new Result<T>(data[data.Count - 1]);
        }

        public Result<T> Replace(int id, T d)
        {
            if (id >= data.Count || id < 0)
                return new Result<T>(null, false);

            if (data.Count >= id)
            {
                data.RemoveAt(id);

                data.Insert(id, d);
            }
            else
            {
                Add(d);
            }

            return new Result<T>(d);
        }

        public Result<T> Remove(T dataToRemove)
        {
            if (data.Contains(dataToRemove))
            {
                data.Remove(dataToRemove);
                return new Result<T>(dataToRemove);
            }

            return new Result<T>(dataToRemove, false);
        }

        public Result<T> RemoveAt(int index)
        {
            if(data.Count >= index)
            {
                var r = new Result<T>(data[index]);
                data.RemoveAt(index);
                return r;
            }

            return new Result<T>(null, false, -1);
        }

        public Result<T> Insert(T dataToInsert, int index)
        {
            data.Insert(index, dataToInsert);

            return new Result<T>(dataToInsert, true, data.IndexOf(dataToInsert));
        }

        public T Find(Predicate<T> p)
        {
            return data.Find(p);
        }

        public Result<T> Get(int index)
        {
            if(index <= data.Count)
            {
                return new Result<T>(data[index], true, index);
            }

            return new Result<T>(null, false);
        }

        public int Count => data.Count;

        public IEnumerable<T> GetAll() => data;

        public void Fixate()
        {
            sqlConnection.Open();

            const int magicNumber = 2;
            // Updates
            PropertyInfo[] infos = expectedType.GetProperties().Where(x => x.GetCustomAttribute(typeof(NameAttribute)) != null).ToArray();

            StringBuilder query = new StringBuilder();

            bool needsBullshitWorkaround = expectedType.GetCustomAttribute(typeof(NotKeywordAttribute)) != null;

            string typeName = expectedType.Name;
            string tableName = needsBullshitWorkaround ? "[" + typeName + "]" : typeName;

            foreach (var item in data)
            {
                if (item.WasUpdated)
                {
                    query = new StringBuilder(@"update " + tableName + " set ");

                    int dumbCounter = 0;

                    foreach(var info in infos)
                    {
                        if (info.Name == "Id")
                            continue;

                        var val = info.GetValue(item);

                        if(info.PropertyType == typeof(string) || info.PropertyType == typeof(DateTime))
                            query.AppendLine($@" {info.Name} = N'{Convert.ChangeType(val, info.PropertyType)}'");
                        else
                            query.AppendLine($@" {info.Name} = {Convert.ChangeType(val, info.PropertyType)}");

                        query.AppendLine($"{(dumbCounter < infos.Length- magicNumber ? "," : "")}");

                        dumbCounter++;
                    }

                    query.AppendLine($@" where {infos[0].Name} = {infos[0].GetValue(item)};");
                }
                else if(item.IsNew)
                {
                    query = new StringBuilder(@"insert into " + tableName + " (");

                    int dumbCounter = 0;
                    foreach (var info in infos)
                    {
                        if (info.Name == "Id")
                            continue;

                        var val = info.Name;

                        query.AppendLine($"{val} {(dumbCounter < infos.Length- magicNumber ? "," : "")}");

                        dumbCounter++;
                    }

                    query.Append($") \nVALUES(");

                    dumbCounter = 0;

                    foreach (var info in infos)
                    {
                        if (info.Name == "Id")
                            continue;

                        var val = info.GetValue(item);

                        if (info.PropertyType == typeof(string) || info.PropertyType == typeof(DateTime))
                            query.AppendLine($@"N'{Convert.ChangeType(val, info.PropertyType)}'");
                        else
                            query.AppendLine($@"{Convert.ChangeType(val, info.PropertyType)}");

                        query.AppendLine($"{(dumbCounter < infos.Length- magicNumber ? "," : "")}");

                        dumbCounter++;
                    }

                    query.AppendLine(@");");
                }
                else if(item.ToBeRemoved)
                {
                    int id = (int)infos[0].GetValue(item);

                    query = new StringBuilder($"delete from {tableName} where {infos[0].Name} = {id}");
                }

                if (!string.IsNullOrEmpty(query.ToString()))
                {
                    try
                    {
                        SqlCommand sqlCommand = new SqlCommand(query.ToString(), sqlConnection);
                        sqlCommand.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        ErrorForm err = new ErrorForm();
                        err.SetText(ex.Message);
                        err.ShowDialog(MainForm.instance);
                    }
                }
            }

            sqlConnection.Close();
        }

        public void Clear()
        {
            data.Clear();
        }
    }
}
