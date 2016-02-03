using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickTick.Entity;

namespace TickTick.Models
{
    public class SyncTaskBean
    {
        public List<TasksServer> Update = new List<TasksServer>();

        public List<TasksProjects> Delete = new List<TasksProjects>();

        public List<TasksServer> Add = new List<TasksServer>();
        //@JsonInclude(JsonInclude.Include.NON_EMPTY)
        private List<TasksProjects> _deletedInTrash = new List<TasksProjects>();

        public List<TasksProjects> DeletedInTrash
        {
            get { return _deletedInTrash; }
            set { _deletedInTrash = value; }
        }
        //@JsonInclude(JsonInclude.Include.NON_EMPTY)
        private List<TasksProjects> _deletedForever = new List<TasksProjects>();

        public List<TasksProjects> DeletedForever
        {
            get { return _deletedForever; }
            set { _deletedForever = value; }
        }
        //@JsonInclude(JsonInclude.Include.NON_EMPTY)
        private List<Tasks> _addAttachments = new List<Tasks>();

        public List<Tasks> AddAttachments
        {
            get { return _addAttachments; }
            set { _addAttachments = value; }
        }
        //@JsonInclude(JsonInclude.Include.NON_EMPTY)
        private List<Tasks> _deleteAttachments = new List<Tasks>();

        public List<Tasks> DeleteAttachments
        {
            get { return _deleteAttachments; }
            set { _deleteAttachments = value; }
        }
    }
}
