using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;

namespace AnalysisViewerSimple
{
    public partial class Form1 : Form
    {
        private DataGridView dataGrid;
        private Button loadButton;

        public Form1()
        {
            CreateForm();
            LoadData();
        }

        private void CreateForm()
        {
            // Настройка формы
            this.Text = "Статистика анализов";
            this.Size = new Size(850, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Кнопка загрузки
            loadButton = new Button();
            loadButton.Text = "Загрузить данные";
            loadButton.Location = new Point(20, 20);
            loadButton.Size = new Size(120, 30);
            loadButton.Click += LoadButton_Click;
            this.Controls.Add(loadButton);

            // Таблица данных
            dataGrid = new DataGridView();
            dataGrid.Location = new Point(20, 60);
            dataGrid.Size = new Size(790, 390);
            dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGrid.ReadOnly = true;
            dataGrid.AllowUserToAddRows = false;
            this.Controls.Add(dataGrid);
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // ЗАМЕНИТЕ НА ВАШУ БАЗУ ДАННЫХ!
                string connectionString = @"database=localhost:C:\Users\big_rubba27\Documents\New Folder\ZACHET;
                                          user=SYSDBA;password=masterkey;";

                using (FbConnection connection = new FbConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM ANALYSIS_COUNT_BY_TYPE ORDER BY TOTAL_COUNT DESC";

                    DataTable dataTable = new DataTable();
                    FbDataAdapter adapter = new FbDataAdapter(query, connection);
                    adapter.Fill(dataTable);

                    dataGrid.DataSource = dataTable;
                }

                MessageBox.Show("Данные успешно загружены!", "Успех",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}\n\nПроверьте путь к базе данных!", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}