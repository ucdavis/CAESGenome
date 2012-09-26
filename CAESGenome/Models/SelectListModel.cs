using System.Web.Mvc;

namespace CAESGenome.Models
{
    public class SelectListModel
    {
        public SelectListModel(string name, SelectList data, string label = null)
        {
            Label = label;
            Name = name;
            Data = data;
        }

        public string Label { get; set; }
        public string Name { get; set; }
        public SelectList Data { get; set; }
        
        public string Id { get { return Name.Replace('.', '_'); } }
    }
}