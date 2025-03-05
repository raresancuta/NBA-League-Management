using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tema_lab10.Domain;

namespace tema_lab10.Repository;

public class StudentsDbRepository : IRepository<long, Student>
{
    public StudentsDbRepository(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public string ConnectionString { set; get; }

    public IEnumerable<Student> FindAll()
    {
        List<Student> students = new List<Student>();
        string query = "SELECT * FROM students ";
        using (var connection = new NpgsqlConnection(ConnectionString))
        {
            try
            {
                connection.Open();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            long id = reader.GetInt64(reader.GetOrdinal("id"));
                            string name = reader.GetString(reader.GetOrdinal("name"));
                            string school = reader.GetString(reader.GetOrdinal("school"));
                            students.Add(new Student(id,name,school));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
        return students;
    }

    public Student GetById(long id)
    {
        string query = "SELECT * FROM students WHERE id = @id";
        using (var connection = new NpgsqlConnection(ConnectionString))
        {
            try
            {
                connection.Open();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string name = reader.GetString(reader.GetOrdinal("name"));
                            string school = reader.GetString(reader.GetOrdinal("school"));
                            return new Student(id, name, school);
                        }
                        else
                        {
                            throw new Exception("Student not found");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }

}