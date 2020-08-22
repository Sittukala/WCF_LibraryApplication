using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagementApplication
{
    public class Book
    {
        private string Bookid;
        private string title;
        private string author;
        private DateTime publicationTime;
        private string classificationcode;
        private string keyword;

        public string Bookid1 { get => Bookid; set => Bookid = value; }
        public string Title { get => title; set => title = value; }
        public string Author { get => author; set => author = value; }
        public DateTime PublicationTime { get => publicationTime; set => publicationTime = value; }
        public string Classificationcode { get => classificationcode; set => classificationcode = value; }
        public string Keyword { get => keyword; set => keyword = value; }

        public override string ToString()
        {
            return "ID:" + Bookid + "\tTitle:" + title + "\tAuthor" + author + "\tPublicTime" + publicationTime + "\tClassifyCode" + classificationcode + "\tKeyword" + keyword;
        }
    }
}