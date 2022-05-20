using CourseProject.Content.Models;
using CourseProject.Core;
using CourseProject.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace CourseProject
{
    public partial class Form1 : Form
    {
        public const string ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=TeacherWorkloadControl;Integrated Security=True;";

        public SqlConnection Connection { get; private set; }

        public static Form1 instance { get; private set; }

        /*
        public DataStorage<TeacherSubject> TeacherSubject;
        public DataStorage<Teacher> Teacher;
        */

        public DataStorage<Model> TeacherSubject;
        public DataStorage<Model> Teacher;
        public DataStorage<Model> Subject;
        public DataStorage<Model> Group;
        public DataStorage<Model> Schedule;

        public Type CurrentStorage;

        public Dictionary<Type, DataStorage<Model>> Storages;

        public Form1()
        {
            instance = this;

            InitializeComponent();
            MainGrid.Columns.Clear();

            Storages = new Dictionary<Type, DataStorage<Model>>();

            // TO-DO: Пернуть запросом в БД и получить инфу оттуда

            Connection = new SqlConnection(ConnectionString);

            TeacherSubject = new DataStorage<Model>(Connection, typeof(TeacherSubject));
            Teacher = new DataStorage<Model>(Connection, typeof(Teacher));
            Subject = new DataStorage<Model>(Connection, typeof(Subject));
            Schedule = new DataStorage<Model>(Connection, typeof(Schedule));
            Group = new DataStorage<Model>(Connection, typeof(Group));

            Storages.Add(typeof(TeacherSubject), TeacherSubject);
            Storages.Add(typeof(Teacher), Teacher);
            Storages.Add(typeof(Subject), Subject);
            Storages.Add(typeof(Schedule), Schedule);
            Storages.Add(typeof(Group), Group);

            FuckTabControlMyHomiesHateTabControl();

            PullDatas();

            ShowDataFor(Teacher);
        }

        private void PullDatas()
        {
            foreach(var storage in Storages.Values)
            {
                PullDataFor(storage);
            }
        }

        private void ClearDatas()
        {
            foreach (var storage in Storages.Values)
                storage.Clear();
        }

        private void FuckTabControlMyHomiesHateTabControl()
        {
            tableControl.TabPages.Clear();

            foreach (var kvp in Storages)
            {
                tableControl.TabPages.Add(kvp.Value.expectedType.Name);
            }

            tableControl.Selected += TableControl_Selected;
            tableControl.SelectedIndex = 1;
        }

        private void TableControl_Selected(object sender, TabControlEventArgs e)
        {
            TabControl control = (TabControl)sender;

            int index = control.SelectedIndex;
            string name = control.TabPages[index].Text;

            Type t = Assembly.GetAssembly(typeof(Model)).GetTypes().Where(x => x.Name == name).First();

            ShowDataFor(Storages[t]);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void ShowDataFor<T>(DataStorage<T> storage)
            where T : Model
        {
            MainGrid.Columns.Clear();
            MainGrid.ClearSelection();

            PropertyInfo[] infos = storage.expectedType.GetProperties();
            List<T> omegaLUL = (List<T>)storage.GetAll();

            int columnId = 0;

            foreach (PropertyInfo f in infos)
            {
                NameAttribute attr = (NameAttribute)f.GetCustomAttribute(typeof(NameAttribute));

                if (attr == null)
                    continue;

                MainGrid.Columns.Add(attr.Name.ToLower(), attr.Name);

                for (int i = 0; i < omegaLUL.Count; i++)
                {
                    if (MainGrid.RowCount < omegaLUL.Count + 1)
                        MainGrid.RowCount++;

                    var value = f.GetValue(omegaLUL[i]);

                    MainGrid[columnId, i].Value = value == null ? "Bruh" : value;
                }

                columnId++;
            }

            CurrentStorage = storage.expectedType;
        }

        /*private void ShowDataFor<T>(DataStorage<T> storage)
            where T : Model
        {
            MainGrid.Columns.Clear();
            MainGrid.ClearSelection();

            PropertyInfo[] infos = typeof(T).GetProperties();
            List<T> omegaLUL = (List<T>)storage.GetAll();

            int columnId = 0;

            foreach (PropertyInfo f in infos)
            {
                NameAttribute attr = (NameAttribute)f.GetCustomAttribute(typeof(NameAttribute));

                if (attr == null)
                    continue;

                MainGrid.Columns.Add(attr.Name.ToLower(), attr.Name);

                for (int i = 0; i < omegaLUL.Count; i++)
                {
                    if (MainGrid.RowCount < omegaLUL.Count + 1)
                        MainGrid.RowCount++;

                    var value = f.GetValue(omegaLUL[i]);

                    MainGrid[columnId, i].Value = value == null ? "Bruh": value;
                }

                columnId++;
            }

            CurrentStorage = typeof(T);
        }*/

        private void PullDataFor<T>(DataStorage<T> storage)
            where T : Model
        {
            Connection.Open();

            bool needsBullshitWorkaround = storage.expectedType.GetCustomAttribute(typeof(NotKeywordAttribute)) != null;

            string typeName = storage.expectedType.Name;
            string tableName = needsBullshitWorkaround ? "["+typeName+"]":typeName;


            string query = @"select * from " + tableName;
            SqlCommand c = new SqlCommand(query, Connection);
            SqlDataReader reader = c.ExecuteReader();

            while (reader.Read())
            {
                var data = (T)Activator.CreateInstance(storage.expectedType);

                PropertyInfo[] infos = storage.expectedType.GetProperties();

                int num = 0;
                foreach (PropertyInfo f in infos.Where(x => x.GetCustomAttribute(typeof(NameAttribute)) != null))
                {
                    var d = reader[num];
                    ref var obj = ref data;
                    f.SetValue(obj, d);
                    num++;
                }

                storage.Add(data);
            }

            Connection.Close();
            reader.Close();
        }

        /*private void PullDataFor<T>(DataStorage<T> storage)
            where T : Model, new()
        {
            SqlCommand c = new SqlCommand(@"select * from " + typeof(T).Name, Connection);
            Connection.Open();

            SqlDataReader reader = c.ExecuteReader();

            while (reader.Read())
            {
                var data = new T();
                PropertyInfo[] infos = typeof(T).GetProperties();

                int num = 0;
                foreach(PropertyInfo f in infos.Where(x =>x.GetCustomAttribute(typeof(NameAttribute)) != null))
                {
                    var d = reader[num];
                    ref var obj = ref data;
                    f.SetValue(obj, d);
                    num++;
                }

                storage.Add(data);
            }

            reader.Close();
        }*/

        private void button1_Click(object sender, System.EventArgs e)
        {
            ShowDataFor(Teacher);
        }

        private void showTeacherSubject_Click(object sender, System.EventArgs e)
        {
            ShowDataFor(TeacherSubject);
        }

        private void commitChanges_Click(object sender, System.EventArgs e)
        {
            // to-do: пернуть запросов в БД, вкинув все изменения сделанные в приложении

            foreach (var kvp in Storages)
            {
                kvp.Value.Fixate();
            }

            ClearDatas();
            PullDatas();

            ShowDataFor(Storages[CurrentStorage]);
        }

        // мессиво из кала
        private void MainGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            var st = Storages[CurrentStorage];
            for (int index = 0; index < MainGrid.RowCount-1; index++)
            {
                int cellId = index;

                object[] parameters = new object[MainGrid.ColumnCount];

                for (int i = 0; i < MainGrid.ColumnCount; i++)
                {
                    parameters[i] = MainGrid[i, cellId].Value;
                }

                if(st.Count >= index+1)
                    st.Replace(index, Model.UpdateModel(CurrentStorage, parameters));
                else
                    st.Add(Model.CreateNewModel(CurrentStorage, parameters));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
