using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fqn_MVCByZeroStartDemo
{

    public class Books
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string author;
        public string Author
        {
            get { return author; }
            set { author = value; }
        }

        private int publisherId;
        public int PublisherId
        {
            get { return publisherId; }
            set { publisherId = value; }
        }

        private DateTime publishDate;
        public DateTime PublishDate
        {
            get { return publishDate; }
            set { publishDate = value; }
        }

        private string iSBN;
        public string ISBN
        {
            get { return iSBN; }
            set { iSBN = value; }
        }

        private int wordsCount;
        public int WordsCount
        {
            get { return wordsCount; }
            set { wordsCount = value; }
        }

        private decimal unitPrice;
        public decimal UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value; }
        }

        private string contentDescription;
        public string ContentDescription
        {
            get { return contentDescription; }
            set { contentDescription = value; }
        }

        private string aurhorDescription;
        public string AurhorDescription
        {
            get { return aurhorDescription; }
            set { aurhorDescription = value; }
        }

        private string editorComment;
        public string EditorComment
        {
            get { return editorComment; }
            set { editorComment = value; }
        }

        private string tOC;
        public string TOC
        {
            get { return tOC; }
            set { tOC = value; }
        }

        private int categoryId;
        public int CategoryId
        {
            get { return categoryId; }
            set { categoryId = value; }
        }

        private int clicks;
        public int Clicks
        {
            get { return clicks; }
            set { clicks = value; }
        }
    }
}