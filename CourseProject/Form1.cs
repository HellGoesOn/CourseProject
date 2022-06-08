using CourseProject.Content.Models;
using CourseProject.Core;
using CourseProject.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace CourseProject
{
    public partial class MainForm : Form
    {
        public const string ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=TeacherWorkloadControl;";

        private string _userInfo = "Integrated Security = true;";
        public string UserInfo 
        {
            get => _userInfo;
            set
            {
                if (value != _userInfo)
                {
                    _userInfo = value;
                    Connection = new SqlConnection(ConnectionString + _userInfo);
                }
            }
        }

        public SqlConnection Connection { get; private set; }

        public static MainForm instance { get; private set; }

        /*
        public DataStorage<TeacherSubject> TeacherSubject;
        public DataStorage<Teacher> Teacher;
        */

        public DataStorage<Model> TeacherSubject;
        public DataStorage<Model> Teacher;
        public DataStorage<Model> Subject;
        public DataStorage<Model> Group;
        public DataStorage<Model> Schedule;
        public DataStorage<Model> ClassType;

        public Type CurrentStorage;

        public Dictionary<Type, DataStorage<Model>> Storages;

        public Model CurrentModel { get; set; }
        public Type lastKnownType;
        public int currentId;

        public Dictionary<Type, PropertyInfo[]> Infos = new Dictionary<Type, PropertyInfo[]>();

        public MainForm()
        {
            instance = this;

            InitializeComponent();
            MainGrid.Columns.Clear();

            Storages = new Dictionary<Type, DataStorage<Model>>();

            // TO-DO: Пернуть запросом в БД и получить инфу оттуда

            Connection = new SqlConnection(ConnectionString + UserInfo);

            TeacherSubject = new DataStorage<Model>(Connection, typeof(TeacherSubject));
            Teacher = new DataStorage<Model>(Connection, typeof(Teacher));
            Subject = new DataStorage<Model>(Connection, typeof(Subject));
            Schedule = new DataStorage<Model>(Connection, typeof(Schedule));
            Group = new DataStorage<Model>(Connection, typeof(Group));
            ClassType = new DataStorage<Model>(Connection, typeof(ClassType));


            Storages.Add(typeof(TeacherSubject), TeacherSubject);
            Storages.Add(typeof(Teacher), Teacher);
            Storages.Add(typeof(Subject), Subject);
            Storages.Add(typeof(Schedule), Schedule);
            Storages.Add(typeof(Group), Group);
            Storages.Add(typeof(ClassType), ClassType);

            foreach (var t in Storages.Keys)
            {
                Infos.Add(t, t.GetProperties());
            }

            FuckTabControlMyHomiesHateTabControl();

            PullDatas();

            ShowDataFor(Teacher);
            lastKnownType = typeof(Teacher);
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
                NameAttribute attr = kvp.Value.expectedType.GetCustomAttribute<NameAttribute>();

                if (attr != null)
                {
                    TabPage pg = new TabPage(attr.Name)
                    {
                        Name = kvp.Key.Name
                    };
                    tableControl.TabPages.Add(pg);
                }
            }

            tableControl.Selected += TableControl_Selected;
            tableControl.SelectedIndex = 1;
        }

        private void TableControl_Selected(object sender, TabControlEventArgs e)
        {
            TabControl control = (TabControl)sender;

            int index = control.SelectedIndex;
            string name = control.TabPages[index].Name;

            Type t = Assembly.GetAssembly(typeof(Model)).GetTypes().Where(x => x.Name == name).First();

            ShowDataFor(Storages[t]);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }


        private void PullDataFor<T>(DataStorage<T> storage)
            where T : Model
        {
            Connection.Open();

            bool needsBullshitWorkaround = storage.expectedType.GetCustomAttribute(typeof(NotKeywordAttribute)) != null;

            string typeName = storage.expectedType.Name;
            string tableName = needsBullshitWorkaround ? "[" + typeName + "]" : typeName;


            string query = @"select * from " + tableName;
            SqlCommand c = new SqlCommand(query, Connection);
            SqlDataReader reader = c.ExecuteReader();

            while (reader.Read())
            {
                var data = (T)Activator.CreateInstance(storage.expectedType);

                PropertyInfo[] infos = Infos[storage.expectedType];

                int num = 0;
                foreach (PropertyInfo f in infos.Where(x => x.GetCustomAttribute(typeof(NameAttribute)) != null))
                {
                    var d = reader[num];
                    ref var obj = ref data;
                    f.SetValue(obj, d);
                    num++;
                }

                data.id = (int)infos[0].GetValue(data);

                storage.Add(data);
            }

            Connection.Close();
            reader.Close();
        }

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

        private void RefreshDisplay()
        {
            MainGrid.Columns.Clear();
            ShowDataFor(Storages[CurrentStorage]);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void deleteSelected_Click(object sender, EventArgs e)
        {
            ConfirmDeletionForm f = new ConfirmDeletionForm();

            f.ShowDialog(this);

            if (f.DialogResult == DialogResult.OK)
            {
                for(int i = MainGrid.SelectedCells.Count-1; i >= 0; i--)
                {
                    int rowIndex = MainGrid.SelectedCells[i].RowIndex;
                    int id = (int)MainGrid[0, rowIndex].Value;

                    PropertyInfo[] infos = Infos[Storages[CurrentStorage].expectedType];

                    var get = Storages[CurrentStorage].Find(x => (int)infos[0].GetValue(x) == id);
                    get.ToBeRemoved = true;
                    MainGrid.Rows.RemoveAt(MainGrid.SelectedCells[i].RowIndex);
                    MainGrid.Update();

                    KillContextPanel();
                }

                foreach (var kvp in Storages)
                {
                    kvp.Value.Fixate();
                }

                ClearDatas();
                PullDatas();
            }
        }

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
        }

        private void MainForm_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void MainGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = -999;
            if (MainGrid.RowCount > 0)
            {
                object val = MainGrid[0, MainGrid.SelectedCells[0].RowIndex].Value;
                currentId = MainGrid.SelectedCells[0].RowIndex;
                id = val != null ? (int)val : -999;
            }

            CurrentModel = Storages[CurrentStorage].Find(x => x.id == id);

            MakeContextPanel();

        }

        private void addEntry_Click(object sender, EventArgs e)
        {
            if (contextPanel.Controls.Count <= 0)
            {
                MakeContextPanel();
                return;
            }

            if(CurrentModel != null && Storages[CurrentModel.GetType()].Find(x => x == CurrentModel) != null)
            {
                MessageBox.Show("Данная запись уже существует");
                return;
            }
            else
            {
                Storages[lastKnownType].Add(GetModel(ModelStyle.New));
                RefreshDisplay();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
             foreach(DataGridViewRow row in MainGrid.Rows)
            {
                row.Visible = true;

                if (row.Cells[0].Value == null)
                    continue;

                var model = Storages[CurrentStorage].GetAll().FirstOrDefault(x => x.id == (int)row.Cells[0].Value);

                if (model == null)
                    continue;

                string text = model.ToString().ToLower();

                if (!text.Contains(filterBox.Text.ToLower()))
                    row.Visible = false;
            }
        }
    }
}
