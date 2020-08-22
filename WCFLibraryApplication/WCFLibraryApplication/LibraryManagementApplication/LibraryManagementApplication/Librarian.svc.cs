using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LibraryManagementApplication
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Librarian" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Librarian.svc or Librarian.svc.cs at the Solution Explorer and start debugging.
    public class Librarian : ILibrarian
    {
        static MySqlConnection getConnection()
        {

            string myConnectionString = "Database=ssa46;DataSource = mysql.mcscw3.le.ac.uk; User Id = ssa46; Password =ot5Oe4oh";
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            return connection;
        }

        //Librarian to add books to database
        public bool addBook(int bookID, string TitleName, string AuthorName, string Press, DateTime PublicationTime, string Classificationcode, string Keyword)
        {
            bool query = true;

            MySqlConnection connection = getConnection();
            connection.Open();
            MySqlCommand mycm = new MySqlCommand("insert into BookTableNew(bookID,TitleName,AuthorName,Press,PublicationTime,Classificationcode,Keyword) values (?bookID_para,?TitleName_para,?AuthorName_para,?Press_para,?PublicationTime_para,?Classificationcode_para,?Keyword_para)", connection);
            mycm.Prepare();
            //     mycm.CommandText = string.Format("insert into feature(type_no,feature_no,feature_name) values (?type_no_para,?feature_no_para,?feature_name_para)");

            mycm.Parameters.AddWithValue("?bookID_para", bookID);
            mycm.Parameters.AddWithValue("?TitleName_para", TitleName);
            mycm.Parameters.AddWithValue("?AuthorName_para", AuthorName);
            mycm.Parameters.AddWithValue("?Press_para", Press);
            mycm.Parameters.AddWithValue("?PublicationTime_para", PublicationTime);
            mycm.Parameters.AddWithValue("?Classificationcode_para", Classificationcode);
            mycm.Parameters.AddWithValue("?Keyword_para", Keyword);

            mycm.ExecuteNonQuery();

            connection.Close();


            return query;


        }

        //Librarian to delete books
        public string deleteBook(int bookID)
        {
            string message = "";
            try
            {
                MySqlConnection connection = getConnection();
                connection.Open();
                MySqlCommand mycm = new MySqlCommand("", connection);
                mycm.Prepare();
                mycm.CommandText = String.Format("delete from BookTableNew where bookID=?bookID_para");
                mycm.Parameters.AddWithValue("?bookID_para", bookID);
                mycm.ExecuteNonQuery();
                message = "Book deleted from DB";
                connection.Close();
                return message;
            }
            catch
            {
                message = "Book cannot be deleted";
                return message;
            }
        }

        //Librarian to send email to staff if duedate is < 10
        public string staffEmail(int staff_id)
        {
            bool query;
            string message="Ignore notfication";
            try
            {
                MySqlConnection connection = getConnection();
                connection.Open();
                MySqlCommand mycm = new MySqlCommand("", connection);
                mycm.Prepare();
                mycm.CommandText = String.Format("select bookID,borrowdate,duedate from Staff where staff_userID= ?staff_userID_para");
                mycm.Parameters.AddWithValue("?staff_userID_para", staff_id);
                MySqlDataReader msdr = mycm.ExecuteReader();

                while (msdr.Read())
                {
                    if (msdr.HasRows)
                    {
                        DateTime b_date =msdr.GetDateTime("borrowdate");
                        DateTime d_date = msdr.GetDateTime("duedate");
                        System.TimeSpan difference_date = d_date - b_date;
                       
                        if (difference_date.Days < 10)
                        {
                            message="email notification to staff";
                            query= true;
                        }
                    }

                }
               

            }
            catch
            {
                query = false;
            }
            return message;
        }

        //Librarian to send email to student if due_date<10
        public string studentEmail(int student_id)
        {
            bool query;
            string message = "Ignore notfication";
            try
            {
                MySqlConnection connection = getConnection();
                connection.Open();
                MySqlCommand mycm = new MySqlCommand("", connection);
                mycm.Prepare();
                mycm.CommandText = String.Format("select bookID,borrowdate,duedate from Staff where staff_userID= ?staff_userID_para");
                mycm.Parameters.AddWithValue("?staff_userID_para", student_id);
                MySqlDataReader msdr = mycm.ExecuteReader();

                while (msdr.Read())
                {
                    if (msdr.HasRows)
                    {
                        DateTime b_date = msdr.GetDateTime("borrowdate");
                        DateTime d_date = msdr.GetDateTime("duedate");
                        System.TimeSpan difference_date = d_date - b_date;

                        if (difference_date.Days < 10)
                        {
                            message = "email notification to student";
                            query = true;
                        }
                    }

                }


            }
            catch
            {
                query = false;
            }
            return message;
        }
        public void DoWork()
        {
        }
    }
}
