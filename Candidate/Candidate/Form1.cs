using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Candidate
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
        }

        private ICollection<Candidate> GetData()
        {
            const string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Candidate;Integrated Security=True";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                const string sqlExpression = "SELECT * FROM Candidate";

                using (var command = new SqlCommand(sqlExpression, connection))
                { 
                    var reader = command.ExecuteReader();
                    var data = new List<Candidate>();

                    while (reader.Read())
                    {
                        var candidate = new Candidate
                        {
                            Id = Convert.ToInt32(reader[0].ToString()),
                            Name = reader[1].ToString(),
                            Number = Convert.ToInt32(reader[4].ToString()),
                            Professional = reader[3].ToString(),
                            Age = Convert.ToInt32(reader[2].ToString())
                        };

                        data.Add(candidate);
                    }

                    return data;
                }
            }
        }

        private void LoadData()
        {
            var data = GetData();
            dataGridView1.DataSource = data;            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {


                using (TextWriter tw = new StreamWriter("All about Candidate.txt"))
                {
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            tw.Write($"{dataGridView1.Rows[i].Cells[j].Value.ToString()} ");

                            if (j == dataGridView1.Columns.Count - 1)
                            {
                                tw.Write(",");
                            }
                        }
                        tw.WriteLine();
                    }
                }


                MessageBox.Show("Файл успешно сохранен");
            }
            catch
            {
                MessageBox.Show("Ошибка при сохранении файла!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            richTextBox1.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var data = GetData();
            var averageAge = data.Average(c => c.Age);
            var mostFrequencyProfession = data
                .GroupBy(c => c.Professional)
                .OrderByDescending(c => c.Key.Count())
                .First().Key;

            var result = $"Общее колличество участников - {data.Count}" +
                $"\nСредний возраст - {averageAge}" +
                $"\nСамая часто встречающаяся профессия - {mostFrequencyProfession}";

            richTextBox1.Text = result;
        }

       
    }
}
