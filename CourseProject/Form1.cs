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

        public Type CurrentStorage;

        public Dictionary<Type, DataStorage<Model>> Storages;

        public Model CurrentModel { get; set; }
        public Type lastKnownType;
        public int currentId;

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
            List<T> omegaLUL = (List<T>)storage.GetAll().Where(x => !x.ToBeRemoved).ToList();

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

                PropertyInfo[] infos = storage.expectedType.GetProperties();

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

        private void updateButton_Click(object sender, EventArgs e)
        {
            var st = Storages[CurrentStorage];

            if(CurrentModel == null)
            {
                MessageBox.Show("Не выбранны данные для изменения");
                return;
            }
            try
            {
                object[] parameters = new object[CurrentModel.GetType().GetProperties().Where(x => x.GetCustomAttribute<NameAttribute>() != null).ToList().Count];

                int fuck = parameters.Length - 1;

                for (int i = contextPanel.Controls.Count - 1; i >= 0; i--)
                {
                    if (!(contextPanel.Controls[i] is TextBox))
                        break;

                    parameters[fuck] = (object)contextPanel.Controls[i].Text;
                    fuck--;
                }

                int id = parameters[0] == null ? -1 : (int)Convert.ChangeType(parameters[0], typeof(int));

                if (st.Find(x => x.id == CurrentModel.id && !x.ToBeRemoved) != null)
                    st.Replace(currentId, Model.UpdateModel(CurrentStorage, parameters));

                RefreshDisplay();
            }
            catch
            {

            }
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

                    PropertyInfo[] info = Storages[CurrentStorage].expectedType.GetProperties();

                    var get = Storages[CurrentStorage].Find(x => (int)info[0].GetValue(x) == id);
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
            object val = MainGrid[0, MainGrid.SelectedCells[0].RowIndex].Value;
            int id = val != null ? (int)val : -999;

            CurrentModel = Storages[CurrentStorage].Find(x => x.id == id);
            currentId = MainGrid.SelectedCells[0].RowIndex;

            MakeContextPanel();

        }

        private void MakeContextPanel()
        {
            PropertyInfo[] fields = Storages[CurrentStorage].expectedType.GetProperties();
            KillContextPanel();

            int topOffset = 0;
            int longest = 0;

            foreach (var field in fields)
            {
                var attr = field.GetCustomAttribute<NameAttribute>();
                if (attr == null)
                    continue;

                var txt = new Label();
                txt.Text = attr.Name;
                var measured = TextRenderer.MeasureText(txt.Text, txt.Font).Width;
                txt.Width = measured;
                txt.Top = topOffset;
                contextPanel.Controls.Add(txt);

                topOffset += txt.Height + 2;
                if (measured > longest)
                    longest = measured;
            }

            topOffset = 0;

            int foo = 0;

            foreach (var field in fields)
            {
                if (field.GetCustomAttribute<NameAttribute>() == null)
                    continue;

                var txt = new TextBox();
                txt.Width = 380;
                txt.Name = "txt" + foo;
                if (CurrentModel != null)
                    txt.Text = field.GetValue(CurrentModel).ToString();

                txt.Left = longest;
                txt.Top = topOffset;
                contextPanel.Controls.Add(txt);

                topOffset += txt.Height + 2;
            }

            lastKnownType = CurrentStorage;
        }

        private void KillContextPanel()
        {
            foreach (Control v in contextPanel.Controls)
            {
                v.Dispose();
            }

            contextPanel.Controls.Clear();
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
                object[] parameters = new object[lastKnownType.GetProperties().Where(x => x.GetCustomAttribute<NameAttribute>() != null).ToList().Count];

                int fuck = parameters.Length - 1;

                for (int i = contextPanel.Controls.Count - 1; i >= 0; i--)
                {
                    if (!(contextPanel.Controls[i] is TextBox))
                        break;

                    parameters[fuck] = (object)contextPanel.Controls[i].Text;
                    fuck--;
                }


                Storages[lastKnownType].Add(Model.CreateNewModel(lastKnownType, parameters));
                RefreshDisplay();
            }
        }
    }
}
