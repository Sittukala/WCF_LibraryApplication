using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LibraryManagementApplication
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "StudentService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select StudentService.svc or StudentService.svc.cs at the Solution Explorer and start debugging.
    public class StudentService : IStudentService
    {
        DateTime due = DateTime.Today.AddDays(20);
        public void DoWork()
        {
        }
        static MySqlConnection getConnection()
        {

            string myConnectionString = "Database=ssa46;DataSource = mysql.mcscw3.le.ac.uk; User Id = ssa46; Password =ot5Oe4oh";
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            return connection;
        }
        //Add search books by authorname
        public string getBookbyAuthor(string Authorname)
        {
           
            Book b= new Book();
            try
            {
                //Open connection
                MySqlConnection connection = getConnection();
                connection.Open();
                // SQL query assignment
                MySqlCommand mycm = new MySqlCommand("Select bookID,TitleName from BookTableNew where AuthorName = ?AuthorName_para ", connection);
                mycm.Parameters.AddWithValue("?AuthorName_para", Authorname);
                //execute query
                MySqlDataReader msdr = mycm.ExecuteReader();

                while (msdr.Read())
                {
                    if (msdr.HasRows)
                    {

                     
                        b.Title = msdr.GetString("TitleName");
                        b.Bookid1 = msdr.GetString("bookID");

                       

                    }
                }

                msdr.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return "Book Title" + " " + b.Title+ "Book Id " + " " +b.Bookid1;
        }
        
        //Add search books by titlename
        public string getBookbyID(string TitleName)
        {
            Book b = new Book();
            try
            {
                //Open connection
                MySqlConnection connection = getConnection();
                connection.Open();
                // SQL query assignment
                MySqlCommand mycm = new MySqlCommand("Select bookID from BookTableNew where TitleName = ?TitleName_para ", connection);
                mycm.Parameters.AddWithValue("?TitleName_para", TitleName);
                //execute query
                MySqlDataReader msdr = mycm.ExecuteReader();
                while (msdr.Read())
                {
                    if (msdr.HasRows)
                    {

                        b.Bookid1= msdr.GetString("bookID");



                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return b.Bookid1;
        }

        //Add the search result books to borrowlist
        public bool addToStudentBorrowList(string student_id, DateTime borrow )
        {
            bool query = true;
            due=borrow.AddDays(20);
            MySqlConnection connection = getConnection();
            connection.Open();
            MySqlCommand mycm = new MySqlCommand("insert into StudentTable(bookID,student_userID,borrowdate,duedate) values (?bookID_para,?student_userID_para,?borrowdate_para,?duedate_para)", connection);
            mycm.Prepare();
            //     mycm.CommandText = string.Format("insert into feature(type_no,feature_no,feature_name) values (?type_no_para,?feature_no_para,?feature_name_para)");
            string search_book = getBookbyID("C");

            


            mycm.Parameters.AddWithValue("?bookID_para", getBookbyID("C"));
            mycm.Parameters.AddWithValue("?student_userID_para", student_id);
            mycm.Parameters.AddWithValue("?borrowdate_para", borrow);
            mycm.Parameters.AddWithValue("?duedate_para",due);
           
          

           mycm.ExecuteNonQuery();

            connection.Close();
            return query;

         


        }
    }

    }

