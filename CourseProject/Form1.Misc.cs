using CourseProject.Core;
using CourseProject.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace CourseProject
{
    public partial class MainForm : Form
    {
        private void MakeContextPanel()
        {
            PropertyInfo[] fields = Infos[CurrentStorage];
            KillContextPanel();

            int topOffset = 0;
            int longest = 0;

            foreach (var field in fields)
            {
                var attr = field.GetCustomAttribute<NameAttribute>();
                if (attr == null)
                    continue;

                var txt = new Label
                {
                    Text = attr.Name
                };
                var measured = TextRenderer.MeasureText(txt.Text, txt.Font).Width;
                txt.Width = measured;
                txt.Top = topOffset;
                contextPanel.Controls.Add(txt);

                topOffset += txt.Height + 2;
                if (measured > longest)
                    longest = measured;

                if (field.Name == "Id")
                {
                    txt.Visible = false;
                }
            }

            topOffset = 0;

            int foo = 0;

            foreach (var field in fields)
            {
                List<SourceAttribute> attr = field.GetCustomAttributes<SourceAttribute>().ToList();

                if (attr.Count <= 0)
                    attr = null;

                if (field.GetCustomAttribute<NameAttribute>() == null)
                    continue;

                // textBox if we do not expect an outside source
                if (attr == null && field.PropertyType != typeof(DateTime))
                {
                    var txt = new TextBox
                    {
                        Width = 380,
                        Name = "txt" + foo
                    };
                    if (CurrentModel != null)
                        txt.Text = field.GetValue(CurrentModel).ToString();

                    txt.Left = longest;
                    txt.Top = topOffset;
                    contextPanel.Controls.Add(txt);
                    if (field.Name == "Id")
                    {
                        txt.Visible = false;
                    }

                    if(field.Has<IgnoreUpdateAttribute>())
                    {
                        txt.ReadOnly = true;
                    }

                    topOffset += txt.Height + 2;
                }
                else if(field.PropertyType == typeof(DateTime))
                {
                    var txt = new DateTimePicker
                    {
                        Top = topOffset,
                        Left = longest
                    };

                    if (CurrentModel != null)
                    {
                        DateTime s = (DateTime)field.GetValue(CurrentModel);
                        txt.Value = s;
                    }
                    txt.Format = DateTimePickerFormat.Custom;
                    txt.CustomFormat = "День: dd/MM/yy Время: hh:mm";
                    txt.Width = 380;

                    contextPanel.Controls.Add(txt);
                    topOffset += txt.Height + 2;
                }
                else
                {
                    var txt = new ComboBox
                    {
                        Top = topOffset,
                        Left = longest
                    };

                    var pinf = attr[0].SourceType.GetProperties().Where(x => x.Name == attr[0].FieldName).ToList().First();

                    foreach (var b in Storages[attr[0].SourceType].GetAll())
                    {
                        txt.Items.Add(new ComboBoxItem(SourceAttribute.GetFullSauce(field, b), b.id));
                    }

                    if (CurrentModel != null)
                    {
                        var bruv = Storages[attr[0].SourceType].Find(x => x.id == (int)field.GetValue(CurrentModel));
                        txt.SelectedItem = txt.Items.Cast<ComboBoxItem>().FirstOrDefault(x => (int)x.Value == bruv.id);
                    }

                    txt.Width = 380;

                    contextPanel.Controls.Add(txt);
                    topOffset += txt.Height + 2;
                }

                foo++;
            }

            lastKnownType = CurrentStorage;
        }


        private void updateButton_Click(object sender, EventArgs e)
        {
            var st = Storages[CurrentStorage];

            if (CurrentModel == null)
            {
                MessageBox.Show("Не выбранны данные для изменения");
                return;
            }
            try
            {
                if (st.Find(x => x.id == CurrentModel.id && !x.ToBeRemoved) != null)
                    st.Replace(currentId, GetModel(ModelStyle.Update));

                RefreshDisplay();
            }
            catch
            {

            }
        }

        public Model GetModel(ModelStyle type)
        {
            object[] parameters = new object[lastKnownType.GetProperties().Where(x => x.GetCustomAttribute<NameAttribute>() != null).ToList().Count];

            int fuck = parameters.Length - 1;

            for (int i = contextPanel.Controls.Count - 1; i >= 0; i--)
            {
                var isTextBox = contextPanel.Controls[i] is TextBox;
                var isComboBox = contextPanel.Controls[i] is ComboBox;
                var isDateTimePicker = contextPanel.Controls[i] is DateTimePicker;
                if (!isTextBox && !isComboBox && !isDateTimePicker)
                    break;

                if (isTextBox)
                    parameters[fuck] = (object)contextPanel.Controls[i].Text;

                if (isComboBox)
                {
                    ComboBox box = contextPanel.Controls[i] as ComboBox;
                    ComboBoxItem item = box.SelectedItem as ComboBoxItem;
                    object value = item.Value;
                    parameters[fuck] = value;
                }

                if(isDateTimePicker)
                {
                    DateTimePicker box = contextPanel.Controls[i] as DateTimePicker;
                    parameters[fuck] = box.Value;
                }

                fuck--;
            }

            parameters[0] = string.IsNullOrWhiteSpace(parameters[0].ToString()) ? -1 : (int)Convert.ChangeType(parameters[0], typeof(int));

            if (type == ModelStyle.Update)
                return Model.UpdateModel(lastKnownType, parameters);

            return Model.CreateNewModel(lastKnownType, parameters);
        }

        private void KillContextPanel()
        {
            foreach (Control v in contextPanel.Controls)
            {
                v.Dispose();
            }

            contextPanel.Controls.Clear();
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
                SourceAttribute src = f.GetCustomAttribute<SourceAttribute>();

                if (attr == null)
                    continue;

                MainGrid.Columns.Add(attr.Name.ToLower(), attr.Name);
                if (columnId == 0)
                    MainGrid.Columns[columnId].Visible = false;

                for (int i = 0; i < omegaLUL.Count; i++)
                {
                    if (MainGrid.RowCount < omegaLUL.Count + 1)
                        MainGrid.RowCount++;

                    var value = f.GetValue(omegaLUL[i]);

                    if (src == null)
                        MainGrid[columnId, i].Value = value ?? "Bruh";
                    else if (value != null)
                    {
                        var bull = Storages[src.SourceType].Find(x => x.id == (int)value);

                        if (bull != null)
                        {
                            MainGrid[columnId, i].Value = SourceAttribute.GetFullSauce(f, bull);
                        }
                    }

                    MainGrid.Columns[columnId].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    MainGrid.Columns[columnId].Resizable = DataGridViewTriState.True;
                }

                columnId++;
            }

            CurrentStorage = storage.expectedType;
        }
    }
}
