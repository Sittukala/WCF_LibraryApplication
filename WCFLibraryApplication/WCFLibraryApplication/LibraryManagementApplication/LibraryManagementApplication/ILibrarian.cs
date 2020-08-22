using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LibraryManagementApplication
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILibrarian" in both code and config file together.
    [ServiceContract]
    public interface ILibrarian
    {
        [OperationContract]
        void DoWork();
        [OperationContract]
        bool addBook(int bookID, String TitleName, String AuthorName, String Press, DateTime PublicationTime, String Classificationcode, String Keyword);

        [OperationContract]
        string deleteBook(int bookID);
        [OperationContract]
        string staffEmail(int staff_id);
        [OperationContract]
        string studentEmail(int student_id);
    }
}
