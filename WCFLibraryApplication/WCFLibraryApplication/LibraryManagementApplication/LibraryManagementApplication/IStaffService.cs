using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LibraryManagementApplication
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IStaffService" in both code and config file together.
    [ServiceContract]
    public interface IStaffService
    {
        [OperationContract]
        void DoWork();
        [OperationContract]
        string getBookbyAuthor(string Authorname);

        // [OperationContract]
        //  string addtoBorrowList();

        [OperationContract]
        string getBookbyID(string TitleName);

        [OperationContract]
        bool addtoStaffBorrowList(string staff_id, DateTime borrow);
    }
}
